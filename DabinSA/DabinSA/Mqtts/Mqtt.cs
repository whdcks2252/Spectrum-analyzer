using DabinSA.ViewModel;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DabinSA.Mqtts
{
    public class Mqtt
    {

        private MqttFactory _factory; // MQTT Client를 생성하기 위한 Factory Class
        private MqttClientOptions _options; // 비동기 연결에 필요한 옵션 파라미터에 들어감
        private MqttApplicationMessage _message; // Client가 구독시도하는 메시지
        private CancellationToken _cancellationToken; // 비동기 호출을 취소
        private MainViewModel mainViewModel;

        public IMqttClient MqttClient; // MQTT Client

        public Mqtt(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            _factory = new MqttFactory();
            MqttClient = _factory.CreateMqttClient();
            _cancellationToken = new CancellationToken();


        }


        //구독자
        public async Task recive()
        {
            var factory = new MqttFactory();
            var client = factory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
             .WithTcpServer("192.168.123.95", 1883) // MQTT 브로커 주소
             .Build();

            client.UseConnectedHandler(async e =>
            {
                Debug.WriteLine("Connected to MQTT broker");
                await client.SubscribeAsync(new MqttTopicFilter { Topic = "pact/data2", QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce });

            });

            //연결이 끊어지면 재시도
            client.UseDisconnectedHandler(async e =>
            {
                Debug.WriteLine("Disconnected from MQTT broker");
                await Task.Delay(TimeSpan.FromSeconds(5));
                try
                {
                    await client.ConnectAsync(options, CancellationToken.None);
                }
                catch
                {
                    Debug.WriteLine("Reconnection failed");
                }
            });

            client.UseApplicationMessageReceivedHandler(e =>
            {
                test(e);
            });

            await client.ConnectAsync(options, CancellationToken.None);

            Debug.WriteLine("Press Enter to exit.");
            Console.ReadLine();

        }
        //구독자
        public async Task recive2()
        {
            var factory = new MqttFactory();
            var client = factory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
             .WithTcpServer("192.168.123.95", 1883) // MQTT 브로커 주소
             .Build();

            client.UseConnectedHandler(async e =>
            {
                Debug.WriteLine("Connected to MQTT broker");
                await client.SubscribeAsync(new MqttTopicFilter { Topic = "pact/data1", QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce });

            });

            //연결이 끊어지면 재시도
            client.UseDisconnectedHandler(async e =>
            {
                Debug.WriteLine("Disconnected from MQTT broker");
                await Task.Delay(TimeSpan.FromSeconds(5));
                try
                {
                    await client.ConnectAsync(options, CancellationToken.None);
                }
                catch
                {
                    Debug.WriteLine("Reconnection failed");
                }
            });

            client.UseApplicationMessageReceivedHandler(e =>
            {
                test2(e);
            });

            await client.ConnectAsync(options, CancellationToken.None);

            Debug.WriteLine("Press Enter to exit.");

        }
        public async Task recive3()
        {
            var factory = new MqttFactory();
            var client = factory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
             .WithTcpServer("192.168.123.95", 1883) // MQTT 브로커 주소
             .Build();

            MqttClient.UseConnectedHandler(async e =>
            {
                Debug.WriteLine("Connected to MQTT broker");
                await MqttClient.SubscribeAsync(new MqttTopicFilter { Topic = "pact/command", QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce });

            });

            //연결이 끊어지면 재시도
            MqttClient.UseDisconnectedHandler(async e =>
            {
                Debug.WriteLine("Disconnected from MQTT broker");
                await Task.Delay(TimeSpan.FromSeconds(5));
                try
                {
                    await MqttClient.ConnectAsync(options, CancellationToken.None);
                }
                catch
                {
                    Debug.WriteLine("Reconnection failed");
                }
            });

            await MqttClient.ConnectAsync(options, CancellationToken.None);

            Debug.WriteLine("Press Enter to exit.");

        }



        private void test(MqttApplicationMessageReceivedEventArgs e)
        {
            List<float> recvSpectrumList = new List<float>();

            const int CntOfHeader = 2;
            const int FourByte = 4;

            int MAX_SPECTRUM_NUM = 1001;
            byte[] receivedSpectrum = e.ApplicationMessage.Payload;
            Int32[] _spectrumData = new Int32[MAX_SPECTRUM_NUM];
            StringBuilder sb = new StringBuilder();


            var mode = BitConverter.ToInt32(receivedSpectrum, 0 * FourByte);
            var type = BitConverter.ToInt32(receivedSpectrum, 1 * FourByte);



            for (int i = 0; i < MAX_SPECTRUM_NUM; i++)
            {
                if ((i + 2) * 4 >= receivedSpectrum.Length)
                {
                    break;
                }

                _spectrumData[i] = BitConverter.ToInt32(receivedSpectrum, (i + CntOfHeader) * FourByte);
                if (i < 5)
                {
                    sb.Append(_spectrumData[i]);
                    sb.Append(" ");
                }

                // 스펙트럼 데이터가 100을 곱해서 들어옴
                float spectrumData = (float)BitConverter.ToInt32(receivedSpectrum, (i + CntOfHeader) * FourByte) / 100;
                recvSpectrumList.Add(spectrumData);
            }
            int maxSpectrum = _spectrumData.Max(); ;

            


            string strSpectrumMessage = sb.ToString();

            string strLog = string.Format("Cnt Of Payload {0} ", receivedSpectrum.Length);

            // Spectrum에 0.01 곱해준 값
            double doubledMaxSpectrum = (double)maxSpectrum / 100.0;

            // 임시 로그 출력
            strLog = string.Format("Binary Subscriber Receive Max = {0} ", doubledMaxSpectrum.ToString());
            Debug.WriteLine(_spectrumData[1000].ToString());
            Debug.WriteLine(_spectrumData.Length.ToString());
            Debug.WriteLine(recvSpectrumList[1000].ToString());
            Debug.WriteLine(recvSpectrumList.Count.ToString());
            mainViewModel.Setchart2(ref recvSpectrumList);
        }


        public async void RequestSpectrum()
        {
            if (MqttClient.IsConnected)
            {
                const string FixedValuesPadding = "0 0 1 0 2 0 0 2 0 0 2 0 1 47000 0 0 10000 1 0 10000 0 800 0 1 100 0 0 0";
            Queue queue = new Queue();

            queue.Enqueue("0x04 0x00");
            queue.Enqueue(double.Parse(mainViewModel.CenterFre+"000000"));
            queue.Enqueue(int.Parse(mainViewModel.Span+"000000"));
            queue.Enqueue("10");
            queue.Enqueue("10");
            queue.Enqueue("0");
            queue.Enqueue(int.Parse(mainViewModel.Attenuator));
            queue.Enqueue("0");
            queue.Enqueue(int.Parse(mainViewModel.Offset));
            queue.Enqueue(FixedValuesPadding);

            string execStr = "";
            while (queue.Count != 0)
            {
                execStr += queue.Dequeue().ToString() + " ";
            }

            var message = new MqttApplicationMessageBuilder()
                .WithTopic("pact/command")
                .WithPayload(Encoding.ASCII.GetBytes(execStr))
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();


            if (MqttClient.IsConnected)
            {
                // Send ~~ Publish
                await MqttClient.PublishAsync(message, _cancellationToken);
            }

            string strLog = string.Format("Send Message : {0}", Encoding.UTF8.GetString(message.Payload));
            Debug.WriteLine(strLog);



            await Task.Delay(100); // 잠시 대기 후

            var message2 = new MqttApplicationMessageBuilder()
               .WithTopic("pact/command")
               .WithPayload(Encoding.ASCII.GetBytes("0x11"))
               .WithExactlyOnceQoS()
               .WithRetainFlag()
               .Build();

            if (MqttClient.IsConnected)
            {
                // Send ~~ Publish
                await MqttClient.PublishAsync(message2, _cancellationToken);
            }

            }
        }





        private void test2(MqttApplicationMessageReceivedEventArgs e)
        {

            // 추가적으로 Receive Queue를 점검하고 여러 조건에 따라 설정할 것들이 있지만 (ex. Temperature, IQImb 등)
            // 일단 수신하는 지 확인하기 위해 로그만 찍어줌

            string recvMsgPayload = string.Format("Subscriber Receive Message : {0}", Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
            // Subscriber Receive Message: 0x06 0x02 20 3.537598
            // [2022 - 02 - 21 17:08:25] Subscriber Receive Message: 0x06 0x01
            // [2022 - 02 - 21 17:08:25] Subscriber Receive Message: 0x51 0
            // [2022 - 02 - 21 17:08:26] Subscriber Receive Message: 0x51 0

            // Ignore Message
            if (recvMsgPayload.Contains("0x06 0x01") || recvMsgPayload.Contains("0x51 0") || recvMsgPayload.Contains("0x25 0") || recvMsgPayload.Contains("0x30"))
            {
                return;
            }

            // 배터리 메시지
            string batMsg = "0x06 0x02";
            if (recvMsgPayload.Contains(batMsg))
            {
                // strLog  =  recvMsgPayload.Replace(batMsg , "");
                return;
            }


            Debug.WriteLine(recvMsgPayload);

            // WriteLogEvent(strLog);
        }


        // "192.168.123.95",1883
        //퍼블리셔
        public async Task Connent()
        {
            var factory = new MqttFactory();
            var client = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("192.168.123.95", 1883) // MQTT 브로커 주소
                .Build();

            client.UseConnectedHandler(async e =>
            {
                Debug.WriteLine("Connected to MQTT broker");
                await client.SubscribeAsync(new MqttTopicFilter { Topic = "pact/command", QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce });
            });
            client.UseApplicationMessageReceivedHandler(e =>
            {
                Debug.WriteLine($"Received message on topic: {e.ApplicationMessage.Topic}, payload: {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
            });

            await client.ConnectAsync(options, CancellationToken.None);

            byte[] payload = Encoding.UTF8.GetBytes("0x04 0x00 3650010000 150000000 10 10 0 20 0 0 0 0 0 0 2 0 0 2 0 0 2 0 1 5000 1 0 10000 0 1857 0 1 100 0 0 0");
            var message = new MqttApplicationMessageBuilder()
                .WithTopic("pact/command")
                .WithPayload(payload)
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            await client.PublishAsync(message, CancellationToken.None);



            // 메시지 게시
            // 애플리케이션을 실행 중으로 유지
            Console.ReadLine();

            // await client.DisconnectAsync();

            client.UseDisconnectedHandler(async e =>
            {
                Debug.WriteLine("Disconnected from MQTT broker");
                await Task.Delay(TimeSpan.FromSeconds(5));
                try
                {
                    await client.ConnectAsync(options, CancellationToken.None);
                }
                catch
                {
                    Debug.WriteLine("Reconnection failed");
                }
            });
        }
    }




}

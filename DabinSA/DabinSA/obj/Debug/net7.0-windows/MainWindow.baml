   M S B A M L   `   `   `  ����B  >DabinSA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:DabinSADabinSA  5   	       DabinSA.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:DabinSA     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  5   	   R NOxyPlot.Wpf, Version=2.1.2.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0Y UOxyPlot.Wpf.Shared, Version=2.1.2.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0"oxyhttp://oxyplot.org/wpf   5   	     � Title$  
MainWindow��5
   	   $
��1000��6   $
��1600��6*   
���   � � WindowStartupLocation$ CenterScreen=�5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      $	��250 �6    � RowDefinitions 5   
   �� 5      $��60 �6   6*   �� 5      $��60 �6   6*   �� 5      6   �� 5      $	��140 �6   6+   �� 5      $��70 �6   6*   .+�5   
   � .+�5      ��  �� Orientation$ 
Horizontal=�6   $��10,0,0,0q�62   .+�5      �� $ Vertical=�6   .+�5      �� ���	#FF4B8FFD$
��Bold"�67   $��15(�6J    �� TextAlignment$ Center=�6X   DabinSystems6}   �� 5      
�������$��15(�63   $ Center=�6W   ��6A   �� *Time+6q   5      �� 5       $��10,0,0,0q�63   $��3q�6E   
���   �
�������$��Center=�6   $��Center=�6�   $��14(�6�   ���#8C8C8C$��40��6�   $	��200��6�   $��Signal Generators��6  ��6   �� *SgBT+6!  5!      5"      � 5$   
   $��1��6    5%      �� 5&      �� 5'      $	��200 �6#   .+�5)      �� $ 
Horizontal=�6   .+�5*      ��    CheckBoxMarkerandTrace#��[  6   $��M1��6�    d� 	IsChecked 6K   �� *M1+ �� UpdateSourceTrigger$ PropertyChanged=���6�   �� *MarkerCommand+.��6�   M16�   �� 5+      #��[  6   $��M2��6�    6K   �� *M2+$ PropertyChanged=���6�   �� *MarkerCommand+.��6�   M26�   �� 5,      #��[  6   $��M3��6�    6K   �� *M3+$ PropertyChanged=���6�   �� *MarkerCommand+.��6�   M36�   �� 5-      #��[  6   $��M4��6�    6K   �� *M4+$ PropertyChanged=���6�   �� *MarkerCommand+.��6�   M46�   �� 5.      #��[  6   $��T1��6�    6K   �� *T1+$ PropertyChanged=���6�   �� *MarkerCommand+.��6�   T16�   5/      �� 50      $��1��6   $ 
Horizontal=�6)   .+�51      �� $��
10,10,0,10q�6\   $ Center=�6p   
�������$��20(�6�   ��6   �� *
MarkerName+$ PropertyChanged=�6�   �� 52      $��
10,10,0,10q�6[   $ Center=�6o   
�������$��20(�6�   ��6   �� *	MarkerNum+$ PropertyChanged=�6�   �� 53      $��	5,18,0,10q�6   $ Center=�6/   
�������$��12(�6Y   (dBm)6o   54      55      � 58   
   $��2��6    59      �� 5:      �� 5;      $��20 �6    .+�5>        OxyPlot.Wpf.PlotView  
���   �
�������$��2��6�     OxyPlot.Wpf.PlotViewBase   Model 6A   �� *PlotModelmp+$ PropertyChanged=�y�6�   �� *Chart1+$ PropertyChanged=�*  $Microsoft.Xaml.Behaviors.Interaction	   	Behaviors	 5?         DabinSA.OxyPlotBehavior  5@      
   
OxyCommand
 6,   �� *
OxyCommand+5B        5D      
���   �
�������$��2��6�    6A   �� *PlotModelmp2+$ PropertyChanged=�y�6�   �� *Chart2+$ PropertyChanged=��� 5G      $��10,30,1235,550q�6   $��15(�60   
�������(dBm)6X   � 5I      $��
40,-30,0,0q�6   $��1��6'    5J      �� 5K      �� 5L      �� 5M      .+�5O      �� $��0��6   
�������$��20(�6?   ��6M   �� *
StartFre+$ PropertyChanged=��� 5P      $ Center=�6   $��1��63   
�������$��20(�6V   ��6d   �� *	CenterFre+$ PropertyChanged=��� 5Q      $ Right=�6   $��2��63   
�������$��20(�6V   ��6d   �� *	StopFre+$ PropertyChanged=��� 5R      $��135,0,-55,0q�6   $ Right=�62   $��2��6I   
�������$��20(�6l   (MHz)6�   5S      5T      � 5U   
   $��20,0,0,0q�6   $��3��6!    5V      �� 5W      �� 5X      .+�5Z      �  5[      �� 5\      �� 5]      �� 5^      �� 5_      .+�5a      �� $��0��6   
�������$��15(�6?   ��6M   �� *CenterFreIn+$ PropertyChanged=��� 5b      $��1��6   
�������$��15(�6?   ��6M   �� *AmpIn+$ PropertyChanged=��� 5c      $��2��6   
�������$��15(�6?   ��6M   �� *
OffsetIn+$ PropertyChanged=��� 5d      $��3��6   
�������$��15(�6?   ��6M   �� *SpanIn+$ PropertyChanged=�5e      � 5f      $��1��6    5g      �� 5h      �� 5i      �� 5j      �� 5k      .+�5m      �� $��0��6   
�������$��15(�6?   ��6M   �� *SGmode+$ PropertyChanged=��� 5n      $��1��6   
�������$��15(�6?   ��6M   �� *SGFrequency+$ PropertyChanged=��� 5o      $��2��6   
�������$��15(�6?   ��6M   �� *SGAmp+$ PropertyChanged=�5p      5q      � 5t   
   $��1��6   $��2��6   .+�5u      �� y�6   �� *Stack+$ PropertyChanged=�.+�5v      �� $��	0,0,20,20q�6   $��20(�6/   $��Center=�6=   $��Center=�6Z   ���	#FF4B8FFD��6�   �� *
SubMenuTxt+$ PropertyChanged=��� 5w      -   5         ��Name$	 abc��5w      ��6,   �� *SourceCollection+$ PropertyChanged=���5x      �� 5y      .R�5z      �� $ Vertical=�6%   5{      ��5}      �� 5~      .R�5      ��   ButtonStyle#��[ 6%   ��5�   %   ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" �� �� Path$" DataContext.SubButtonCommand���5�   %   �� *
ButtonName+��5�   !   �� *
ButtonName+$ PropertyChanged=�5�      5�      5�         DabinSA.SelectMarker  5�      y�6"   �� *SelectMakerUI+$ PropertyChanged=� �� Mode$ TwoWay=�6{   �� 5�      #��[ 6;   y�6   �� *SelectMakerUI+��6`   �� *	Command+.��6}   Back6�   5�      � 5�   
   $��4��6   .+�5�      �� $��10,0,0,0q�6   $ 
Horizontal=�6+   .+�5�      	� $��	Frequency��6g     MyButtonStyle#��[ 6�   $��	Frequency��6�   ��6   �� *ChageSASubMenu+$ PropertyChanged=�6�   	� 5�      $
��Span��6g   #��[ 6�   $
��Span��6�   ��6   �� *ChageSASubMenu+$ PropertyChanged=�6�   	� 5�      $��	Amplitude��6g   #��[ 6�   $��	Amplitude��6�   ��6   �� *ChageSASubMenu+$ PropertyChanged=�6�   	� 5�      $��Marker��6g   #��[ 6�   $��Marker��6�   ��6   �� *ChageSASubMenu+$ PropertyChanged=�6�   	� 5�      $��Trace��6g   #��[ 6�   $��Trace��6�   ��6   �� *ChageSASubMenu+$ PropertyChanged=�6�   5�      5�         DabinSA.Modal  5�   
   $��5��6   $��5��6'    K� ZIndex$ 10��6<      ShadowColor$ Black��6N      IsOpen 6b   �� *IsCalculatorModalOpen+$ TwoWay=�.��5�      � $	��500��6   $	��500��6   .+�5�         DabinSA.Cal   � DataContext 6   �� *CalViewModel+5�      5�        5�   
   $��5��6   $��5��6'   $ 10��6<   $ Black��6N    6b   �� *IsSGModalOpen+$ TwoWay=�.��5�      � $	��500��6   $	��500��6   .+�5�         DabinSA.SGView   6   �� *SGViewModel+5�      5�      5�      5�      
   M S B A M L   `   `   `  ����B  >DabinSA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:DabinSADabinSA  5          DabinSA.SGView   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:DabinSA     5      .��5	      �   � ColumnDefinitions  5
   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      $��70 �6   6*   �� 5      6   �� 5      6   �� 5      6   �� 5      6   .+�5   
   �� $��2��6   $��30(�6)   Signal Generators6K   �� 5   
   $��1��6   $��15(�6!   $��8,0,0,68q�6/   $
��Left=�6A   $��55��6\   Mode :6q   �� 5   
   $��1��6   $��
70,0,58,82q�6    $��2��64   ��6H   �� *
ModeList+ �� SelectedItem 6i   �� *SelectCombo+�� 5   
   $��1��6   $��15(�6#   $��2��62   $��7,65,27,107q�6C   Frequency :6h   �� 5   
    �� TextAlignment$ Right=�6   $��15(�6(   $��3��66   $��98,70,118,14q�6J   $��1��6`   ��6m   �� *FreTxt+ �� UpdateSourceTrigger$ PropertyChanged=�{�6�   �� *	SGFreTrue+�� 5   
   $��10(�6   $��2��6"   $��
12,73,68,8q�62   $��1��6F   (MHz)6Z   �� 5!   
   $��15(�6   $��10,10,32,66q�6#   $��2��68   Amplitude :6S   �� 5"   
   $ Right=�6   $��15(�6(   $��3��66   $��98,10,118,73q�6J   $��2��6`   ��6m   �� *AmpTxt+$ PropertyChanged=�{�6�   �� *	SGAmpTrue+�� 5#   
   $��10(�6   $��2��6"   $��12,14,75,73q�62   $��2��6G   (dB)6Z   �� 5%   
   $��2��6   $��62,27,62,46q�6!   $��3��66   $��2��6C   ��6W   �� *
GenerateBT+.��6v   
Generate6�   5&      5'      
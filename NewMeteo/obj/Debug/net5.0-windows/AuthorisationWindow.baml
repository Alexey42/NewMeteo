   M S B A M L   `   `   `  ����C  ?NewMeteo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:NewMeteoNewMeteo  5   	   "    NewMeteo.AuthorisationWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:NewMeteo     5   	     � Title$  Welcome��5   	   $	��300��6   $	��280��6&   .��5	      � .+�5
   
   �� -   5         ��Name$ _TabControl��5
      $
��Left=�6*   $	��216��6E   $	��Top=�6R   $	��272��6j   .@�5      �� $��Sign in��6   .��5      � ���#f5f5f5 � ColumnDefinitions 5      �� 5      $	��17* �6+   �� 5      $	��21* �6+   .+�5      �� $
��Name��6   $
��Left=�6+   $��	56,60,0,0q�6F   $	��Top=�6Y   $��Arial)�6q   $��16(�6�   �� 5      $��Password��6   $
��Left=�6/   $��
30,125,0,0q�6J   $	��Top=�6^   $��Arial)�6v   $��16(�6�   �� 5      $��Sign in��6   $
��Left=�6.   $��	40,10,0,0q�6I   $	��Top=�6\   $��81��6t   $��Arial)�6   $��18(�6�   $
��Bold"�6�   5      5      �� 5      $��Sign up��6   .��5      � ���#f5f5f5 5      �� 5      $��4* �6+   �� 5      $	��11* �6+   �� 5      $
��108* �6+   .+�5      �� $
��Name��6   $
��Left=�6+   $��	56,60,0,0q�6F   $	��Top=�6Y   $��Arial)�6q   $��16(�6�   $��3��6�   �� 5      $��Password��6   $
��Left=�6/   $��
21,125,0,0q�6J   $	��Top=�6^   $��Arial)�6v   $��16(�6�   $��1��6�   $��2��6�   �� 5      $��Sign up��6   $
��Left=�6.   $��8,10,0,0q�6I   $	��Top=�6[   $
��Bold"�6s   $��Arial)�6�   $��18(�6�   $��2��6�   5       5!      5"      �� 5#   
   -   5         ��Name$ button��5#       �� 	IsDefault$
 True��6/   $��Ok��6@   $
��Left=�6M   $��105,234,0,0q�6h   $	��Top=�6}   $��70��6�   $��25��6�   $��Arial)�6�   $��16(�6�   �� 5$   
   -   5         ��Name$ error_message��5$      $�� ��6'   
���  ��$��Center=�6C   $
��Left=�6g   $��
13,180,0,0q�6�   $	��Top=�6�   $	��246��6�   $��14(�6�   $��Normal$�6�   $��Arial)�6�   $��26��6�   �� 5%   
   -   5         ��Name$
 name��5%      $
��Left=�6    $��23��6;   $��
116,85,0,0q�6G    �� TextWrapping$
 Wrap=�6[   $�� ��6o   $	��Top=�6w   $	��120��6�   $��Arial)�6�   $��16(�6�   G� 5&   
   -   5         G�Name$ password��5&      $
��Left=�6(   $��116,152,0,0q�6C   $	��Top=�6X   $	��120��6p   $��23��6|   $��16(�6�   $��Arial)�6�   5(      5)      
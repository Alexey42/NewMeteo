   M S B A M L   `   `   `  ����C  ?NewMeteo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:NewMeteoNewMeteo  5   	       NewMeteo.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:NewMeteo     5   	   -   5          � Title$  
Map editor��5   	   $	��450��6   $	��800��6)   .��5
      � .+�5   
   o� $��Center=�6   $��22��6,   $	��Top=�68   $	��800��6P   .@�5      m� $
��File��6   .@�5      m� -   5        $
��Open��5      6;   m� 5      -   5        $
��Save��5      6;   m� 5      -   5        $
��Find��5      6;   m� 5      -   5        $��Send to base��5      6C   5      m� 5      -   5         m�Name$	 btn��5      $��Bezier��6$   64   m� 5      -   5        $��Point��5      6&   m� 5      -   5        $
��Crop��5      6%   m� 5      -	   5        $��3DView��5      6'   m� 5      $��Options��6   .@�5      m� -
   5        $��Set smoothing��5      62   5      5      �� 5   
   -   5         ��Name$ canvas_main��5      $��10,27,10,10q�6&   .+�5         NewMeteo.PanAndZoom  -   5          Name$ zoom_border��5      $
}�True��64   
�������$��2,0,0,0q�6_      Child. 5      �� -   5        $	��img��5      $
��Left=�6�   $	��Top=�6�   ��5      �� *Source.PixelWidth+ �� RelativeSource �� *Self+��5      �� *Source.PixelHeight+ �� *Self+z�5       V� 5!      .~�5"      �� -   5        	img_scale�� �� CenterX$	 0.5Q�5"   @    �� CenterY$	 0.5Q�6N   6^   T� 5#      -   5        img_transform��5#   J   5$      5&      5'      � 5(      -   5         �Name$ figurePolyline��5(      
���  ��$��2��6<   6R   �� 5)      -   5        	 ��Name$	 
figureLine��5)      
���  ��$��2��64   
 J� IsHitTestVisible$
 False��6H   6c   F� 5*      -   5         F�Name$ figure��5*      
���  ����5+      B� 5,      .��5-      E� -   5        
pathFigure��.��5.      �� -   5        bezierSegment��5.   ,   �� 5/      -   5        lineSegment��5/   A   50      51      z�53      V� 54      .~�55      �� $ 0Q�6)   $ 0Q�65   6C   T� 56      6/   57      59      �� 5:      -   5         ��Name$ WrapRectangle��5:      
������� J� Opacity$	 0.2Q�6;   $��1��5;       �� StrokeDashArray$	 4 2R�6%   
�������z�5<      V� 5=      .~�5>      �� $ 0Q�6)   $ 0Q�65   6C   T� 5?      6/   5@      5B      �� 5C      -   5        $��valueInPoint_lbl��5C      $��0��6,    �� Right$ 0��68   5D      5E      5F      
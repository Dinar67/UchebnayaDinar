   M S B A M L   `   `   `  ����L  HUchebnayaPractica, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:UchebnayaPractica.PagesUchebnayaPractica.Pages  5      &     UchebnayaPractica.Pages.PlanPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:UchebnayaPractica.Pages     5        N� Title$  PlanPage��5	       � 	Resources 5
         SlideIn �� TargetProperty W� From	 W� To g� Duration  SlideOut 
 FadeIn  FadeOut��%�  &	        &	 y     &	 �     &	 _    �� 5   
   .��5      W� $ RenderTransform.X�6   $	 200X�6L   $ 0X�6W   $ 0:0:0.5D�6^   6s   5      �� 5   
   .��5      W� $ RenderTransform.X�6   $ 0X�6L   $	 200X�6U   $ 0:0:0.5D�6^   6s   5      �� 5   
   .��5      W� $ Opacity�6   $ 0X�6B   $ 1X�6K   $ 0:0:0.5D�6R   6g   5      �� 5   
   .��5      W� $ Opacity�6   $ 1X�6B   $ 0X�6K   $ 0:0:0.5D�6R   6g   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��7* �6   �� 5      $	��68* �6   .+�5   
   � $��0��6   .+�5      �� $��Center=�6    �� Orientation$ 
Horizontal=�64   .+�5       �� $��24(�6   ���	#FFBB0C07$
��Bold"�6A   $��	30 0 20 0q�6S   Цех6k   �� 5!      -   5         ��Name$ 
WorkshopCb��5!      	 �� DisplayMemberPath$
	 Name��6/   $	��200��6H   6�   5"      �� 5$      $ 
Horizontal=�6   $��Center=�62   $��Right=�6M   .+�5%      �� -   5        
 ��Name$
 ClearBtn��5%        StilyzedButton#��[ 6+   $	��100��6S   $��0 0 30 0q�6_   .��6�   Очистить6�   �� 5&      -   5        $
 SaveBtn��5&      #��[ 6*   $	��200��6R   $��0 0 30 0q�6^   .��6�   '%Сохранить изменения6�   5'      5(      �� 5*   
   -   5         ��Name$ canvas��5*      $��1��6!   $��10q�6.   z�5+      V� 5,      .~�5-      �� -   5        scaleTransform��5-   %   T� 5.      -   5        translateTransform��5.   )   5/      .+�51      �� -   5         ��Name$ Origin��51      
���  ��$��1��63   $��1��6=    �� Left$	 350��6H   
 �� Top$	 250��6Z   6m   �� 52      -   5        $��	PlanImage��52       J� 	AllowDrop . $��10q�67   $	��500��6C   $	��800��6S   53      �� 55   
   -	   5         ��Name$
 Menu��55      $��25��69     InteractionImage#��[ 6D   $��25��6n   $��Right=�6z   $	��Top=�6�   $��15q�6�   $��1��6�      menu_svgrepo_comDrawingImage#��[ 6�   6   �� 57   
   -
   5         ��Name$ MyFilterPanel��57       J� RenderTransformOrigin$ 0.5,0.5&�6-   $y�	Collapsed=�6M   
�������$��Right=�6w   $	��200��6�   $��1��6�   z�58      T� 59      -   5        myTranslateTransform��59   E    J� Effect 5;      3  -System.Windows.Media.Effects.DropShadowEffect  5<         	Direction$	 221Q�6#      
BlurRadius$ 20Q�63      ShadowDepth$ 5Q�6C      Color$ 	#FFACACAC��6S   6g   .+�5?      �� $	��Top=�6   $��Center=�61   $	��180��6N   .+�5@      �� -   5        $
 Back��5@      $��50��6A   $
��Left=�6L   $��	50 10 0 0q�6g   #��[ 6{    * &arrow_ios_back_svgrepo_comDrawingImage#��[ 6�   z�5A      �� 5B       �� ScaleX$ -1Q�6)   5D      �� 5E      $
��Auto=�6   $	��470��6B   $��5 5 0 0q�6O   $��Disabled=�6`   .��5F      � -   5         �Name$ 	ItemPanel��5F       65   5G      5H      5I      5J      5K      
   M S B A M L   `   `   `  ����L  HUchebnayaPractica, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:UchebnayaPractica.PagesUchebnayaPractica.Pages  5      ,    &UchebnayaPractica.Pages.AddEditFailure   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:UchebnayaPractica.Pages     5        N� Title$  AddEditFailure��5	      .��5      � .+�5   
   �� -   5         ��Name$ 	MainPanel��5      $��Center=�6(   $��Center=�6E   .+�5      �� -   5         ��Name$ TitleTb��5      $
��Bold"�6)   $��20(�6;   ���	#FFBB0C07$��Center=�6`   $��0 0 0 20q�6}   1/Зарегистрировать поломку6�   �� 5      $��Center=�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0 0 0 10q�66   .+�5      �� $	��170��6    Оборудование6:   �� 5      -   5         ��Name$ EquipmentCb��5   +   $	��300��6    �� 
IsEditable .  �� DisplayMemberPath$ Model��6u    �� SelectedItem 6R   �� *	Equipment+6�   5      �� 5      $ 
Horizontal=�6   $��0 0 0 10q�66   .+�5      �� $	��170��6    !Тип оборудования6>   �� 5      -   5        $ TypeEquipmentCb��5   =   $	��300��6   {�.  $
 Name��6V   6q   5      �� 5      $ 
Horizontal=�6   $��0 0 0 10q�66   .+�5      �� $	��170��6    $��Center=�6,   .,Дата и время начала сбоя6a   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ 	StartDate��5   !   $	��235��64   6C   �� 5      -   5        	 ��Name$	 StartTimeTb��5      $��25��64   $��50��6@   $��15 0 0 0q�6K   .��6�   ��5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   9   �� 6   6sys&clr-namespace:System;assembly=mscorlib     69   
 �� 
TargetType"
 ��6$    �� 	Resources 5   "      CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%�   &	        !� 5   &   $
 Left=�6I   $ Center=�6[   $
 None=�6o    5   *   �� 5    .   $��	ЧЧ:ММ��64   ���	#FF909090$��DemiBold"�6[   5"   '   5#   #    �� Triggers 5$   "   P� 5%   &    P� Property � �� �� Empty P� Value# Z 6>   . �5&   *   ��  �� Property � �� �� Value# [  6G   5'   '   P� 5(   &    � �� 6>   Z� . �5)   *   ��  � ��# [  6G   5*   '   P� 5+   &    �@J�IsKeyboardFocused . . �5,   *   ��  � ��
 �����6G   5-   '   5/      .��50   )   51      52      �� 53      $ 
Horizontal=�6   $��0 0 0 10q�66   .+�54      �� $	��170��6    $��Center=�6,   ,*Дата и время конца сбоя6`     55      -   5        $ EndDate��55   !   $	��235��62   6A   �� 56      -   5        $	 	EndTimeTb��56      $��25��62   $��50��6>   $��15 0 0 0q�6I   .��6   ��57      1&clr-namespace:System;assembly=mscorlibSystem 58   9   �� 6   6sys&clr-namespace:System;assembly=mscorlib     69   "
 ��6$    59   "   ��%�   &	        !� 5:   &   $
 Left=�6I   $ Center=�6[   $
 None=�6o    5;   *   �� 5<   .   $��	ЧЧ:ММ��64   ���	#FF909090$��DemiBold"�6[   5>   '   5?   #    5@   "   P� 5A   &    � ��# Z 6>   . �5B   *   ��  � ��# [  6G   5C   '   P� 5D   &    � �� 6>   Z� . �5E   *   ��  � ��# [  6G   5F   '   P� 5G   &    �@J�IsKeyboardFocused . . �5H   *   ��  � ��
 �����6G   5I   '   5K      .��5L   )   5M      5N      �� 5O      -	   5        $ EducationPanel��5O   F   $ Vertical=�6   $��0 0 0 10q�64   .+�5P      �� $��0 0 0 5q�6    $
��Left=�61   Причина поломки6]   �� 5Q      -
   5        $	 ReasonTb��5Q      $	��470��60    �� TextWrapping$
 Wrap=�6<   $��80��6P   ��6]   �� *Reason+6w   5R      5S      �� 5T      -   5         ��Name$ SaveBtn��5T        StilyzedButton#��[ 6&   $��0 10 0 0q�6N   $	��300��6`   .��6�   Сохранить6�   5U      �� 5W   
   -   5         ��Name$
 Back��5W        InteractionImage#��[ 6    * &arrow_ios_back_svgrepo_comDrawingImage#��[ 6H   $��40��6�   $��40��6�   $��10q�6�   $
��Left=�6�   $	��Top=�6�   6�   5X      5Y      
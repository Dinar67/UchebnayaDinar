   M S B A M L   `   `   `  ����L  HUchebnayaPractica, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:UchebnayaPractica.PagesUchebnayaPractica.Pages  5      &     UchebnayaPractica.Pages.TestPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:UchebnayaPractica.Pages     5        N� Title$  TestPage��5	      .��5
      �  � RowDefinitions 5   
   �� 5      $	��53* �6   �� 5      $
��533* �6   .+�5   
   �� $��20,0,0,0q�6   $
��Left=�6'   $��Center=�6B    �� Orientation$ 
Horizontal=�6]   .+�5      �� $��18(�6   $��	0 0 15 0 q�6&   
Тесты6@   �� 5      -   5         ��Name$ SearchTb��5   .   $	��180��6   $��25��6"   .��6d   ��5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   1   �� 6   6sys&clr-namespace:System;assembly=mscorlib     61    �� 
TargetType" ��6    �� 	Resources 5         CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch	 !� Visual��%�   &	        !� 5      $
 Left=�6A   $ Center=�6S   $
 None=�6g   	 5   "   �� 5   &   $��
Поиск��6,   ���	#FF909090$��DemiBold"�6S   5      5      
 �� Triggers
 5      P� 5       P� Property � �� �� Empty P� Value# Z 66   . �5   "   ��  �� Property � �� �� Value# [  6?   5      P� 5        � �� 66   Z� . �5!   "   ��  � ��# [  6?   5"      P� 5#       �@J�IsKeyboardFocused . . �5$   "   ��  � ��
 �����6?   5%      5'      .��5(   !   5)      �� 5*      $��10 0 0 0q�6   $��Center=�6*   Сортировать6R   �� 5+      -   5         ��Name$ SortCb��5+      $��25��6'   $	��180��63   $
��10 0q�6?   .@�5,      �� .��6   Нет6$   �� 5-      .��6   @>По наименованию продукта от А до Я6C   �� 5.      .��6   @>По наименованию продукта от Я до А6C   �� 5/      .��6   По статусу6+   50      51      �� 53   
   -   5         ��Name$ MyList��53       $��1��6    �� View 54      �� 55      .P�56      �� $��ID��6%    �� Width$ 70��61    �� DisplayMemberBinding 6<   �� *Id+6b   �� 57      $/��)Наименование продукта��6%   $	 170��6D    6P   �� *Name+6x   �� 58      $��Номер заказа��6%   $	 200��6;    6G   �� *OrderNumber+6v   �� 59      $(��"Все тесты пройдены��6%   $	 200��6A    �� CellTemplate 5:      �� 5;      .R�5<   "   �� ��61   �� *Passed+ �� Mode$ OneWay=�6[   5=      5?      �� 5@      $ 70��6%   $
��Edit��60    5A      �� 5B      .R�5C   "   ��  ��Name$
 Edit��6E   -   5        $��Center=�5C   (   $��40��6S   $��40��6^    " edit_3_svgrepo_comDrawingImage#��[ 6j     InteractionImage#��[ 6�   6�   5D      5F      �� 5G      $ 70��6%   $��Delete��60    5H      �� 5I      .R�5J   "   �� $ Delete��6E   -   5        $��Center=�5J   (   $��40��6U   $��40��6`    $  delete_1_svgrepo_comDrawingImage#��[ 6l   #��[ 6�   6�   5K      5M      5N      5P      �� 5R   
   $��Right=�6   $��0��61   $ 
Horizontal=�6>   $	��100��6X   $��0 0 40 0q�6d   .+�5S      �� -   5         ��Name$ AddOrderBtn��5S        StilyzedButton#��[ 6*   $��30��6R   $	��100��6^   .��6�   Добавить6�   5T      5W      5X      
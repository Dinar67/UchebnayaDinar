   M S B A M L   `   `   `  ����L  HUchebnayaPractica, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:UchebnayaPractica.ComponentsUchebnayaPractica.Components  5      .    (UchebnayaPractica.Components.SizeControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:UchebnayaPractica.Components     5      $��40��5   "   $��40��6.   $	��975��6=   $	��975��6L   $	��450��6X   .��5	      � .+�5
   
   ��   �� Orientation$  
Horizontal=�6   .+�5      �� -   5         ��Name$ Trash��5       $   delete_1_svgrepo_comDrawingImage#��[  6#   $��25��6z   $��25��6�   $��	10 0 15 0q�6�   6�   �� 5      -   5         ��Name$ NameTb��5      $��25��6<   $	��200��6H   $��0 0 5 0q�6T   ��6&   �� *Name+.��6�   ��5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   1   �� 6   6sys&clr-namespace:System;assembly=mscorlib     61    �� 
TargetType" ��6    �� 	Resources 5        CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%�   &	       !� 5      $
 Left=�6A   $ Center=�6S   $
 None=�6g    5   "   �� 5   &   $-��'Наименование размера��6,   ���	#FF909090$��DemiBold"�6b   5      5      	 �� Triggers	 5      P� 5      
 P� Property
 � �� �� Empty P� Value# Z 66   . �5   "   ��  �� Property � �� �� Value# [ 6?   5      P� 5      
 � �� 66   Z� . �5   "   ��  � ��# [ 6?   5      P� 5      
 �@J�IsKeyboardFocused . . �5   "   ��  � ��
 �����6?   5      5!      .��5"   !   5#      �� 5$      -   5        $ CountTb��5$      $��25��6Y   $��80��6e   ��6'   �� *	SizeValue+ �� StringFormat$ {0:F0}��.��6�   ��5%      1&clr-namespace:System;assembly=mscorlibSystem 5&   1   �� 6   6sys&clr-namespace:System;assembly=mscorlib     61   " ��6    5'      ��%�   &	       !� 5(      $
 Left=�6A   $ Center=�6S   $
 None=�6g    5)   "   �� 5*   &   $��Значение��6,   ���	#FF909090$��DemiBold"�6V   5,      5-      	 5.      P� 5/      
 � ��# Z 66   . �50   "   ��  � ��# [ 6?   51      P� 52      
 � �� 66   Z� . �53   "   ��  � ��# [ 6?   54      P� 55      
 �@J�IsKeyboardFocused . . �56   "   ��  � ��
 �����6?   57      59      .��5:   !   5;      �� 5<      -   5         ��Name$ UnitCb��5<       �� DisplayMemberPath$
 Name��6E   $��25��6^   $	��100��6j   $��5 0 0 0q�6v    �� SelectedItem 6'   �� *Unit+6�   5=      5>      5?      
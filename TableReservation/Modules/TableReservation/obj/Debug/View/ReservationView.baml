   M S B A M L   `   `   `  ����  TableReservation+    %TableReservation.View.ReservationView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   $	��300��6'   �� 5	      $
��Auto �6   $	��350��6*   $	��500��6:   .+�5   
   �� $��0��6   $��0��6!   $��Reservations��61   $��Center=�6E   $��Center=�6`   &   System.Windows.Controls.DataGrid  5   
   $��1��6   $��5q�6    $   HorizontalScrollBarVisibility$
 Auto=�6+   "   VerticalScrollBarVisibility$
 Auto=�6P      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     CanUserResizeColumns .     CanUserResizeRows .     CanUserSortColumns . ��6s   �� 	 �� Path$	 Reservations�
 �� UpdateSourceTrigger$
 PropertyChanged=� �� SelectedItem 5      �� $	 SelectedReservation�$
 PropertyChanged=� �� Mode$ TwoWay=�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Customer Name��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� $	 CustomerName�$
 PropertyChanged=�  5      $ No Of Persons��6%    6<   �� $	 NoOfPersons�$
 PropertyChanged=�  5      $
 From��6%    63   �� $	 TimeFrom�$
 PropertyChanged=�  5      $ To��6%    61   �� $	 TimeTo�$
 PropertyChanged=�5      � 5   
   $��2��6   $
��Left=�6     5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5       $
��Auto �6    �� 5!      $
��Auto �6    �� 5"      $��75 �6    �� 5#      $
��Auto �6     � ColumnDefinitions 5%      �� 5&      �� 5'      .+�5*      �� $��0��6   $��0��6%   $��2��65   $��Quick Information��6I   $��Center=�6b   $��Center=�6}   �� 5,      $��2��6   $��0��6%   $��Customer Name��65   $��Center=�6J   $��Right=�6e   �� 5-      $��2��6   $��1��6%   $��25��65   $��5q�6A   ��6L   �� $&	  SelectedReservation.CustomerName�$
 PropertyChanged=��� 5/      $��3��6   $��0��6%   $��No of Persons��65   $��Center=�6J   $��Right=�6e   �� 50      $��3��6   $��1��6%   $��25��65   $��5q�6A   ��6L   �� $%	 SelectedReservation.NoOfPersons�$
 PropertyChanged=��� 52      $��4��6   $��0��6%   $��	Time From��65   $��Center=�6F   $��Right=�6a   �� 53      $��4��6   $��1��6%   $��25��65   $��5q�6A   ��6L   �� $"	 SelectedReservation.TimeFrom�$
 PropertyChanged=��� 55      $��5��6   $��0��6%   $��Time To��65   $��Center=�6D   $��Right=�6_   �� 56      $��5��6   $��1��6%   $��25��65   $��5q�6A   ��6L   �� $ 	 SelectedReservation.TimeTo�$
 PropertyChanged=��� 58      $��6��6   $��0��6%   $��Contact Number��65   $��Center=�6K   $��Right=�6f   �� 59      $��6��6   $��1��6%   $��25��65   $��5q�6A   ��6L   �� $'	 !SelectedReservation.ContactNumber�$
 PropertyChanged=��� 5;      $��7��6   $��0��6%   $��EMail��65   $��Center=�6B   $��Right=�6]   �� 5<      $��7��6   $��1��6%   $��25��65   $��5q�6A   ��6L   �� $	 SelectedReservation.EMail�$
 PropertyChanged=��� 5>      $��8��6   $��0��6%   $��Table(s)��65   $	��Top=�6E   $��Right=�6]   �� 5?      $��8��6   $��1��6%   $��25��65   $��5q�6A   $	��Top=�6L   $	��200��6d   ��6s   �� $	 SelectedReservationTables�$
 PropertyChanged=�� 5A      $��9��6   $��2��6    $��Center=�64   $��5q�6Q    5B      �� 5C      $
��Auto �6'   �� 5D      $
��Auto �6'   .+�5G      �� $��0��6   $
��Edit��6)   $��25��68   $��5q�6D   $	��100��6O   ��6[   �� $	 EditCommand��� 5H      $��1��6   $��Delete��6)   $��25��6:   $��5q�6F   $	��100��6Q   ��6]   �� $	 DeleteCommand�5I      5J      5K      5L      
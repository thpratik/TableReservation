   M S B A M L   `   `   `  ����X  TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4P:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.ToolkitXceed.Wpf.Toolkit  5      K GTableReservation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:TableReservation.UtilitiesTableReservation.Utilities 5      2   ,TableReservation.View.ReservationDetailsView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      LXceed:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.Toolkit     5      >	Utilities(clr-namespace:TableReservation.Utilities     5      .��5      � $��5q�6     � RowDefinitions  5	   
   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��0��6   $��0��6!   $��2��61   $��Reservation Details��6E   $��Center=�6`   $��Center=�6{   �� 5   
   $��2��6   $��0��6!   $��Customer Name��61   $��Center=�6F   $��Right=�6a   �� 5   
   $��2��6   $��1��6   $��25��6/   $��5q�6;   ��6F   ��  �� Path$% CurrentReservation.CustomerName� �� UpdateSourceTrigger$ PropertyChanged=��� 5   
   $��3��6   $��0��6!   $��No of Persons��61   $��Center=�6F   $��Right=�6a   �� 5    
   $��3��6   $��1��6   $��25��6/   $��5q�6;   ��6F   �� $$ CurrentReservation.NoOfPersons�$ PropertyChanged=��� 5"   
   $��4��6   $��0��6!   $��	Time From��61   $��Center=�6B   $��Right=�6]   #   Xceed.Wpf.Toolkit.ShortUpDown  5#   
   -   5          Name$ timeFrom��5#   9   $��4��6   $��1��6)   $��25��6K   $��60��6W   $��Center=�6b   $
��Left=�6}   $��5q�6�   �   �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int16, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6�   �� $ FromHour�$ PropertyChanged=� �� Mode$ TwoWay=�   Minimum 5$       �� $ MinFromHour�$ PropertyChanged=�   Maximum 6j   �� $ MaxFromHour�$ PropertyChanged=��� 5&   
   $��5��6   $��0��6!   $��Time To��61   $��Center=�6@   $��Right=�6[     5'   
   -   5        $ timeTo��5'   9   $��5��6   $��3��6)   $��25��6I   $��60��6U   $��Center=�6`   $
��Left=�6{   $��5q�6�    6�   �� $ ToHour�$ PropertyChanged=�$ TwoWay=� 5(       �� $ Value�	 �� ElementName$	 timeFrom��$ PropertyChanged=�6 0TableReservation.Utilities.MinMaxToTimeConverter
   Instance �� 	Converter# Z
  5)       �� $ Maximum�$	 timeFrom��$ PropertyChanged=�# Z
 �� 5+   
   $��6��6   $��0��6!   $��Contact Number��61   $��Center=�6G   $��Right=�6b   �� 5,   
   $��6��6   $��1��6   $��25��6/   $��5q�6;   ��6F   �� $&  CurrentReservation.ContactNumber�$ PropertyChanged=��� 5.   
   $��7��6   $��0��6!   $��EMail��61   $��Center=�6>   $��Right=�6Y   �� 5/   
   $��7��6   $��1��6   $��25��6/   $��5q�6;   ��6F   �� $ CurrentReservation.EMail�$ PropertyChanged=�� 51   
   $��8��6   $��0��6   $��2��6,    52      �� 53      $	��225 �6#   �� 54      $
��Auto �6#   �� 55      $	��225 �6#     58      �� 59      $��* �6    �� 5:      $
��Auto �6    �� 5;      $
��Auto �6    �� 5<      $��* �6    .+�5?      &   System.Windows.Controls.DataGrid  $��0��6   $��4��6$   $��0��65   $��5q�6E   $   HorizontalScrollBarVisibility$
 Auto=�6P   "   VerticalScrollBarVisibility$
 Auto=�6u      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     CanUserResizeColumns .     CanUserResizeRows .     CanUserSortColumns . $	��150��5A   z   ��5?   �   �� $ AvailableTables�$ PropertyChanged=� �� SelectedItem 5A      �� $ SelectedAvailableTable�$ PropertyChanged=�$ TwoWay=�   Columns 5B      0  *System.Windows.Controls.DataGridTextColumn  5C      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Table Name��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� $ DisplayName�$ PropertyChanged=�  5D      $ Max Occupancy��6)    6@   �� $ MaxOccupancy�$ PropertyChanged=�5F      �� 5H      $��>>��6   $��1��6(   $��1��65   $��5q�6E   ��6P   �� $ ReserveTableCommand��� 5I      $��<<��6   $��2��6(   $��1��65   $��5q�6E   ��6P   �� $ UnreserveTableCommand�  5K      $��0��6   $��4��6$   $��2��65   $��5q�6E   $
 Auto=�6P   $
 Auto=�6u    .   .   .   .   .   . $	��150��5M   y   ��5K   �   �� $ ReservedTables�$ PropertyChanged=� 5M      �� $ SelectedReservedTable�$ PropertyChanged=�$ TwoWay=� 5N        5O      $ 
Table Name��6)    6=   �� $ DisplayName�$ PropertyChanged=�  5P      $ Max Occupancy��6)    6@   �� $ MaxOccupancy�$ PropertyChanged=�5R      5T      � 5V   
   $��9��6   $��2��6   $��Center=�60   $��5q�6M    5W      �� 5X      $
��Auto �6#   �� 5Y      $
��Auto �6#   .+�5[      �� $��0��6   $
��Save��6%   $��25��64   $��5q�6@   $	��100��6K   ��6W   �� $ SaveCommand��� 5\      $��1��6   $��Cancel��6%   $��25��66   $��5q�6B   $	��100��6M   ��6Y   �� $ CancelCommand�5]      5^      5_      
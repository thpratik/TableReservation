   M S B A M L   `   `   `  ����X  TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4P:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.ToolkitXceed.Wpf.Toolkit  5      K GTableReservation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:TableReservation.UtilitiesTableReservation.Utilities 5      4   .TableReservation.View.ReservationDashBoardView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      LXceed:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.Toolkit     5      >	Utilities(clr-namespace:TableReservation.Utilities     5      .��5      �   � 	Resources  5   
      headerTemplate �� Path  cellTemplate? 9TableReservation.Utilities.ReservationBackgroundConverter   Instance �� 	Converter �� UpdateSourceTrigger��%�   &	        &	 H     �� 5	      .R�5
      �� ��6   �� $ DisplayText�5      �� 5      .R�5      �� ��6   �� $ Value�# Z $ PropertyChanged=�5      5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��5q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5!      �� $��0��6   $��	Time From��6(   $��Center=�69   $
��Left=�6T   #   Xceed.Wpf.Toolkit.ShortUpDown  5"      -   5          Name$ timeFrom��5"   0   $��1��6    $��25��6B   $��60��6N   $��Center=�6Y   $��5q�6t   �   �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int16, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6   �� $ FromHour�$ PropertyChanged=�	 �� Mode$	 TwoWay=�
   Minimum
 5#       �� $ MinFromHour�$ PropertyChanged=�   Maximum 6j   �� $ MaxFromHour�$ PropertyChanged=��� 5%      $��2��6   $��Time To��6(   $��Center=�67   $
��Left=�6R     5&      -   5        $ timeTo��5&   0   $��3��6    $��25��6@   $��60��6L   $��Center=�6W   $��5q�6r    6}   �� $ ToHour�$ PropertyChanged=�$	 TwoWay=�
 5'       �� $ Value� �� ElementName$ timeFrom��$ PropertyChanged=�6 0TableReservation.Utilities.MinMaxToTimeConverter   Instance# Z  5(       �� $ Maximum�$ timeFrom��$ PropertyChanged=�# Z �� 5*      $��4��6   $��No Of Persons��6(   $��Center=�6=   $
��Left=�6X   �� 5+      $��5��6   $��25��6&   $��5q�62   $��50��6=   ��6H   �� $ NoOfPersons�$ PropertyChanged=�$	 TwoWay=��� 5-      $��6��6   $	��Add��6%   $��25��63   $��5q�6?   $	��100��6J   ��6V   �� $ 
AddCommand�5.      &   System.Windows.Controls.DataGrid  50   
   -   5          Name$ dgReservation��50      $��1��6*   $��5,5,10,5q�67   $��Stretch=�6I   $��Stretch=�6g   $   HorizontalScrollBarVisibility$
 Auto=�6�   "   VerticalScrollBarVisibility$
 Auto=�6�      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     CanUserResizeColumns .     CanUserResizeRows .     CanUserSortColumns . 7  1TableReservation.Utilities.AttachedColumnBehavior   HeaderTemplate# [  53         AttachedCellTemplate# [ 54      ��52      �� $ Tables�$ PropertyChanged=�   AttachedColumns 55      �� $ ReservationHours�$ PropertyChanged=�   MappedValues 56      �� $ Reservations�$ PropertyChanged=�   Columns 57      0  *System.Windows.Controls.DataGridTextColumn  58      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Table Name��6%      SortDirection$ 	AscendingX�6�   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 69   �� $ DisplayName�$ PropertyChanged=�6�     59      $ Max Occupancy��6%    6<   �� $ MaxOccupancy�$ PropertyChanged=�5;      � 5=   
   $��2��6   $��5q�6    5>      �� 5?      $
��Auto �6#   �� 5@      $	��300 �6#   �� 5A      $
��Auto �6#   �� 5B      $
��Auto �6#   .+�5E      �� $��0��6   $��Import Tables��6(   $��Center=�6=   $
��Left=�6X   �� 5F      $��1��6   $��25��6&   $��5q�62   $��Stretch=�6=   ��6[   �� $ TableXMLFile�$ PropertyChanged=��� 5H      $��2��6   $��Browse��6%   $��25��66   $��5q�6B   $	��100��6M   ��6Y   �� $ BrowseCommand��� 5I      $��3��6   $��Import��6%   $��25��66   $��5q�6B   $	��100��6M   ��6Y   �� $ ImportCommand�5J      5K      5L      
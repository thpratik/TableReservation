   M S B A M L   `   `   `  ����_  [TableReservation.ApplicationServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullo<clr-namespace:TableReservation.ApplicationServices.Utilities.TableReservation.ApplicationServices.Utilities  5   	   D    >TableReservation.ApplicationServices.Controls.MessageBoxWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   R	Utilities<clr-namespace:TableReservation.ApplicationServices.Utilities     5   	     � 
ResizeMode$  NoResize=�5   	    � SizeToContent$ WidthAndHeight=�6    � WindowStyle$ SingleBorderWindow=�6>    � ShowInTaskbar .   � WindowState$ Normal=�5       � WindowStartupLocation$ CenterScreen=�64   $
��Auto��6Y   $
��Auto��6g    � Title 5   	   ��  �� Path$ MessageTitle� �� UpdateSourceTrigger$ PropertyChanged=�.��5	      � $��5q�6   $
��Auto��6   $
��Auto��6$   $	��150��61   $	��500��6A   	 � RowDefinitions	 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $��0��6   $��5��6   $��2��6'   $��0,0,0,3q�6;   � 5   
   $��1��6   $
��Auto��6   $
��Auto��6*   	 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    $��50��6.   
 � ColumnDefinitions
 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5      �� $��1��6   $��0��6!   $��10q�61   $��50��6=   $��50��6I   $	��Top=�6T   ��6l   �� $ MessageIcon�L  FTableReservation.ApplicationServices.Utilities.MessageBoxIconConverter   Instance �� 	Converter# Z $ PropertyChanged=��� 5      $��1��6   $��1��6%   $��5q�65   $
��Auto��6@   $
��Auto��6N   $��25(�6�   $
��Wrap=�6�   ��6[   �� $ MessageText�$ PropertyChanged=�6�   �� 5      $��3��6   $��0��6&   $��2��66    �� Orientation$ 
Horizontal=�6J   $��Right=�6c   $��5q�6   .+�5      �� $��3q�6   $	��100��6$   $��OK��60   ��6=   �� $ 	OKCommand���6`   �� *.+ �� RelativeSource �� *FindAncestor+ �� AncestorType" �y�5       �� $ MessageButtons�L  FTableReservation.ApplicationServices.Utilities.MessageButtonsConverter   Instance# Z  �� ConverterParameter$ OK��$ PropertyChanged=��� 5!      $��3q�6   $	��100��6$   $	��Yes��60   ��6>   �� $ 
YesCommand���6b   �� *.+ �� *FindAncestor+" �y�5"      �� $ MessageButtons�# Z $	 Yes��$ PropertyChanged=��� 5#      $��3q�6   $	��100��6$   $��No��60   ��6=   �� $ 	NoCommand���6`   �� *.+ �� *FindAncestor+" �y�5$      �� $ MessageButtons�# Z $ No��$ PropertyChanged=��� 5%      $��3q�6   $	��100��6$   $��Cancel��60   ��6A   �� $ CancelCommand���6h   �� *.+ �� *FindAncestor+" �y�5&      �� $ MessageButtons�# Z $ Cancel��$ PropertyChanged=�5'      5(      5)      5*      
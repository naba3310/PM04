   M S B A M L   `   `   `  ����B  >WpfApp4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp4WpfApp4  5   	       WpfApp4.History   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp4     5   	     � Title$4  .История покупок партнера��5      $	��450��6)   $	��800��66   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ lblPartnerInfo��5      $��0��6(   $
��Bold"�65   $��14(�6G   $��10q�6U   �� 5   
   -   5         ��Name$ historyListView��5      $��1��6,   $��10q�69    �� View 5      �� 5      .P�5      �� $��Продукция��6%    �� Width$	 250��6e    �� DisplayMemberBinding 68   �� *ProductName+6e   �� 5      $��Количество��6%   $	 100��6c    69   �� *
Quantity+6c   �� 5      $��Дата продажи��6%   $	 120��6�    6;   �� *Date+ �� StringFormat$ {0:dd.MM.yyyy}��6�   �� 5      $��Цена��6%   $	 120��6v    63   �� *Price+$ {0:N2} руб.��6v   �� 5      $��
Сумма��6%   $	 120��6u    64   �� *Sum+$ {0:N2} руб.��6u   5      5      �� 5   
   -   5         ��Name$ btnBack��5      $��
Назад��6"   $��2��62   $��Right=�6?   $	��100��5      $��10q�6   6)   5       5!      
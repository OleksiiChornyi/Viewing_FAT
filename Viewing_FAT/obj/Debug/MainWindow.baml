   M S B A M L   `   `   `  ����F  BViewing_FAT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Viewing_FATViewing_FAT  5   	       Viewing_FAT.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Viewing_FAT     5   	     � 
ResizeMode$  NoResize=�5   	    � Title$ Viewing FAT��5	   	    � Icon$ icon.jpg��6   $	��382��6-   $��979.6��6:   .��5
      � -   5         �Name$ Grid1��5
      ���	#FF88F3F3.+�5   
   �� -   5        $��Открыть��5      $
��Left=�6#   $	��Top=�6>   $	��119��6V   $��40��6w   $��75,0,0,0q�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$	 dg1��5      $
��Left=�6    $	��353��6;   $��274,0,0,-0.2q�6H   $	��Top=�6^   $	��699��6v      
IsReadOnly .    AutoGenerateColumns .    CanUserAddRows .     CanUserDeleteRows .  	   CanUserReorderColumns	 .  5      �� 5   
   -   5        
 ��Name$
 TextBox_info_prog��5      $
��Left=�6-   $	��353��6H   $��274,0,0,-0.2q�6U    �� TextWrapping$
 Wrap=�6k   $	��Top=�6   $	��699��6�   $��14(�6�    �� 
IsReadOnly . $����Вас приветствует программа для просмотра FAT образа. Для открытия образа необходимо нажать кнопку "Открыть" и выбрать *.vhd файл на компьютере. На этом месте откроется таблица с данными в выбранном образе, их реальным размером, размером, который они занимают на диске и количеством кластеров, выделеных для них. Так же будет выведена информация о образе файловой системы: тип FAT, размер образа и размер, выделяемый для одного кластера.��6�   �� 5   
   -   5         ��Name$ Label_info_fat��5      $�� ��6(   $
��Left=�63   $��0,45,0,0q�6N   $	��Top=�6`   $	��269��6x   �� 5   
   -   5        $ Label_info_vhd��5      $�� ��6(   $
��Left=�63   $��0,76,0,0q�6N   $	��Top=�6`   $	��269��6x   �� 5   
   -   5        $ Label_info_total_clusters��5      $�� ��63   $
��Left=�6>   $��	0,107,0,0q�6Y   $	��Top=�6l   $	��269��6�   �� 5   
   -   5        $" Label_info_num_sect_in_clust��5      $�� ��66   $
��Left=�6A   $��	0,138,0,0q�6\   $	��Top=�6o   $	��269��6�   �� 5   
   -	   5        $! Label_info_bytes_per_sector��5      $�� ��65   $
��Left=�6@   $��	0,169,0,0q�6[   $	��Top=�6n   $	��269��6�   5      5      
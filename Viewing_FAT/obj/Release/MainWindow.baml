   M S B A M L   `   `   `  ���� F  BViewing_FAT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Viewing_FATViewing_FAT      Viewing_FAT.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Viewing_FAT       � 
ResizeMode$  NoResize=� � Title$ Viewing FAT�� � Icon$ icon.jpg��$	��382��$��979.6��.��� -    �Name$ Grid1�����	#FF88F3F3.+��� -   $��Открыть��$
��Left=�$	��Top=�$	��119��$��40��$��75,0,0,0q�&   System.Windows.Controls.DataGrid  -     Name$	 dg1��$
��Left=�$	��353��$��274,0,0,-0.2q�$	��Top=�$	��699��   
IsReadOnly .    AutoGenerateColumns .    CanUserAddRows .     CanUserDeleteRows .  	   CanUserReorderColumns	 .  �� -   
 ��Name$
 TextBox_info_prog��$
��Left=�$	��353��$��274,0,0,-0.2q� �� TextWrapping$
 Wrap=�$	��Top=�$	��699��$��14(� �� 
IsReadOnly . $����Вас приветствует программа для просмотра FAT образа. Для открытия образа необходимо нажать кнопку "Открыть" и выбрать *.vhd файл на компьютере. На этом месте откроется таблица с данными в выбранном образе, их реальным размером, размером, который они занимают на диске и количеством кластеров, выделеных для них. Так же будет выведена информация о образе файловой системы: тип FAT, размер образа и размер, выделяемый для одного кластера.���� -    ��Name$ Label_info_fat��$�� ��$
��Left=�$��0,45,0,0q�$	��Top=�$	��269���� -   $ Label_info_vhd��$�� ��$
��Left=�$��0,76,0,0q�$	��Top=�$	��269���� -   $ Label_info_total_clusters��$�� ��$
��Left=�$��	0,107,0,0q�$	��Top=�$	��269���� -   $" Label_info_num_sect_in_clust��$�� ��$
��Left=�$��	0,138,0,0q�$	��Top=�$	��269���� -	   $! Label_info_bytes_per_sector��$�� ��$
��Left=�$��	0,169,0,0q�$	��Top=�$	��269��
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;
using DiscUtils.Fat;
using DiscUtils;

namespace Viewing_FAT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<InfoFAT> info = new List<InfoFAT>{};
        string FileName;
        string path_to_file = "";
        long size_of_claster;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // диалог для выбора файла
            OpenFileDialog ofd = new OpenFileDialog
            {
                // фильтр форматов файлов
                Filter = "VHD Files(*.vhd;)|*.vhd;|All files (*.*)|*.*",
                // если в диалоге была нажата кнопка Открыть
                InitialDirectory = "C:\\"
            };
            if (ofd.ShowDialog() == true) //Если пользователь выберет файл и нажмет "Open" результатом будет True
            {
                try
                {
                    FileName = ofd.FileName;
                    using (VirtualDisk VHD = VirtualDisk.OpenDisk(ofd.FileName, FileAccess.Read))
                    {
                        FatFileSystem fat = new FatFileSystem(VHD.Partitions[0].Open());

                        size_of_claster = VHD.Parameters.BiosGeometry.BytesPerSector * VHD.Parameters.BiosGeometry.HeadsPerCylinder;
                        Label_info_fat.Content = "Файловая система - " + fat.FileSystemType;
                        Label_info_vhd.Content = "Общий размер образа = " + VHD.Parameters.BiosGeometry.Capacity/1024 + " КБ";
                        Label_info_total_clusters.Content = "В образе всего секторов = " + VHD.Parameters.BiosGeometry.TotalSectors;
                        Label_info_num_sect_in_clust.Content = "Количество секторов на один кластер = " + VHD.Parameters.BiosGeometry.HeadsPerCylinder;
                        Label_info_bytes_per_sector.Content = "Количество байт на сектор = " +  VHD.Parameters.BiosGeometry.BytesPerSector;

                        //Вывод на панель
                        Print_on_Grid(fat, @"");
                        TextBox_info_prog.Visibility = Visibility.Collapsed;
                    }
                }
                catch(Exception) // в случае ошибки выводим MessageBox
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public class InfoFAT
        {
            public string File_direct { get; set; }
            public string Files { get; set; }
            public string File_length_real { get; set; }
            public string File_lenght_on_disk { get; set; }
            public int Num_of_clusters { get; set; }

        }

        long Size_of_directory_real(FatFileSystem fat, string str)
        {
            long size = 0;
            string[] directorys = fat.GetDirectories(str);//directorys name
            string[] files = fat.GetFiles(str);//files name
            for (int i = 0; i < directorys.Length; i++)
            {
                size += Size_of_directory_real(fat, directorys[i]);
            }
            for(int i = 0; i < files.Length; i++)
            {
                size += fat.GetFileLength(@"\\" + files[i]);
            }
            return size;
        }

        long Size_of_directory_on_disk(FatFileSystem fat, string str)
        {
            long size = 0;
            string[] files = fat.GetFiles(str);//files name
            string[] directorys = fat.GetDirectories(str);//directorys name
            for (int i = 0; i < directorys.Length; i++)
            {
                size += Size_of_directory_on_disk(fat, directorys[i]);
            }
            for (int i = 0; i < files.Length; i++)
            {
                size += Size_of_file_on_disk(fat, files[i]);
            }
            return size;
        }

        long Size_of_file_on_disk(FatFileSystem fat, string str)
        {
            long size;
            size = fat.GetFileLength(@"\\" + str);
            if (size % size_of_claster == 0)
                return size;
            else
            {
                size += size_of_claster - (size%size_of_claster);
                return size;
            }
        }

        void Print_on_Grid(FatFileSystem fat, string str)
        {
            dg1.ItemsSource = null;
            info.Clear();

            string[] directorys = fat.GetDirectories(str);//directorys name
            string[] files = fat.GetFiles(str);//files name
            bool is_path_change = false;//Изменялся ли путь

            //Вывод директорий
            if (directorys.Length > 0)
            {
                //path to back directory
                if (directorys[0].Contains(@"\"))
                {
                    string back = directorys[0].Remove(directorys[0].LastIndexOf(@"\", directorys[0].Length - 1));
                    if (back.Contains(@"\"))
                    {
                        path_to_file = back.Remove(back.LastIndexOf(@"\", back.Length - 1));
                        is_path_change = true;
                    }

                    //Button Back
                    List<InfoFAT> tmp_list = new List<InfoFAT>
                    {
                        new InfoFAT
                        {
                            File_direct = "Назад",
                            Files="..",
                            File_length_real = "0",
                            File_lenght_on_disk = "0",
                            Num_of_clusters = 0
                        }
                    };
                    info.AddRange(tmp_list);
                }

                for (int i = 0; i < directorys.Length; i++)
                {
                    List<InfoFAT> tmp_list = new List<InfoFAT>
                    {
                        new InfoFAT
                        {
                            File_direct = "Папка",
                            Files=directorys[i],
                            File_length_real = Size_of_directory_real(fat, directorys[i]).ToString() +  " Bytes",
                            File_lenght_on_disk = Size_of_directory_on_disk(fat, directorys[i]).ToString() + " Bytes",
                            Num_of_clusters = (int)(Size_of_directory_on_disk(fat, directorys[i])/size_of_claster)
                        }
                    };
                    info.AddRange(tmp_list);
                }
            }
            else
            {
                //path to back directory
                if (files.Length > 0)
                {
                    if (files[0].Contains(@"\"))
                    {
                        string back = files[0].Remove(files[0].LastIndexOf(@"\", files[0].Length - 1));
                        if (back.Contains(@"\"))
                        {
                            path_to_file = back.Remove(back.LastIndexOf(@"\", back.Length - 1));
                            is_path_change = true;
                        }
                    }
                }

                //Button back
                List<InfoFAT> tmp_list = new List<InfoFAT>
                {
                    new InfoFAT 
                    {
                        File_direct = "Назад",
                        Files="..", 
                        File_length_real = "0",
                        File_lenght_on_disk = "0",
                        Num_of_clusters = 0
                    }
                };
                info.AddRange(tmp_list);
            }
            //path to back directory
            if (!is_path_change)
            {
                if(files.Length > 0 || directorys.Length > 0)
                    path_to_file = @"";
                else if(str.Contains(@"\"))
                    path_to_file = str.Remove(str.LastIndexOf(@"\", str.Length - 1));
            }
            for (int i = 0; i < files.Length; i++)
            {
                List<InfoFAT> tmp_list = new List<InfoFAT>
                {
                    new InfoFAT 
                    {
                        File_direct = "Файл", 
                        Files=files[i], 
                        File_length_real = fat.GetFileLength(@"\\" + files[i]).ToString() + " Bytes", 
                        File_lenght_on_disk = Size_of_file_on_disk(fat, files[i]).ToString() + " Bytes",
                        Num_of_clusters = (int)(Size_of_file_on_disk(fat, files[i])/size_of_claster)
                    }
                };
                info.AddRange(tmp_list);
            }
            dg1.ItemsSource = info;
            dg1.Columns[0].Header = "Тип";
            dg1.Columns[1].Header = "Файлы";
            dg1.Columns[2].Header = "Реальный размер файлов";
            dg1.Columns[3].Header = "Размер файлов на диске";
            dg1.Columns[4].Header = "Количество занимаемых кластеров";
        }

        private void Dg1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (this.dg1.Columns.Count != 0 && this.dg1.SelectedItem != null)
                {
                    //Чтобы не было исключений
                    string str = ((Viewing_FAT.MainWindow.InfoFAT)this.dg1.SelectedItem).Files;
                    if (str != null)
                    {
                        if (((Viewing_FAT.MainWindow.InfoFAT)this.dg1.SelectedItem).File_direct == "Папка")
                        {
                            using (VirtualDisk VHD = VirtualDisk.OpenDisk(FileName, FileAccess.Read))
                            {
                                FatFileSystem fat = new FatFileSystem(VHD.Partitions[0].Open());
                                Print_on_Grid(fat, str);
                            }
                        }
                        else if(((Viewing_FAT.MainWindow.InfoFAT)this.dg1.SelectedItem).File_direct == "Файл")
                        {
                            Save(str);
                        }
                        else if (((Viewing_FAT.MainWindow.InfoFAT)this.dg1.SelectedItem).File_direct == "Назад")
                        {
                            using (VirtualDisk VHD = VirtualDisk.OpenDisk(FileName, FileAccess.Read))
                            {
                                FatFileSystem fat = new FatFileSystem(VHD.Partitions[0].Open());
                                Print_on_Grid(fat, path_to_file);
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error in mouse double click of datagrid", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save(string str)
        {
            using (VirtualDisk VHD = VirtualDisk.OpenDisk(FileName, FileAccess.Read))
            {
                FatFileSystem fat = new FatFileSystem(VHD.Partitions[0].Open());

                //Копирование файла
                using (Stream bootStream = fat.OpenFile(str, FileMode.Open, FileAccess.ReadWrite))
                {
                    //Определение формата сохраняемого файла
                    string file_format = str.Remove(0, str.LastIndexOf('.'));

                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        Title = "Cохранение файла",
                        Filter = "Files(*" + file_format.ToUpper() + ")|*" + file_format.ToUpper() + "|All files (*.*)|*.*",
                        InitialDirectory = "c:\\"
                    };
                    if (sfd.ShowDialog() == true)
                    {
                        try
                        {
                            using (FileStream outStream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                CopyStream(bootStream, outStream);
                            }
                        }

                        catch
                        {
                            MessageBox.Show("Невозможно сохранить файл", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
    }
}

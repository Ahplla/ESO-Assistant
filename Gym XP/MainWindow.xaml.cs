using ESO_Assistant.Classes;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Gat.Controls;


namespace Gym_XP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        private string FFilePath { get; set; }
        public string FilePath
        {
            get { return FFilePath; }
            set
            {
                FFilePath = value;
                NotifyPropertyChanged("FilePath");
            }
        }

        public string GetMD5(string FileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(FileName))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "‌​").ToLower();
                }
            }
        }

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            ToolTipService.ShowDurationProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(Int32.MaxValue));
            ToolTipService.InitialShowDelayProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(0));
            using (RegistryKey R = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\age of empires 3 expansion pack 2\\1.0"))
            {
                object P = R.GetValue("setuppath");
                if (P != null)
                {
                    FilePath = P.ToString();
                    if (File.Exists(Path.Combine(FilePath, "DataPY.bar")))
                        if (GetMD5(Path.Combine(FilePath, "DataPY.bar")) == "e6‌​c0‌​d4‌​0d‌​08‌​86‌​f5‌​97‌​a0‌​85‌​46‌​63‌​2b‌​53‌​6a‌​5d")
                        {
                            bEnable.IsEnabled = true;
                            bDisable.IsEnabled = false;
                        }
                        else
                        {
                            bEnable.IsEnabled = false;
                            bDisable.IsEnabled = true;
                        }
                }
                else
                {
                    bEnable.IsEnabled = true;
                    bDisable.IsEnabled = false;
                }
            }
        }

        private async void bEnable_Click(object sender, RoutedEventArgs e)
        {
            bEnable.IsEnabled = false;
            try
            {
                if (File.Exists(Path.Combine(FilePath, "DataPY.bar")))
                {
                    using (FileStream fileStream = File.Create(Path.Combine(Paths.GetAppDirectoryPath(), "DataPY_mod.zip")))
                    {
                        Assembly.GetExecutingAssembly().GetManifestResourceStream("Gym_XP.Mod.DataPY.zip").CopyTo(fileStream);
                    }
                    await Task.Run(() => ZipFile.OpenRead(Path.Combine(Paths.GetAppDirectoryPath(), "DataPY_mod.zip")).Entries[0].ExtractToFile(Path.Combine(FilePath, "DataPY.bar"), true));
                    bDisable.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("The necessary file does not exist or the destination folder you selected is invalid!");
                    bEnable.IsEnabled = true;
                }
            }
            catch { bEnable.IsEnabled = true; }

        }

        private async void bDisable_Click(object sender, RoutedEventArgs e)
        {
            bDisable.IsEnabled = false;
            try
            {
                if (File.Exists(Path.Combine(FilePath, "DataPY.bar")))
                {
                    using (FileStream fileStream = File.Create(Path.Combine(Paths.GetAppDirectoryPath(), "DataPY_original.zip")))
                    {
                        Assembly.GetExecutingAssembly().GetManifestResourceStream("Gym_XP.Original.DataPY.zip").CopyTo(fileStream);
                    }
                    await Task.Run(() => ZipFile.OpenRead(Path.Combine(Paths.GetAppDirectoryPath(), "DataPY_original.zip")).Entries[0].ExtractToFile(Path.Combine(FilePath, "DataPY.bar"), true));
                    bEnable.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("The necessary file does not exist or the destination folder you selected is invalid!");
                    bDisable.IsEnabled = true;
                }
            }
            catch { bDisable.IsEnabled = true; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This mod will level up your homecity by 100." + Environment.NewLine +
    "I am not responsible for the consequences of using this mod!" + Environment.NewLine + Environment.NewLine +
   "1. Find a partner with the same mod." + Environment.NewLine +
   "2. Enable mod." + Environment.NewLine +
    "3. Start the game." + Environment.NewLine +
    "4. Play a game on fast mod." + Environment.NewLine +
    "5. Wait 2 minutes and resign (the winner will receive the XP)." + Environment.NewLine +
    "6. Close the game." + Environment.NewLine +
    "7. Disable mod." + Environment.NewLine +
    "8. Done!");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenDialogView openDialog = new OpenDialogView();
            OpenDialogViewModel vm = (OpenDialogViewModel)openDialog.DataContext;
            vm.IsDirectoryChooser = true;
            bool? result = vm.Show();
            if (result == true)
                FilePath = vm.SelectedFolder.Path;
            else
                FilePath = string.Empty;
        }
    }
}

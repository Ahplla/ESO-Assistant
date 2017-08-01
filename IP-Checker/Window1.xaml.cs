#region copyright
/*MIT License

Copyright (c) 2015-2017 XaKO

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/
#endregion
using ESO_Assistant.Classes;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace IP_Checker
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : MetroWindow
    {
        public Window1()
        {
            InitializeComponent();
            DataContext = this;
            dataGridView1.ItemsSource = MyGrid;
            Open(Path.Combine(Paths.GetAppDirectoryPath(), "IP.txt"));
        }
        public ObservableCollection<Row> MyGrid = new ObservableCollection<Row>();
        public class Row : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public void NotifyPropertyChanged(string propName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

            }
            private string ip = "";
            private string eso = "";
            public string IP
            {
                get { return ip; }
                set
                {
                    if (ip != value)
                    {
                        ip = value;
                        NotifyPropertyChanged("IP");
                    }
                }
            }
            public string ESO
            {
                get { return eso; }
                set
                {
                    if (eso != value)
                    {
                        eso = value;
                        NotifyPropertyChanged("ESO");
                    }
                }
            }
        }

        private void Save(string path)
        {
            StreamWriter file = new StreamWriter(path, false, Encoding.UTF8);
            try
            {
                for (int r = 0; r <= MyGrid.Count - 1; r++)
                {
                    if (MyGrid[r].IP != "" && MyGrid[r].ESO != "")
                        file.WriteLine(MyGrid[r].IP + "=" + MyGrid[r].ESO);
                }

                file.Close();
            }
            catch
            {
                file.Close();
            }
        }

        private void Open(string path)
        {
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path, Encoding.UTF8);
                try
                {
                    MyGrid.Clear();
                    string newline;
                    while ((newline = file.ReadLine()) != null)
                    {
                        string[] values = newline.Split('=');
                        var R = new Row()
                        {
                            IP = values[0],
                            ESO = values[1]
                        };
                        if (R.IP != "" && R.ESO != "")
                            if (MyGrid.Any(p => p.ESO == R.ESO && p.IP == R.IP) == false)
                                MyGrid.Add(R);
                    }
                    file.Close();
                }
                catch
                {
                    file.Close();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                Filter = "Text files (*.txt)|*.txt"
            };
            if (openFileDialog1.ShowDialog() == true)
            {
                Open(openFileDialog1.FileName);

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog()
            {
                Filter = "Text files (*.txt)|*.txt"
            };
            if (saveFileDialog1.ShowDialog() == true)
            {
                Save(saveFileDialog1.FileName);
            }
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {

        }

        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            Save(Path.Combine(Paths.GetAppDirectoryPath(), "IP.txt"));
            e.Cancel = true;
            this.Hide();
        }

        private void dataGridView1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dataGridView1.SelectedItem != null && ((MainWindow)Apps.Current.MainWindow).button2.IsEnabled)
                try
                {
                    if ((dataGridView1.SelectedItem as Row).IP != "")
                    {
                        ((MainWindow)Apps.Current.MainWindow).textBox1.Text = (dataGridView1.SelectedItem as Row).IP;
                        ((MainWindow)Apps.Current.MainWindow).button2.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }
                }
                catch { }
        }
    }
}

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
using IP_Checker.Utils;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Media;
using ESO_Assistant.Classes;
using Microsoft.Win32;

namespace IP_Checker
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        public ObservableCollection<Row> MyGrid = new ObservableCollection<Row>();
      

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


        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            Save(Path.Combine(Paths.GetAppDirectoryPath(), "IP.txt"));
            Environment.Exit(0);
        }

        private void dataGridView1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dataGridView1.SelectedItem != null && button2.IsEnabled)
                try
                {
                    if ((dataGridView1.SelectedItem as Row).IP != "")
                    {
                        textBox1.Text = (dataGridView1.SelectedItem as Row).IP;
                        button2.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }
                }
                catch { }
        }
    


    private string FCountryText { get; set; }
        public string CountryText
        {
            get { return FCountryText; }
            set
            {
                FCountryText = value;
                NotifyPropertyChanged("CountryText");
            }
        }

        private string FCityText { get; set; }
        public string CityText
        {
            get { return FCityText; }
            set
            {
                FCityText = value;
                NotifyPropertyChanged("CityText");
            }
        }


        private string FTimeText { get; set; }
        public string TimeText
        {
            get { return FTimeText; }
            set
            {
                FTimeText = value;
                NotifyPropertyChanged("TimeText");
            }
        }

        private string FHourText { get; set; }
        public string HourText
        {
            get { return FHourText; }
            set
            {
                FHourText = value;
                NotifyPropertyChanged("HourText");
            }
        }

        private string FBPS { get; set; }
        public string BPS
        {
            get { return FBPS; }
            set
            {
                FBPS = value;
                NotifyPropertyChanged("BPS");
            }
        }

        private string FPPS { get; set; }
        public string PPS
        {
            get { return FPPS; }
            set
            {
                FPPS = value;
                NotifyPropertyChanged("PPS");
            }
        }


        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            ToolTipService.ShowDurationProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(Int32.MaxValue));
            ToolTipService.InitialShowDelayProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(0));

            GeneralTimer.Tick += GeneralTimer_Tick;
            GeneralTimer.Start();
        dataGridView1.ItemsSource = MyGrid;
        Open(Path.Combine(Paths.GetAppDirectoryPath(), "IP.txt"));
        listView1.ItemsSource = MyView;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }
        MediaPlayer Sound1 = new MediaPlayer();
        private DispatcherTimer GeneralTimer = new DispatcherTimer(DispatcherPriority.Render)
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };
        private ObservableCollection<ListItem> MyView = new ObservableCollection<ListItem>();
        public class ListItem : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public void NotifyPropertyChanged(string propName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

            }
            private string ip = "";
            private string eso = "";
            private string pr = "";
            private string avatar = "pack://application:,,,/Avatars/avatarY52-sm.(0,0,4,1).jpg";
            private string firstupdate = "";
            private string lastupdate = "";
            private string hint = "";

            public string IP
            {
                get { return ip; }
                set
                {
                    if (ip != value)
                    {
                        ip = value;
                        NotifyPropertyChanged("IP");
                        NotifyPropertyChanged("IPHidden");
                    }
                }
            }
            public string IPHidden
            {
                get { return new string('●', ip.Length); }
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
            public string PR
            {
                get { return pr; }
                set
                {
                    if (pr != value)
                    {
                        pr = value;
                        NotifyPropertyChanged("PR");
                    }
                }
            }

            public string Avatar
            {
                get { return avatar; }
                set
                {
                    if (avatar != value)
                    {
                        avatar = value;
                        NotifyPropertyChanged("Avatar");
                    }
                }
            }

            /* public string HomeCity
             {
                 get { return homecity; }
                 set
                 {
                     if (homecity != value)
                     {
                         homecity = value;
                         NotifyPropertyChanged("HomeCity");
                     }
                 }
             }*/
            public string FirstUpdate
            {
                get { return firstupdate; }
                set
                {
                    if (firstupdate != value)
                    {
                        firstupdate = value;
                        NotifyPropertyChanged("FirstUpdate");
                        NotifyPropertyChanged("Combined");
                    }
                }
            }
            public string LastUpdate
            {
                get { return lastupdate; }
                set
                {
                    if (lastupdate != value)
                    {
                        lastupdate = value;
                        NotifyPropertyChanged("LastUpdate");
                        NotifyPropertyChanged("Combined");
                    }
                }
            }
            public string Hint
            {
                get { return hint; }
                set
                {
                    if (hint != value)
                    {
                        hint = value;
                        NotifyPropertyChanged("Hint");
                    }
                }
            }

            public string Combined
            {
                get { return string.Format("{0}" + (char)10 + "{1}", FirstUpdate, LastUpdate); }
            }
        }
    
        public SharpPcap.LibPcap.LibPcapLiveDevice captureDevice = null;
        public SharpPcap.WinPcap.WinPcapDevice statDevice = null;
        private string adresseLocale = null;
        public bool isHost = false;

        public string AdresseLocale { get => adresseLocale; set => adresseLocale = value; }

        private string CompareIP(string ip1, string ip2)
        {
            string R = "";
            try
            {
                byte[] IP1 = IPAddress.Parse(ip1).GetAddressBytes();
                byte[] IP2 = IPAddress.Parse(ip2).GetAddressBytes();
                /*  if (BitConverter.IsLittleEndian)
                  {
                      Array.Reverse(IP1);
                      Array.Reverse(IP2);
                  }*/
                byte A = IP1[0];
                byte B = IP1[1];
                byte C = IP1[2];
                byte D = IP1[3];

                byte A2 = IP2[0];
                byte B2 = IP2[1];
                byte C2 = IP2[2];
                byte D2 = IP2[3];

                if (A == A2)
                    R = "Low probability";
                else
                    return R;
                if (B == B2)
                    R = "Medium probability";
                if (C == C2)
                    R = "High probability";
                if (D == D2)
                    R = "100% probability";
            }
            catch
            {
                R = "";
            }

            return R;
        }


        private int LWIndexOf(string Text)
        {

            int R = -1;
            for (int i = 0; i < MyView.Count; i++)
                if (MyView[i].IP == Text)

                    return (i);
            return (R);
        }
        private int posNull(int start, byte[] data)
        {
            if (data != null)
                for (int i = start; i < data.Length - 1; i++)
                    if (data[i] == 0 && data[i + 1] == 0)
                        if (data[start + 1] == 0)
                            return i + 1;
                        else
                            return i;
            return data.Length;
        }
        private string GetPR(byte[] data)
        {
            string R = "";
            int count = 0;
            if (data != null)
            {
                for (int i = 0; i < data.Length - 6; i++)
                {
                    if (data[i] == 0 && data[i + 1] == 0 && data[i + 2] == 6 || data[i + 2] == 4 && data[i + 3] == 0 && data[i + 4] > 47 && data[i + 4] < 58)
                        count++;

                    if (count == 2)
                    {

                        byte[] P = new byte[2];
                        P[0] = data[i + 4];
                        if (data[i + 6] != 0)
                            P[1] = data[i + 6];
                        return Encoding.ASCII.GetString(P);

                    }
                }
            }
            return R;
        }

        private string GetAvatar(byte[] data)
        {
            string R = "";
            int start = 0;
            //     int end = 0;
            if (data != null)
            {
                for (int i = 0; i < data.Length - 3; i++)
                {
                    if (data[i] == 0 && data[i + 1] == 0 && data[i + 2] == 2 && data[i + 3] == 0)
                    {
                        start = i + 8;
                        try
                        {
                            return Encoding.Unicode.GetString(data, start, posNull(start, data) - start);
                        }
                        catch
                        { }
                    }
                }
            }
            return R;
        }

        private string GetHomeCity(byte[] data)
        {
            string R = "";
            int start = 0;
            //     int end = 0;
            if (data != null)
            {
                for (int i = 0; i < data.Length - 3; i++)
                {
                    if (data[i] == 0 && data[i + 1] == 0 && (data[i + 2] == 16) && data[i + 3] == 0)
                    {
                        start = i + 4;
                        try
                        {
                            return Encoding.Unicode.GetString(data, start, posNull(start, data) - start);
                        }
                        catch { }
                    }

                }


            }
            return R;
        }


        private string GetPair(string ip)
        {
            StringBuilder Names = new StringBuilder();
            for (int i = 0; i < MyGrid.Count; i++)
                if (CompareIP(MyGrid[i].IP, ip) != "")
                    Names.AppendLine(MyGrid[i].ESO + " : " + CompareIP(MyGrid[i].IP, ip));
            if (Names.Length == 0)
                return "Unknown person. Add to IP and ESO names...";
            else
            {
                string R = Names.ToString();
                return R.Remove(R.Length - Environment.NewLine.Length);
            }
        }

        private void ParseData(byte[] byteData, string srcIP, string dstIP)
        {
            Stat S = new Stat();
            string localIP = AdresseLocale;
            string IP = localIP;
            if (srcIP == localIP)
                IP = dstIP;
            if (dstIP == localIP)
                IP = srcIP;
            if (IP != localIP)
                if (byteData != null && (byteData[0] == 3 || byteData[0] == 4))
                {

                    int index = LWIndexOf(IP);

                    if (index == -1)
                    {
                        var LI = new ListItem()
                        {
                            IP = IP
                        };

                        if (byteData[0] == 3 && byteData[8] == 6)
                        {
                            isHost = (localIP == srcIP);
                            if (!isHost)
                                if (LI.ESO == "")
                                {
                                    LI.ESO = Encoding.Unicode.GetString(byteData, 13, posNull(13, byteData) - 13);
                                }
                        }
                        if (byteData[0] == 3 && byteData[7] == 0 && byteData[8] == 1 && byteData[9] == 19 && byteData[10] == 10)
                            if (isHost)
                            {

                                if (byteData[11] == 106 || byteData[11] == 130 || byteData[11] == 154 || byteData[11] == 178 || byteData[11] == 202 || byteData[11] == 226 || byteData[11] == 250)
                                    if (LI.ESO == "")
                                        LI.ESO = Encoding.Unicode.GetString(byteData, 19, posNull(19, byteData) - 19);

                                if (byteData[11] == 118 || byteData[11] == 142 || byteData[11] == 166 || byteData[11] == 190 || byteData[11] == 214 || byteData[11] == 238 || (byteData[11] == 6 && byteData[12] == 1))
                                    if (LI.PR == "")
                                        LI.PR = S.FormatPR(Encoding.Unicode.GetString(byteData, 19, posNull(19, byteData) - 19));

                                /*   if (byteData[11] == 125 || byteData[11] == 149 || byteData[11] == 173 || byteData[11] == 197 || byteData[11] == 221 || byteData[11] == 245 || (byteData[11] == 13 && byteData[12] == 1))
                                       if (Encoding.Unicode.GetString(byteData, 19, posNull(19, byteData) - 19) != "")
                                           LI.HomeCity = (Encoding.Unicode.GetString(byteData, 19, posNull(19, byteData) - 19));*/

                                if (byteData[11] == 116 || byteData[11] == 140 || byteData[11] == 164 || byteData[11] == 188 || byteData[11] == 212 || byteData[11] == 236 || (byteData[11] == 4 && byteData[12] == 1))
                                    if (LI.Avatar == "pack://application:,,,/Avatars/avatarY52-sm.(0,0,4,1).jpg")
                                        LI.Avatar = S.GetAvatarFromID(Encoding.Unicode.GetString(byteData, 19, posNull(19, byteData) - 19));

                            }

                        if (byteData[0] == 4)
                            if (!isHost)
                            {
                                if (LI.PR == "")
                                    LI.PR = S.FormatPR(GetPR(byteData));
                                if (LI.Avatar == "pack://application:,,,/Avatars/avatarY52-sm.(0,0,4,1).jpg")
                                    LI.Avatar = S.GetAvatarFromID(GetAvatar(byteData));
                                /*if (GetHomeCity(byteData) != "")
                                    LI.HomeCity = GetHomeCity(byteData);*/
                            }

                        string msg = LI.IP;
                        if (LI.ESO != "")
                            msg += ": " + LI.ESO;
                        //ShowCustomBalloon("User " + msg + " is connected!", LI.Avatar);
                        if (LI.ESO != "")
                        {
                            var R = new Row()
                            {
                                IP = LI.IP,
                                ESO = LI.ESO
                            };
                            try
                            {
                                Dispatcher.Invoke((Action)(() =>
                                {
                                    if (MyGrid.Any(p => p.ESO == R.ESO && p.IP == R.IP) == false)
                                        MyGrid.Add(R);
                                }));
                            }
                            catch { }
                        }
                        string Date = DateTime.Now.ToLongTimeString();
                        LI.FirstUpdate = Date;
                        LI.LastUpdate = Date;

                        LI.Hint = GetPair(LI.IP);
                        try
                        {
                            Dispatcher.Invoke((Action)(() =>
                            {
                                if (LI.IP != "")
                                {
                                    Sound1.Open(new Uri("music.mp3", UriKind.Relative));
                                    Sound1.Play();
                                }
                                MyView.Add(LI);
                                foreach (GridViewColumn c in dataGridView.Columns)
                                {
                                    c.Width = 0; //set it to no width
                                    c.Width = double.NaN; //resize it automatically
                                }
                            }));
                        }
                        catch { }
                    }
                    else
                    {
                        var LI = MyView[index];


                        if (byteData[0] == 3 && byteData[8] == 6)
                        {
                            isHost = (localIP == srcIP);
                            if (!isHost)
                                if (LI.ESO == "")
                                    LI.ESO = Encoding.Unicode.GetString(byteData, 13, posNull(13, byteData) - 13);
                        }
                        if (byteData[0] == 3 && byteData[7] == 0 && byteData[8] == 1 && byteData[9] == 19 && byteData[10] == 10)
                            if (isHost)
                            {

                                if (byteData[11] == 106 || byteData[11] == 130 || byteData[11] == 154 || byteData[11] == 178 || byteData[11] == 202 || byteData[11] == 226 || byteData[11] == 250)
                                    if (LI.ESO == "")
                                        LI.ESO = Encoding.Unicode.GetString(byteData, 19, posNull(19, byteData) - 19);

                                if (byteData[11] == 118 || byteData[11] == 142 || byteData[11] == 166 || byteData[11] == 190 || byteData[11] == 214 || byteData[11] == 238 || (byteData[11] == 6 && byteData[12] == 1))
                                    if (LI.PR == "")
                                        LI.PR = S.FormatPR(Encoding.Unicode.GetString(byteData, 19, posNull(19, byteData) - 19));

                                /*if (byteData[11] == 125 || byteData[11] == 149 || byteData[11] == 173 || byteData[11] == 197 || byteData[11] == 221 || byteData[11] == 245 || (byteData[11] == 13 && byteData[12] == 1))
                                    if (Encoding.Unicode.GetString(byteData, 19, posNull(19, byteData) - 19) != "")
                                        LI.HomeCity = (Encoding.Unicode.GetString(byteData, 19, posNull(19, byteData) - 19));*/

                                if (byteData[11] == 116 || byteData[11] == 140 || byteData[11] == 164 || byteData[11] == 188 || byteData[11] == 212 || byteData[11] == 236 || (byteData[11] == 4 && byteData[12] == 1))
                                    if (LI.Avatar == "pack://application:,,,/Avatars/avatarY52-sm.(0,0,4,1).jpg")
                                        LI.Avatar = S.GetAvatarFromID(Encoding.Unicode.GetString(byteData, 19, posNull(19, byteData) - 19));

                            }

                        if (byteData[0] == 4)
                            if (!isHost)
                            {

                                if (LI.PR == "")
                                    LI.PR = S.FormatPR(GetPR(byteData));
                                if (LI.Avatar == "pack://application:,,,/Avatars/avatarY52-sm.(0,0,4,1).jpg")
                                    LI.Avatar = S.GetAvatarFromID(GetAvatar(byteData));
                                /*if (GetHomeCity(byteData) != "")
                                    LI.HomeCity = GetHomeCity(byteData);*/

                            }

                        string Date = DateTime.Now.ToLongTimeString();
                        if (LI.LastUpdate != Date)
                            LI.LastUpdate = Date;
                        if (LI.ESO != "")
                        {
                            var R = new Row()
                            {
                                IP = LI.IP,
                                ESO = LI.ESO
                            };
                            try
                            {
                                Dispatcher.Invoke((Action)(() =>
                                {
                                    if (MyGrid.Any(p => p.ESO == R.ESO && p.IP == R.IP) == false)
                                        MyGrid.Add(R);
                                }));
                            }
                            catch { }
                        }

                        LI.Hint = GetPair(LI.IP);
                        try
                        {
                            Dispatcher.Invoke((Action)(() =>
                            {
                                MyView[index] = LI;
                                foreach (GridViewColumn c in dataGridView.Columns)
                                {
                                    c.Width = 0; //set it to no width
                                    c.Width = double.NaN; //resize it automatically
                                }
                            }));
                        }
                        catch { }
                    }
                }
        }

        private void Program_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            Packet packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

            var udpPacket = (UdpPacket)packet.Extract(typeof(UdpPacket));
            var ipPacket = (IpPacket)packet.Extract(typeof(IpPacket));
            //    captureDevice.StopCapture();
            if (udpPacket != null && ipPacket != null)
            {
                var srcIP = ipPacket.SourceAddress.ToString();
                var dstIP = ipPacket.DestinationAddress.ToString();
                var srcPort = udpPacket.SourcePort;
                var data = udpPacket.PayloadData;
                if (srcPort == 2300 || srcPort == 2301)
                {
                    ParseData(data, srcIP, dstIP);

                }

            }
            //    captureDevice.StartCapture();
        }

        private void GeneralTimer_Tick(object sender, EventArgs e)
        {
            GeneralTimer.Stop();
            try
            {
                for (int i = 0; i < MyView.Count; i++)
                    if (MyView[i].LastUpdate != null)
                        if ((int)DateTime.Now.Subtract(DateTime.Parse(MyView[i].LastUpdate)).TotalMilliseconds > 2500)
                        {
                            string msg = MyView[i].IP;
                            if (MyView[i].ESO != "")
                                msg += ": " + MyView[i].ESO;
                            //ShowCustomBalloon("User " + msg + " is disconnected!", MyView[i].Avatar);
                            MyView.RemoveAt(i);
                            break;
                        }
            }
            catch { }
            GeneralTimer.Start();
        }
        static ulong oldSec = 0;
        static ulong oldUsec = 0;

        private void device_OnPcapStatistics(
             object sender, SharpPcap.WinPcap.StatisticsModeEventArgs statistics)
        {
            ulong delay = (statistics.Statistics.Timeval.Seconds - oldSec) *
                1000000 - oldUsec + statistics.Statistics.Timeval.MicroSeconds;
            long bps = (statistics.Statistics.RecievedBytes * 1000000) / (long)delay;
            long pps = (statistics.Statistics.RecievedPackets * 1000000) / (long)delay;
            try
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    BPS = "Bytes per sec: " + bps.ToString("N0");
                    PPS = "Packets per sec: " + pps.ToString("N0");

                }));
            }
            catch { }

            oldSec = statistics.Statistics.Timeval.Seconds;
            oldUsec = statistics.Statistics.Timeval.MicroSeconds;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string strIP = null;
            IPHostEntry HosyEntry = Dns.GetHostEntry((Dns.GetHostName()));
            if (HosyEntry.AddressList.Length > 0)
            {
                foreach (IPAddress ip in HosyEntry.AddressList)
                {
                    strIP = ip.ToString();
                    if (
                        (strIP[0] == 49 && strIP[1] == 48 && strIP[2] == 46) || // 10.
                        (strIP[0] == 49 && strIP[1] == 55 && strIP[2] == 50 && strIP[3] == 46 && ((strIP[4] == 49 && strIP[5] > 54) || (strIP[4] == 50) || (strIP[4] == 51 && strIP[5] < 50 && strIP[5] > 47)) && strIP[6] == 46) || // 172.16 - 172.31
                        (strIP[0] == 49 && strIP[1] == 57 && strIP[2] == 50 && strIP[3] == 46 && strIP[4] == 49 && strIP[5] == 54 && strIP[6] == 56) // 192.168
                        )
                        AdresseLocale = strIP;
                }
            }

            foreach (SharpPcap.LibPcap.LibPcapLiveDevice dev in SharpPcap.LibPcap.LibPcapLiveDeviceList.Instance)
            {
                for (int i = 0; i < dev.Addresses.Count; i++)
                {
                    var ip = dev.Addresses[i].Addr.ipAddress;
                    if (ip != null)
                        if (ip.ToString() == AdresseLocale)
                        {
                            captureDevice = dev;
                            break;
                        }

                }
            }
            foreach (SharpPcap.WinPcap.WinPcapDevice dev in SharpPcap.WinPcap.WinPcapDeviceList.Instance)
            {
                for (int i = 0; i < dev.Addresses.Count; i++)
                {
                    var ip = dev.Addresses[i].Addr.ipAddress;
                    if (ip != null)
                        if (ip.ToString() == AdresseLocale)
                        {
                            statDevice = dev;
                            break;
                        }

                }
            }
            if (statDevice != null)
            {
                statDevice.OnPcapStatistics += new SharpPcap.WinPcap.StatisticsModeEventHandler(device_OnPcapStatistics);
                int readTimeoutMilliseconds = 1000;
                statDevice.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
                statDevice.Filter = "ip and udp";
                statDevice.Mode = SharpPcap.WinPcap.CaptureMode.Statistics;
                statDevice.StartCapture();
            }
            if (captureDevice != null)
            {
                captureDevice.OnPacketArrival += new PacketArrivalEventHandler(Program_OnPacketArrival);
                captureDevice.Open(DeviceMode.Promiscuous, 1000);
                captureDevice.Filter = "ip and udp";
                captureDevice.StartCapture();

            }
        }

        /*private void ShowCustomBalloon(string msg, string image)
        {
           // MyNotifyIcon.HideBalloonTip();
            if (image == "")
                MyNotifyIcon.ShowBalloonTip("ESO-Assistant - IP checker", msg, BalloonIcon.Info);
            else
            {

                var bitmap = new Bitmap(Application.GetResourceStream(new Uri(image)).Stream);
                var h = bitmap.GetHicon();

                MyNotifyIcon.ShowBalloonTip("ESO-Assistant - IP checker", msg, System.Drawing.Icon.FromHandle(h), true);
            }
        }*/

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            try
            {
                captureDevice.Close();
                statDevice.Close();

            }
            catch
            {
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            button2.IsEnabled = false;
            image1.Source = null;
            image2.Source = null;
            image2.IsEnabled = false;
            CountryText = "";
            CityText = "";
            TimeText = "";
            HourText = "";
            GetIPInfo("http://api.sypexgeo.net/json/" + textBox1.Text);
        }

        async Task<string> HttpGetAsync(string URI)
        {
            try
            {
                HttpClient hc = new HttpClient();
                Task<System.IO.Stream> result = hc.GetStreamAsync(URI);

                System.IO.Stream vs = await result;
                using (StreamReader am = new StreamReader(vs, Encoding.UTF8))
                {
                    return await am.ReadToEndAsync();
                }
            }
            catch
            {
                return "";
            }
        }

        async void GetVPN(string URL)
        {
            string json = await HttpGetAsync(URL);
            try
            {
                VPNInfo vpnInfo = JsonConvert.DeserializeObject<VPNInfo>(json);
                double R = Double.Parse(vpnInfo.result, new CultureInfo("en-us"));
                if (R >= 0 && R < 0.5)
                {
                    image2.Source = new BitmapImage(new Uri("pack://application:,,,/VPN/1.png"));
                    image2.ToolTip = "No VPN detected!";
                    image2.IsEnabled = true;
                }
                else
                    if (R >= 0.5 && R < 0.9)
                {
                    image2.Source = new BitmapImage(new Uri("pack://application:,,,/VPN/2.png"));
                    image2.ToolTip = "VPN: Low probability";
                    image2.IsEnabled = true;
                }
                else
                        if (R >= 0.9 && R < 0.95)
                {
                    image2.Source = new BitmapImage(new Uri("pack://application:,,,/VPN/3.png"));
                    image2.ToolTip = "VPN: Medium probability";
                    image2.IsEnabled = true;
                }
                else
                            if (R >= 0.95 && R < 0.99)
                {
                    image2.Source = new BitmapImage(new Uri("pack://application:,,,/VPN/4.png"));
                    image2.ToolTip = "VPN: High probability";
                    image2.IsEnabled = true;
                }
                else
                                if (R >= 0.99)
                {
                    image2.Source = new BitmapImage(new Uri("pack://application:,,,/VPN/5.png"));
                    image2.ToolTip = "VPN detected!";
                    image2.IsEnabled = true;
                }
            }
            catch
            {
            }
            button2.IsEnabled = true;
        }

        async void GetIPInfo(string URI)
        {

            string json = await HttpGetAsync(URI);
            try
            {
                IPInfo ipInfo = JsonConvert.DeserializeObject<IPInfo>(json);
                string ip = ipInfo.ip;
                string city = ipInfo.city.name_en;
                string country = ipInfo.country.name_en;
                string flag = ipInfo.country.iso;
                string utc = ipInfo.region.utc;
                if (city == "")
                    utc = ipInfo.country.utc;
                if (country != "")
                    CountryText = "Country: " + country;
                else
                    CountryText = "Coutry: unknown";
                if (city == "")
                    CityText = "City: unknown";
                else
                    CityText = "City: " + city;
                if (utc == "")
                {
                    TimeText = "Local Time: unknown";
                    HourText = "Hours Between: unknown";
                }
                else
                {
                    DateTime Times = TimeZone.CurrentTimeZone.ToUniversalTime(DateTime.Now.AddMinutes(Double.Parse(utc, new CultureInfo("en-us")) * 60));
                    TimeText = "Local Time: " + Times.ToString();
                    double Hours = Double.Parse(utc, new CultureInfo("en-us")) - TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).TotalHours;
                    if (Hours > 0)
                        HourText = "Hours Between: +" + Hours.ToString();
                    else
                        HourText = "Hours Between: " + Hours.ToString();
                }
                try
                {
                    image1.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/" + flag + ".png"));
                }
                catch
                {
                    image1.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/_unknown.png"));

                }
                GetVPN("http://check.getipintel.net/check.php?ip=" + ip + "&contact=main.xakops@yandex.ru&format=json&flags=f");
            }
            catch
            {
                button2.IsEnabled = true;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (pbutton1.IsOpen)
                pbutton1.IsOpen = false;
            else
                pbutton1.IsOpen = true;
        }

        private void listView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listView1.SelectedItem != null && button2.IsEnabled)
            {
                textBox1.Text = (listView1.SelectedItem as ListItem).IP;
                button2.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItem != null)
            {
                var LI = listView1.SelectedItem as ListItem;
                var R = new Row()
                {
                    IP = LI.IP,
                    ESO = LI.ESO
                };
                if (MyGrid.Any(p => p.ESO == R.ESO && p.IP == R.IP) == false)
                    MyGrid.Add(R);
                ShowDialog();
            }
        }

        private void MetroWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Sound1.Close();
            }
            catch { }
        }

    }
}

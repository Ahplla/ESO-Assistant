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
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using Gappalytics.Core;
using Microsoft.Win32;

namespace ESO_Assistant.Classes
{
    public class Feed : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }
        private string FAuthor = "";
        public string Author
        {
            get { return FAuthor; }
            set
            {
                if (FAuthor != value)
                {
                    FAuthor = value;
                    NotifyPropertyChanged("Author");
                    NotifyPropertyChanged("Tip");
                }
            }
        }

        public string Tip
        {
            get { return Title + Environment.NewLine + "by " + Author + " at " + Date; }
        }

        private string FDate;
        public string Date
        {
            get { return FDate; }
            set
            {
                if (FDate != value)
                {
                    FDate = value;
                    NotifyPropertyChanged("Date");
                    NotifyPropertyChanged("Tip");
                }
            }
        }

        private string FTitle = "";
        public string Title
        {
            get { return FTitle; }
            set
            {
                if (FTitle != value)
                {
                    FTitle = value;
                    NotifyPropertyChanged("Title");
                    NotifyPropertyChanged("Tip");
                }
            }
        }

        private string FLink = "";
        public string Link
        {
            get { return FLink; }
            set
            {
                if (FLink != value)
                {
                    FLink = value;
                    NotifyPropertyChanged("Link");
                }
            }
        }
    }



    public class Notification : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }
        private string FOwner = "";
        public string Owner
        {
            get { return FOwner; }
            set
            {
                if (FOwner != value)
                {
                    FOwner = value;
                    NotifyPropertyChanged("Owner");
                }
            }
        }

        private string FDate;
        public string Date
        {
            get { return FDate; }
            set
            {
                if (FDate != value)
                {
                    FDate = value;
                    NotifyPropertyChanged("Date");
                }
            }
        }

        private string FTitle = "";
        public string Title
        {
            get { return FTitle; }
            set
            {
                if (FTitle != value)
                {
                    FTitle = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        private string FIcon = "";
        public string Icon
        {
            get { return FIcon; }
            set
            {
                if (FIcon != value)
                {
                    FIcon = value;
                    NotifyPropertyChanged("Icon");
                }
            }
        }
    }


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


    public class VPNInfo
    {
        public string status { get; set; }
        public string result { get; set; }
        public string queryIP { get; set; }
        public string queryFlags { get; set; }
        public object queryOFlags { get; set; }
        public string queryFormat { get; set; }
        public string contact { get; set; }
    }

    public class AnalyticsHelper
    {

        private IAnalyticsSession _analyticsSession;

        private static AnalyticsHelper _Current;
        public static AnalyticsHelper Current
        {
            get
            {
                if (_Current == null)
                    Initialize();
                return _Current;
            }
        }


        /// <summary>
        /// This method displays how to use non persistent mode of Gappalytics, each time you run will make a new unique user on GA dashboard
        /// For persistent mode, store first visit date, visit count (increment each time), and random number somewhere like app settings
        /// Use Constructor overload for Analytics sessions 
        /// </summary>
        private static void Initialize()
        {

            using (RegistryKey AS = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                object oUserGUID = AS.GetValue("UserGUID");
                object oVisitCount = AS.GetValue("VisitCount");
                object oFirstVisit = AS.GetValue("FirstVisit");
                int UserGUID;
                int VisitCount;
                DateTime FirstVisit;
                if (oUserGUID == null)
                {
                    Random random = new Random();
                    UserGUID = random.Next(0, int.MaxValue);
                    AS.SetValue("UserGUID", UserGUID);
                }
                else
                {
                    UserGUID = (int)oUserGUID;
                }

                if (oVisitCount == null)
                {
                    VisitCount = 1;
                    AS.SetValue("VisitCount", VisitCount);
                }
                else
                {
                    VisitCount = (int)oVisitCount + 1;
                }

                if (oFirstVisit == null)
                {
                    FirstVisit = DateTime.Now;
                    AS.SetValue("FirstVisit", FirstVisit);
                }
                else
                {
                    FirstVisit = DateTime.Parse(oFirstVisit.ToString());
                }


                _Current = new AnalyticsHelper()
                {
                    _analyticsSession = new AnalyticsSession("", "UA-100762863-1", UserGUID, VisitCount, FirstVisit)

                };
            }
        }


        public void LogEvent(string Category, string Label)
        {
            try
            {
                var request = _analyticsSession.CreatePageViewRequest("/event/", "Event");
                request.SendEvent(Category, "", Label, "");
            }
            catch
            {
            }
        }
    }
    public class City
    {
        public string id { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string name_ru { get; set; }
        public string name_en { get; set; }
        public string name_de { get; set; }
        public string name_fr { get; set; }
        public string name_it { get; set; }
        public string name_es { get; set; }
        public string name_pt { get; set; }
        public string okato { get; set; }
        public string vk { get; set; }
        public string population { get; set; }
    }

    public class Region
    {
        public string id { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string name_ru { get; set; }
        public string name_en { get; set; }
        public string name_de { get; set; }
        public string name_fr { get; set; }
        public string name_it { get; set; }
        public string name_es { get; set; }
        public string name_pt { get; set; }
        public string iso { get; set; }
        public string timezone { get; set; }
        public string okato { get; set; }
        public string auto { get; set; }
        public string vk { get; set; }
        public string utc { get; set; }
    }

    public class Country
    {
        public string id { get; set; }
        public string iso { get; set; }
        public string continent { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string name_ru { get; set; }
        public string name_en { get; set; }
        public string name_de { get; set; }
        public string name_fr { get; set; }
        public string name_it { get; set; }
        public string name_es { get; set; }
        public string name_pt { get; set; }
        public string timezone { get; set; }
        public string area { get; set; }
        public string population { get; set; }
        public string capital_id { get; set; }
        public string capital_ru { get; set; }
        public string capital_en { get; set; }
        public string cur_code { get; set; }
        public string phone { get; set; }
        public string neighbours { get; set; }
        public string vk { get; set; }
        public string utc { get; set; }
    }

    public class IPInfo
    {
        public string ip { get; set; }
        public City city { get; set; }
        public Region region { get; set; }
        public Country country { get; set; }
        public string error { get; set; }
        public string request { get; set; }
        public string created { get; set; }
        public string timestamp { get; set; }
    }

    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }


    public class ClanMember : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }
        private string FName = "";
        private int FID = 0;
        public string Name
        {
            get { return FName; }
            set
            {
                if (FName != value)
                {
                    FName = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public int ID
        {
            get { return FID; }
            set
            {
                if (FID != value)
                {
                    FID = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }
    }

    public class TreeItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }
        private string FName = "";
        private int FViewers = 0;
        private string FStatus = "";
        private int FFollowers = 0;
        private TimeSpan FLength;
        private string FURL;

        public string Name
        {
            get { return FName; }
            set
            {
                if (FName != value)
                {
                    FName = value;
                    NotifyPropertyChanged("Name");
                    NotifyPropertyChanged("Hint");
                }
            }
        }

        public string URL
        {
            get { return FURL; }
            set
            {
                if (FURL != value)
                {
                    FURL = value;
                    NotifyPropertyChanged("URL");
                }
            }
        }

        public string Status
        {
            get { return FStatus; }
            set
            {
                if (FStatus != value)
                {
                    FStatus = value;
                    NotifyPropertyChanged("Status");
                    NotifyPropertyChanged("Hint");
                }
            }
        }

        public TimeSpan Length
        {
            get { return FLength; }
            set
            {
                if (FLength != value)
                {
                    FLength = value;
                    NotifyPropertyChanged("Length");
                    NotifyPropertyChanged("Hint");
                }
            }
        }

        public int Viewers
        {
            get { return FViewers; }
            set
            {
                if (FViewers != value)
                {
                    FViewers = value;
                    NotifyPropertyChanged("Viewers");
                    NotifyPropertyChanged("Hint");
                }
            }
        }

        public int Followers
        {
            get { return FFollowers; }
            set
            {
                if (FFollowers != value)
                {
                    FFollowers = value;
                    NotifyPropertyChanged("Followers");
                    NotifyPropertyChanged("Hint");
                }
            }
        }

        public string Hint
        {
            get
            {
                return Name + Environment.NewLine + "Status: " + Status + Environment.NewLine + "Followers: " + Followers.ToString() + Environment.NewLine + "Viewers: " + Viewers.ToString() + Environment.NewLine + "Length: " + Length.ToString(@"hh\:mm\:ss");
            }
        }

    }

    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }
        public Game()
        {
            FStreams = new ObservableCollection<TreeItem>();
        }

        public string Name { get; set; }

        private string FIcon { get; set; }
        public string Icon
        {
            get
            { return FIcon; }
            set
            {
                if (FIcon != value)
                {
                    FIcon = value;
                    NotifyPropertyChanged("Icon");
                }
            }
        }
        private ObservableCollection<TreeItem> FStreams { get; set; }
        public ObservableCollection<TreeItem> Streams
        {
            get
            { return FStreams; }
            set
            {
                if (FStreams != value)
                {
                    FStreams = value;
                    NotifyPropertyChanged("Streams");
                }
            }
        }



    }

    public class ELOItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }
        private string FName = "";
        private int FValueELO = 1600;
        private string FELOChangeUp = "1600";
        private string FELOChangeDown = "1600";
        public string Name
        {
            get { return FName; }
            set
            {
                if (FName != value)
                {
                    FName = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public string ELOChangeUp
        {
            get { return FELOChangeUp; }
            set
            {
                if (FELOChangeUp != value)
                {
                    FELOChangeUp = value;
                    NotifyPropertyChanged("ELOChangeUp");
                }
            }
        }

        public string ELOChangeDown
        {
            get { return FELOChangeDown; }
            set
            {
                if (FELOChangeDown != value)
                {
                    FELOChangeDown = value;
                    NotifyPropertyChanged("ELOChangeDown");
                }
            }
        }

        public int ELOValue
        {
            get { return FValueELO; }
            set
            {
                if (FValueELO != value)
                {
                    FValueELO = value;
                    NotifyPropertyChanged("ELOValue");
                    NotifyPropertyChanged("ELOChangeUp");
                    NotifyPropertyChanged("ELOChangeDown");
                }
            }
        }

    }


        public class ChartItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }
        private string FName = "";
        private Int64 FValue = 0;



        public string Name
        {
            get { return FName; }
            set
            {
                if (FName != value)
                {
                    FName = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }


        public Int64 Value
        {
            get { return FValue; }
            set
            {
                if (FValue != value)
                {
                    FValue = value;
                    NotifyPropertyChanged("Value");
                }
            }
        }

    }


    [DataContract]
    public class ListItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }
        private string name = "";
        private int status = 0;
        private string icon = "pack://application:,,,/Icons/0.png";
        private string hint = "";

        public string Icon
        {
            get { return icon; }
            set
            {
                if (icon != value)
                {
                    icon = value;
                    NotifyPropertyChanged("Icon");
                }
            }
        }
        [DataMember]
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged("Name");
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
        public int Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    if (value == 1)
                    {
                        ((MainWindow)System.Windows.Application.Current.MainWindow).Notifications.Insert(0, new Notification { Owner = "Friends", Title = Name + " is online!", Date = DateTime.Now.ToString(),Icon= "pack://application:,,,/Launcher/friends.png" });
                        ((MainWindow)System.Windows.Application.Current.MainWindow).NotifyPropertyChanged("NotificationCount");
                    }
                    status = value;
                    NotifyPropertyChanged("Status");
                }
            }
        }
    }

    public class Preview
    {
        public string small { get; set; }
        public string medium { get; set; }
        public string large { get; set; }
        public string template { get; set; }
    }

    public class Links
    {
        public string self { get; set; }
        public string follows { get; set; }
        public string commercial { get; set; }
        public string stream_key { get; set; }
        public string chat { get; set; }
        public string features { get; set; }
        public string subscriptions { get; set; }
        public string editors { get; set; }
        public string teams { get; set; }
        public string videos { get; set; }
    }

    public class Channel
    {
        public bool mature { get; set; }
        public bool partner { get; set; }
        public string status { get; set; }
        public string broadcaster_language { get; set; }
        public string display_name { get; set; }
        public string game { get; set; }
        public string language { get; set; }
        public int _id { get; set; }
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public object delay { get; set; }
        public string logo { get; set; }
        public object banner { get; set; }
        public string video_banner { get; set; }
        public object background { get; set; }
        public string profile_banner { get; set; }
        public string profile_banner_background_color { get; set; }
        public string url { get; set; }
        public int views { get; set; }
        public int followers { get; set; }
        public Links _links { get; set; }
    }

    public class Links2
    {
        public string self { get; set; }
    }

    public class Stream
    {
        public object _id { get; set; }
        public string game { get; set; }
        public int viewers { get; set; }
        public int video_height { get; set; }
        public double average_fps { get; set; }
        public int delay { get; set; }
        public string created_at { get; set; }
        public bool is_playlist { get; set; }
        public string stream_type { get; set; }
        public Preview preview { get; set; }
        public Channel channel { get; set; }
        public Links2 _links { get; set; }
    }

    public class Links3
    {
        public string self { get; set; }
        public string next { get; set; }
        public string featured { get; set; }
        public string summary { get; set; }
        public string followed { get; set; }
    }

    public class RootObject
    {
        public int _total { get; set; }
        public List<Stream> streams { get; set; }
        public Links3 _links { get; set; }
    }
}

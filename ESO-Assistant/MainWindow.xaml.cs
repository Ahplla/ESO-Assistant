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
using AutoUpdaterDotNET;
using ESO_Assistant.Classes;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace ESO_Assistant
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        private static bool IsConnectedToInternet()
        {
            return InternetGetConnectedState(out int Desc, 0);
        }

        private int FThreadCount;
        private string _ESOPop { get; set; }
        public string ESOPop
        {
            get { return _ESOPop; }
            set
            {
                _ESOPop = value;
                NotifyPropertyChanged("ESOPop");
            }
        }
        private string _TADPop { get; set; }
        public string TADPop
        {
            get { return _TADPop; }
            set
            {
                _TADPop = value;
                NotifyPropertyChanged("TADPop");
            }
        }
        private string _NillaPop { get; set; }
        public string NillaPop
        {
            get { return _NillaPop; }
            set
            {
                _NillaPop = value;
                NotifyPropertyChanged("NillaPop");
            }
        }
        private string hESOC { get; set; }
        public string ESOCList
        {
            get { return hESOC; }
            set
            {
                hESOC = value;
                NotifyPropertyChanged("ESOCList");
            }
        }
        private string hESO { get; set; }
        public string ESOHint
        {
            get { return hESO; }
            set
            {
                hESO = value;
                NotifyPropertyChanged("ESOHint");
            }
        }
        private string hInternet { get; set; }
        public string InternetHint
        {
            get { return hInternet; }
            set
            {
                hInternet = value;
                NotifyPropertyChanged("InternetHint");
            }
        }


        private string FMaxPRToolTip { get; set; }
        public string MaxPRToolTip
        {
            get { return FMaxPRToolTip; }
            set
            {
                FMaxPRToolTip = value;
                NotifyPropertyChanged("MaxPRToolTip");
            }
        }

        private string FPRToolTip { get; set; }
        public string PRToolTip
        {
            get { return FPRToolTip; }
            set
            {
                FPRToolTip = value;
                NotifyPropertyChanged("PRToolTip");
            }
        }

        private string FMaxELOToolTip { get; set; }
        public string MaxELOToolTip
        {
            get { return FMaxELOToolTip; }
            set
            {
                FMaxELOToolTip = value;
                NotifyPropertyChanged("MaxELOToolTip");
            }
        }

        private string FELOToolTip { get; set; }
        public string ELOToolTip
        {
            get { return FELOToolTip; }
            set
            {
                FELOToolTip = value;
                NotifyPropertyChanged("ELOToolTip");
            }
        }

        private string FGToolTip { get; set; }
        public string GToolTip
        {
            get { return FGToolTip; }
            set
            {
                FGToolTip = value;
                NotifyPropertyChanged("GToolTip");
            }
        }
        private string FKey { get; set; }
        public string Key
        {
            get { return FKey; }
            set
            {
                FKey = value;
                NotifyPropertyChanged("Key");
            }
        }

        private Brush FKeyColor { get; set; }
        public Brush KeyColor
        {
            get { return FKeyColor; }
            set
            {
                FKeyColor = value;
                NotifyPropertyChanged("KeyColor");
            }
        }

        private bool FKeyEnabled { get; set; }
        public bool KeyEnabled
        {
            get { return FKeyEnabled; }
            set
            {
                FKeyEnabled = value;
                NotifyPropertyChanged("KeyEnabled");
            }
        }

        private string FWPToolTip { get; set; }
        public string WPToolTip
        {
            get { return FWPToolTip; }
            set
            {
                FWPToolTip = value;
                NotifyPropertyChanged("WPToolTip");
            }
        }
        private string FMaxPRImage { get; set; }
        public string MaxPRImage
        {
            get { return FMaxPRImage; }
            set
            {
                FMaxPRImage = value;
                NotifyPropertyChanged("MaxPRImage");
            }
        }


        private double FDesired { get; set; }
        public double Desired
        {
            get { return FDesired; }
            set
            {
                FDesired = value;
                NotifyPropertyChanged("Desired");
                NotifyPropertyChanged("Required");
            }
        }

        private double FCorrection { get; set; }
        public double Correction
        {
            get
            {
                if (FCorrection == 0)
                    return 100;
                return FCorrection;
            }
            set
            {
                FCorrection = value;

                NotifyPropertyChanged("Correction");
                NotifyPropertyChanged("Required");
            }
        }

        public string Required
        {
            get
            {
                if (Wins < Desired)
                    if (Desired < Correction)
                        return Math.Ceiling(Games * (Desired - Wins) /
                         (Correction - Desired)).ToString();
                return "";
            }
        }

        private string FPRImage { get; set; }
        public string PRImage
        {
            get { return FPRImage; }
            set
            {
                FPRImage = value;
                NotifyPropertyChanged("PRImage");
            }
        }

        private string FMaxELOImage { get; set; }
        public string MaxELOImage
        {
            get { return FMaxELOImage; }
            set
            {
                FMaxELOImage = value;
                NotifyPropertyChanged("MaxELOImage");
            }
        }

        private string FELOImage { get; set; }
        public string ELOImage
        {
            get { return FELOImage; }
            set
            {
                FELOImage = value;
                NotifyPropertyChanged("ELOImage");
            }
        }

        private string FGImage { get; set; }
        public string GImage
        {
            get { return FGImage; }
            set
            {
                FGImage = value;
                NotifyPropertyChanged("GImage");
            }
        }

        private string FWPImage { get; set; }
        public string WPImage
        {
            get { return FWPImage; }
            set
            {
                FWPImage = value;
                NotifyPropertyChanged("WPImage");
            }
        }

        private Brush InternetColor { get; set; }
        public Brush Internet
        {
            get { return InternetColor; }
            set
            {
                InternetColor = value;
                NotifyPropertyChanged("Internet");
            }
        }

        private Brush FColorStreak { get; set; }
        public Brush ColorStreak
        {
            get { return FColorStreak; }
            set
            {
                FColorStreak = value;
                NotifyPropertyChanged("ColorStreak");
            }
        }

        private Brush ESOColor { get; set; }
        public Brush ESO
        {
            get { return ESOColor; }
            set
            {
                ESOColor = value;
                NotifyPropertyChanged("ESO");
            }
        }

        private Brush ESOCColor { get; set; }
        public Brush ESOC
        {
            get { return ESOCColor; }
            set
            {
                ESOCColor = value;
                NotifyPropertyChanged("ESOC");
            }
        }

        private int FPT { get; set; }
        public int PT
        {
            get { return FPT; }
            set
            {
                FPT = value;
                NotifyPropertyChanged("PT");
            }
        }

        private int FNB { get; set; }
        public int NB
        {
            get { return FNB; }
            set
            {
                FNB = value;
                NotifyPropertyChanged("NB");
            }
        }

        private string FElapsed { get; set; }
        public string Elapsed
        {
            get { return "Elapsed: " + FElapsed; }
            set
            {
                FElapsed = value;
                NotifyPropertyChanged("Elapsed");
            }
        }

        private string FProgress { get; set; }
        public string Progress
        {
            get { return FProgress; }
            set
            {
                FProgress = value;
                NotifyPropertyChanged("Progress");
            }
        }
        private string FPTStatus { get; set; }
        public string PTStatus
        {
            get { return "Point Traider: " + FPTStatus + Environment.NewLine; }
            set
            {
                FPTStatus = value;
                NotifyPropertyChanged("PTStatus");
            }
        }

        private string FNBStatus { get; set; }
        public string NBStatus
        {
            get { return "Noob Basher: " + FNBStatus + Environment.NewLine; }
            set
            {
                FNBStatus = value;
                NotifyPropertyChanged("NBStatus");
            }
        }


        private string FiCheat { get; set; }
        public string iCheat
        {
            get { return "pack://application:,,,/Cheats/" + FiCheat + ".png"; }
            set
            {
                FiCheat = value;
                NotifyPropertyChanged("iCheat");
            }
        }



        private string FCheatUnitsSP { get; set; }
        public string CheatUnitsSP
        {
            get { return FCheatUnitsSP; }
            set
            {
                FCheatUnitsSP = value;
                NotifyPropertyChanged("CheatUnitsSP");
            }
        }

        private string FCheatUnitsTR { get; set; }
        public string CheatUnitsTR
        {
            get { return FCheatUnitsTR; }
            set
            {
                FCheatUnitsTR = value;
                NotifyPropertyChanged("CheatUnitsTR");
            }
        }

        private string FCheatUnitsDM { get; set; }
        public string CheatUnitsDM
        {
            get { return FCheatUnitsDM; }
            set
            {
                FCheatUnitsDM = value;
                NotifyPropertyChanged("CheatUnitsDM");
            }
        }

        private string FCheatUnitsSPn { get; set; }
        public string CheatUnitsSPn
        {
            get { return FCheatUnitsSPn; }
            set
            {
                FCheatUnitsSPn = value;
                NotifyPropertyChanged("CheatUnitsSPn");
            }
        }

        private string FCheatUnitsDMn { get; set; }
        public string CheatUnitsDMn
        {
            get { return FCheatUnitsDMn; }
            set
            {
                FCheatUnitsDMn = value;
                NotifyPropertyChanged("CheatUnitsDMn");
            }
        }





        private Brush FCheatColorSP { get; set; }
        public Brush CheatColorSP
        {
            get { return FCheatColorSP; }
            set
            {
                FCheatColorSP = value;
                NotifyPropertyChanged("CheatColorSP");
            }
        }

        private Brush FCheatColorTR { get; set; }
        public Brush CheatColorTR
        {
            get { return FCheatColorTR; }
            set
            {
                FCheatColorTR = value;
                NotifyPropertyChanged("CheatColorTR");
            }
        }

        private Brush FCheatColorDM { get; set; }
        public Brush CheatColorDM
        {
            get { return FCheatColorDM; }
            set
            {
                FCheatColorDM = value;
                NotifyPropertyChanged("CheatColorDM");
            }
        }

        private Brush FCheatColorSPn { get; set; }
        public Brush CheatColorSPn
        {
            get { return FCheatColorSPn; }
            set
            {
                FCheatColorSPn = value;
                NotifyPropertyChanged("CheatColorSPn");
            }
        }

        private Brush FCheatColorDMn { get; set; }
        public Brush CheatColorDMn
        {
            get { return FCheatColorDMn; }
            set
            {
                FCheatColorDMn = value;
                NotifyPropertyChanged("CheatColorDMn");
            }
        }








        private string FCheatMsgSP { get; set; }
        public string CheatMsgSP
        {
            get { return FCheatMsgSP; }
            set
            {
                FCheatMsgSP = value;
                NotifyPropertyChanged("CheatMsgSP");
            }
        }

        private string FCheatMsgTR { get; set; }
        public string CheatMsgTR
        {
            get { return FCheatMsgTR; }
            set
            {
                FCheatMsgTR = value;
                NotifyPropertyChanged("CheatMsgTR");
            }
        }

        private string FCheatMsgDM { get; set; }
        public string CheatMsgDM
        {
            get { return FCheatMsgDM; }
            set
            {
                FCheatMsgDM = value;
                NotifyPropertyChanged("CheatMsgDM");
            }
        }

        private string FCheatMsgSPn { get; set; }
        public string CheatMsgSPn
        {
            get { return FCheatMsgSPn; }
            set
            {
                FCheatMsgSPn = value;
                NotifyPropertyChanged("CheatMsgSPn");
            }
        }

        private string FCheatMsgDMn { get; set; }
        public string CheatMsgDMn
        {
            get { return FCheatMsgDMn; }
            set
            {
                FCheatMsgDMn = value;
                NotifyPropertyChanged("CheatMsgDMn");
            }
        }




        private string FPTInfo { get; set; }
        public string PTInfo
        {
            get { return FPTInfo; }
            set
            {
                FPTInfo = value;
                NotifyPropertyChanged("PTInfo");
            }
        }

        private string FNBInfo { get; set; }
        public string NBInfo
        {
            get { return FNBInfo; }
            set
            {
                FNBInfo = value;
                NotifyPropertyChanged("NBInfo");
            }
        }

        private Visibility FAllVisible { get; set; }
        public Visibility AllVisible
        {
            get { return FAllVisible; }
            set
            {
                FAllVisible = value;
                NotifyPropertyChanged("AllVisible");
            }
        }

        private string FLastLogin { get; set; }
        private string FLastUpdate { get; set; }
        public string LastLogin
        {
            get { return "Last login: " + FLastLogin; }
            set
            {
                FLastLogin = value;
                NotifyPropertyChanged("LastLogin");
            }
        }


        private string FCheater { get; set; }
        public string Cheater
        {
            get { return FCheater; }
            set
            {
                FCheater = value;
                NotifyPropertyChanged("Cheater");
            }
        }

        private string FTwitchVisibility { get; set; }
        public string TwitchVisibility
        {
            get { return FTwitchVisibility; }
            set
            {
                FTwitchVisibility = value;
                NotifyPropertyChanged("TwitchVisibility");
            }
        }



        public string LastUpdate
        {
            get { return "Last update: " + FLastUpdate; }
            set
            {
                FLastUpdate = value;
                NotifyPropertyChanged("LastUpdate");
            }
        }
        private string FWinPercent { get; set; }
        public string WinPercent
        {
            get { return FWinPercent + " %"; }
            set
            {
                FWinPercent = value;
                NotifyPropertyChanged("Wins");

                NotifyPropertyChanged("WinPercent");
                NotifyPropertyChanged("Required");
            }
        }

        public double Wins
        {
            get
            {
                if (string.IsNullOrEmpty(FWinPercent)) return 0;
                return double.Parse(FWinPercent);
            }
        }

        private int FGames { get; set; }
        public int Games
        {
            get { return FGames; }
            set
            {
                FGames = value;

                NotifyPropertyChanged("Games");
                NotifyPropertyChanged("Required");
            }
        }


        private bool FLastLoginEnabled { get; set; }
        public bool LastLoginEnabled
        {
            get { return FLastLoginEnabled; }
            set
            {
                FLastLoginEnabled = value;
                NotifyPropertyChanged("LastLoginEnabled");
            }
        }

        private bool FClanEnabled { get; set; }
        public bool ClanEnabled
        {
            get { return FClanEnabled; }
            set
            {
                FClanEnabled = value;
                NotifyPropertyChanged("ClanEnabled");
            }
        }



        private string FClanName { get; set; }
        public string ClanName
        {
            get { return "Description: " + FClanName; }
            set
            {
                FClanName = value;
                NotifyPropertyChanged("ClanName");
            }
        }
        private string FClanTag { get; set; }
        public string ClanTag
        {
            get { return "Tag: " + FClanTag; }
            set
            {
                FClanTag = value;
                NotifyPropertyChanged("ClanTag");
            }
        }
        private string FClanOwner { get; set; }
        public string ClanOwner
        {
            get { return "Leader: " + FClanOwner; }
            set
            {
                FClanOwner = value;
                NotifyPropertyChanged("ClanOwner");
            }
        }
        private string FClanDate { get; set; }
        public string ClanDate
        {
            get { return "Date Created: " + FClanDate; }
            set
            {
                FClanDate = value;
                NotifyPropertyChanged("ClanDate");
            }
        }


        private bool FSPEnabled { get; set; }
        public bool SPEnabled
        {
            get { return FSPEnabled; }
            set
            {
                FSPEnabled = value;
                NotifyPropertyChanged("SPEnabled");
            }
        }

        private bool FTREnabled { get; set; }
        public bool TREnabled
        {
            get { return FTREnabled; }
            set
            {
                FTREnabled = value;
                NotifyPropertyChanged("TREnabled");
            }
        }

        private bool FDMEnabled { get; set; }
        public bool DMEnabled
        {
            get { return FDMEnabled; }
            set
            {
                FDMEnabled = value;
                NotifyPropertyChanged("DMEnabled");
            }
        }

        private bool FSPnEnabled { get; set; }
        public bool SPnEnabled
        {
            get { return FSPnEnabled; }
            set
            {
                FSPnEnabled = value;
                NotifyPropertyChanged("SPnEnabled");
            }
        }

        private bool FDMnEnabled { get; set; }
        public bool DMnEnabled
        {
            get { return FDMnEnabled; }
            set
            {
                FDMnEnabled = value;
                NotifyPropertyChanged("DMnEnabled");
            }
        }




        private string FStreak { get; set; }
        public string Streak
        {
            get { return FStreak; }
            set
            {
                FStreak = value;
                NotifyPropertyChanged("Streak");
            }
        }

        private string FMaxStreak { get; set; }
        public string MaxStreak
        {
            get { return FMaxStreak; }
            set
            {
                FMaxStreak = value;
                NotifyPropertyChanged("MaxStreak");
            }
        }

        private string FInfo { get; set; }
        public string Info
        {
            get { return FInfo; }
            set
            {
                FInfo = value;
                NotifyPropertyChanged("Info");
            }
        }

        private double FPR { get; set; }
        public double PR
        {
            get { return FPR; }
            set
            {
                FPR = value;
                NotifyPropertyChanged("PR");
            }
        }

        private double FMaxPR { get; set; }
        public double MaxPR
        {
            get { return FMaxPR; }
            set
            {
                FMaxPR = value;
                NotifyPropertyChanged("MaxPR");
            }
        }

        private double FELO { get; set; }
        public double ELO
        {
            get { return FELO; }
            set
            {
                FELO = value;
                NotifyPropertyChanged("ELO");
            }
        }

        private double FMaxELO { get; set; }
        public double MaxELO
        {
            get { return FMaxELO; }
            set
            {
                FMaxELO = value;
                NotifyPropertyChanged("MaxELO");
            }
        }


        private double FWonP { get; set; }

        public double WonP
        {
            get { return FWonP; }
            set
            {
                FWonP = value;
                NotifyPropertyChanged("WonP");
                NotifyPropertyChanged("WonProgress");
            }
        }


        private double FUsedP { get; set; }

        public double UsedP
        {
            get { return FUsedP; }
            set
            {
                FUsedP = value;
                NotifyPropertyChanged("UsedP");
                NotifyPropertyChanged("UsedProgress");
            }
        }
        public string WonProgress
        {
            get { return "Won: " + FWonP + " %"; }
        }

        public string UsedProgress
        {
            get { return "Used: " + FUsedP + " %"; }
        }


        private long FOverallGames { get; set; }
        public long OverallGames
        {
            get { return FOverallGames; }
            set
            {
                FOverallGames = value;
                NotifyPropertyChanged("OverallGames");
            }
        }


        private string FRank { get; set; }
        public string Rank
        {
            get { return FRank; }
            set
            {
                FRank = value;
                NotifyPropertyChanged("Rank");
            }
        }

        private string FNickForAdding { get; set; }
        public string NickForAdding
        {
            get { return FNickForAdding; }
            set
            {
                FNickForAdding = value;
                NotifyPropertyChanged("NickForAdding");
            }
        }

        private string FCiv { get; set; }
        public string Civ
        {
            get { return FCiv; }
            set
            {
                FCiv = value;
                NotifyPropertyChanged("Civ");
            }
        }

        private ObservableCollection<ChartItem> FWonPie = new ObservableCollection<ChartItem>();

        public ObservableCollection<ChartItem> WonPie
        {
            get { return FWonPie; }
            set
            {
                FWonPie = value;
                NotifyPropertyChanged("WonPie");
            }
        }

        private ObservableCollection<ChartItem> FUsedPie = new ObservableCollection<ChartItem>();

        public ObservableCollection<ChartItem> UsedPie
        {
            get { return FUsedPie; }
            set
            {
                FUsedPie = value;
                NotifyPropertyChanged("UsedPie");
            }
        }

        private Color FOnline { get; set; }
        public Color Online
        {
            get { return FOnline; }
            set
            {
                FOnline = value;
                NotifyPropertyChanged("Online");
            }
        }
        private string FClan { get; set; }
        public string Clan
        {
            get { return FClan; }
            set
            {
                FClan = value;
                NotifyPropertyChanged("Clan");
            }
        }

        private string FFC1 { get; set; }
        public string FC1
        {
            get { return FFC1; }
            set
            {
                FFC1 = value;
                NotifyPropertyChanged("FC1");
            }
        }

        private string FFC2 { get; set; }
        public string FC2
        {
            get { return FFC2; }
            set
            {
                FFC2 = value;
                NotifyPropertyChanged("FC2");
            }
        }

        private string FFC3 { get; set; }
        public string FC3
        {
            get { return FFC3; }
            set
            {
                FFC3 = value;
                NotifyPropertyChanged("FC3");
            }
        }

        private string FClanDesc { get; set; }
        public string ClanDesc
        {
            get { return FClanDesc; }
            set
            {
                FClanDesc = value;
                NotifyPropertyChanged("ClanDesc");
            }
        }
        private string FAvatar { get; set; }
        public string Avatar
        {
            get { return FAvatar; }
            set
            {
                FAvatar = value;
                NotifyPropertyChanged("Avatar");
            }
        }

        private WPF.Common.VisibilityAnimation.AnimationType FAType { get; set; }
        public WPF.Common.VisibilityAnimation.AnimationType AType
        {
            get { return FAType; }
            set
            {
                FAType = value;
                NotifyPropertyChanged("AType");
            }
        }

        public int ELO1
        {
            get { int e1 = 0;
                foreach (ELOItem e in Initial1)
                    e1 += e.ELOValue;
                return e1;
            }
        }

        public int ELO2
        {
            get
            {
                int e2 = 0;
                foreach (ELOItem e in Initial2)
                    e2 += e.ELOValue;
                return e2;
            }
        }
        public int CalcELO(int W,int L,int N)
        {
        return  Math.Max(Math.Min((int)Math.Round(16 + (L - W) * 2 / (double)N * 16 / 400), 31), 1);
        }




        private Stopwatch stopWatch = new Stopwatch();

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand ?? (deleteCommand = new RelayCommand(param => this.DeleteItem(), null)); }
        }

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get { return addCommand ?? (addCommand = new RelayCommand(param => this.AddItem(), null)); }
        }

        UserStatus US = new UserStatus("");
        Stat SP = new Stat();
        Stat TR = new Stat();
        Stat DM = new Stat();
        Stat SPn = new Stat();
        Stat DMn = new Stat();

        CheatDetector CheatSP = new CheatDetector();
        CheatDetector CheatTR = new CheatDetector();
        CheatDetector CheatDM = new CheatDetector();
        CheatDetector CheatSPn = new CheatDetector();
        CheatDetector CheatDMn = new CheatDetector();



        string FriendPath;
        private ObservableCollection<ClanMember> FClanList = new ObservableCollection<ClanMember>();

        public ObservableCollection<ClanMember> ClanList
        {
            get { return FClanList; }
            set
            {
                FClanList = value;
                NotifyPropertyChanged("ClanList");
            }
        }
        List<Game> GamesStream = new List<Game>();
        private ObservableCollection<ListItem> Friends = new ObservableCollection<ListItem>();
        private ObservableCollection<ELOItem> Initial1 = new ObservableCollection<ELOItem>();
        private ObservableCollection<ELOItem> Initial2 = new ObservableCollection<ELOItem>();
        private ListItem selectedItem;
        public ListItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    NotifyPropertyChanged("SelectedItem");
                }
            }
        }
        public void DeleteItem()
        {
            if (SelectedItem != null)
                Friends.Remove(SelectedItem);
        }
        public void AddItem()
        {
            if (FriendsIndexOf(NickForAdding) == -1 && !string.IsNullOrEmpty(NickForAdding))
            {
                Friends.Add(new ListItem { Name = NickForAdding, Status = 0 });
                NickForAdding = "";
            }

        }


        private string FFC1ToolTip { get; set; }
        public string FC1ToolTip
        {
            get { return FFC1ToolTip; }
            set
            {
                FFC1ToolTip = value;
                NotifyPropertyChanged("FC1ToolTip");
            }
        }

        private string FFC2ToolTip { get; set; }
        public string FC2ToolTip
        {
            get { return FFC2ToolTip; }
            set
            {
                FFC2ToolTip = value;
                NotifyPropertyChanged("FC2ToolTip");
            }
        }

        private string FFC3ToolTip { get; set; }
        public string FC3ToolTip
        {
            get { return FFC3ToolTip; }
            set
            {
                FFC3ToolTip = value;
                NotifyPropertyChanged("FC3ToolTip");
            }
        }

        public void LoadItem()
        {
            if (SelectedItem != null && tbESO.IsEnabled)
            {
                tbESO.Text = SelectedItem.Name;
                bGet.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }
        private DispatcherTimer TwitchTADTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(1000)
        };
        private DispatcherTimer TwitchNillaTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(1000)
        };
        private DispatcherTimer TwitchAOEOTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(1000)
        };
        private DispatcherTimer TwitchAOEIIHDTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(1000)
        };
        private DispatcherTimer TwitchAOEIIConqTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(1000)
        };
        private DispatcherTimer UpdaterTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(1000)
        };
        private DispatcherTimer PopTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };
        private DispatcherTimer FriendsTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };
        private DispatcherTimer InternetTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };
        private DispatcherTimer ESOTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };
        private DispatcherTimer ESOCTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };

        private string AppVer = "Release: 5.0";

        public MainWindow()
        {

            FriendPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ESO-Assistant");
            AType = WPF.Common.VisibilityAnimation.AnimationType.None;
            InitializeComponent();
            // AllVisible = Visibility.Hidden;

            ToolTipService.ShowDurationProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(Int32.MaxValue));
            ToolTipService.InitialShowDelayProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(0));
            NickForAdding = "";
            ESOCList = "ESOC Users online: ---";
            InternetHint = "Checking Internet connection...";
            ESOHint = "Checking ESO servers...";
            ESOPop = "ESO: ---";
            TADPop = "TAD: ---";
            NillaPop = "Nilla: ---";
            Internet = Brushes.Yellow;
            ESOC = Brushes.Yellow;
            ESO = Brushes.Yellow;
            GamesStream.Add(new Game() { Name = "AoE III: TAD", Icon = "pack://application:,,,/Twitch/iii.png" });
            GamesStream.Add(new Game() { Name = "AoE III", Icon = "pack://application:,,,/Twitch/iii.png" });
            GamesStream.Add(new Game() { Name = "AoE Online", Icon = "pack://application:,,,/Twitch/o.png" });
            GamesStream.Add(new Game() { Name = "AoE II: HD Edition", Icon = "pack://application:,,,/Twitch/ii.png" });
            GamesStream.Add(new Game() { Name = "AoE II: The Conqs", Icon = "pack://application:,,,/Twitch/ii.png" });

            if (!Directory.Exists(FriendPath))

                Directory.CreateDirectory(FriendPath);
            if (File.Exists(Path.Combine(FriendPath, "FriendList.json")))
            {
                string json = File.ReadAllText(Path.Combine(FriendPath, "FriendList.json"));
                Friends = JsonConvert.DeserializeObject<ObservableCollection<ListItem>>(json);
            }
            else
                Friends = new ObservableCollection<ListItem>();
            //ClanList = new ObservableCollection<ClanMember>();
            //   ClanList.Add(new ClanMember { Name = "sdsdsd", ID = 2 });
            listView1.ItemsSource = Friends;
            dgInitial1.ItemsSource = Initial1;
            dgChange11.ItemsSource = Initial1;
            dgChange21.ItemsSource = Initial1;
            dgInitial2.ItemsSource = Initial2;
            dgChange12.ItemsSource = Initial2;
            dgChange22.ItemsSource = Initial2;
            lwClan.ItemsSource = ClanList;
            treeView1.ItemsSource = GamesStream;
            DataContext = this;
            InternetTimer.Tick += InternetTimer_Tick;
            InternetTimer.Start();
            ESOTimer.Tick += ESOTimer_Tick;
            ESOTimer.Start();
            ESOCTimer.Tick += ESOCTimer_Tick;
            ESOCTimer.Start();
            PopTimer.Tick += PopTimer_Tick;
            PopTimer.Start();
            FriendsTimer.Tick += FriendsTimer_Tick;
            FriendsTimer.Start();

            TwitchTADTimer.Tick += TwitchTADTimer_Tick;
            TwitchTADTimer.Start();
            TwitchNillaTimer.Tick += TwitchNillaTimer_Tick;
            TwitchNillaTimer.Start();
            TwitchAOEOTimer.Tick += TwitchAOEOTimer_Tick;
            TwitchAOEOTimer.Start();
            TwitchAOEIIHDTimer.Tick += TwitchAOEIIHDTimer_Tick;
            TwitchAOEIIHDTimer.Start();
            TwitchAOEIIConqTimer.Tick += TwitchAOEIIConqTimer_Tick;
            TwitchAOEIIConqTimer.Start();
            AutoUpdater.OpenDownloadPage = true;
            UpdaterTimer.Tick += delegate (object sender, EventArgs args)
            {
                UpdaterTimer.Interval = TimeSpan.FromMilliseconds(2 * 60 * 1000);
                AutoUpdater.Start("http://eso-assistant.ucoz.net/UpdateInfo.xml");
            };
            //UpdaterTimer.Start();
            CheckFriendSettings();
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(listView1.Items);
            collectionView.SortDescriptions.Add(new SortDescription("Status", ListSortDirection.Descending));
            collectionView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            var view = (ICollectionViewLiveShaping)CollectionViewSource.GetDefaultView(listView1.Items);
            view.IsLiveSorting = true;
            GetIP();
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

        private string CheckCheatGroup(string Nick)
        {

            for (int i = 0; i < Constant.ACCOUNTTHEFT_LIST.Length; i++)
                if (Constant.ACCOUNTTHEFT_LIST[i].ToLower() == Nick.ToLower())
                    return Constant.ACCOUNTTHEFT_LIST[i] + " is blacklisted!" + Environment.NewLine + "Reason: " +
                      "Account Theft";
            for (int i = 0; i < Constant.MOESBAR_LIST.Length; i++)
                if (Constant.MOESBAR_LIST[i].ToLower() == Nick.ToLower())
                    return Constant.MOESBAR_LIST[i] + " is blacklisted!" + Environment.NewLine + "Reason: " +
    "Moesbar";
            for (int i = 0; i < Constant.DROP_LIST.Length; i++)
                if (Constant.DROP_LIST[i].ToLower() == Nick.ToLower())
                    return Constant.DROP_LIST[i] + " is blacklisted!" + Environment.NewLine + "Reason: " +
    "Drop trick / OOS";

            for (int i = 0; i < Constant.SP_TRADERS_LIST.Length; i++)
                if (Constant.SP_TRADERS_LIST[i].ToLower() == Nick.ToLower())
                    return Constant.SP_TRADERS_LIST[i] + " is blacklisted!" + Environment.NewLine + "Reason: " +
    "Point Trader : Supremacy";

            for (int i = 0; i < Constant.TR_TRADERS_LIST.Length; i++)
                if (Constant.TR_TRADERS_LIST[i].ToLower() == Nick.ToLower())
                    return Constant.TR_TRADERS_LIST[i] + " is blacklisted!" + Environment.NewLine + "Reason: " +
    "Point Trader : Treaty";

            for (int i = 0; i < Constant.DM_TRADERS_LIST.Length; i++)
                if (Constant.DM_TRADERS_LIST[i].ToLower() == Nick.ToLower())
                    return Constant.DM_TRADERS_LIST[i] + " is blacklisted!" + Environment.NewLine + "Reason: " +
    "Point Trader : Deathmatch";

            for (int i = 0; i < Constant.SPn_TRADERS_LIST.Length; i++)
                if (Constant.SPn_TRADERS_LIST[i].ToLower() == Nick.ToLower())
                    return Constant.SPn_TRADERS_LIST[i] + " is blacklisted!" + Environment.NewLine + "Reason: " +
    "Point Trader : Supremacy nilla";

            for (int i = 0; i < Constant.DMn_TRADERS_LIST.Length; i++)
                if (Constant.DMn_TRADERS_LIST[i].ToLower() == Nick.ToLower())
                    return Constant.DMn_TRADERS_LIST[i] + " is blacklisted!" + Environment.NewLine + "Reason: " +
    "Point Trader : Deahmatch nilla";

            for (int i = 0; i < Constant.SP_BASHERS_LIST.Length; i++)
                if (Constant.SP_BASHERS_LIST[i].ToLower() == Nick.ToLower())
                    return Constant.SP_BASHERS_LIST[i] + " is blacklisted!" + Environment.NewLine + "Reason: " +
    "Noobbashers : Supremacy";

            for (int i = 0; i < Constant.TR_BASHERS_LIST.Length; i++)
                if (Constant.TR_BASHERS_LIST[i].ToLower() == Nick.ToLower())
                    return Constant.TR_BASHERS_LIST[i] + " is blacklisted!" + Environment.NewLine + "Reason: " +
        "Noobbashers : Treaty";

            for (int i = 0; i < Constant.DM_BASHERS_LIST.Length; i++)
                if (Constant.DM_BASHERS_LIST[i].ToLower() == Nick.ToLower())
                    return Constant.DM_BASHERS_LIST[i] + " is blacklisted!" + Environment.NewLine + "Reason: " +
    "Noobbashers : Deathmatch";

            for (int i = 0; i < Constant.SPn_BASHERS_LIST.Length; i++)
                if (Constant.SPn_BASHERS_LIST[i].ToLower() == Nick.ToLower())
                    return Constant.SPn_BASHERS_LIST[i] + " is blacklisted!" + Environment.NewLine + "Reason: " +
    "Noobbashers : Supremacy nilla";

            for (int i = 0; i < Constant.DMn_BASHERS_LIST.Length; i++)
                if (Constant.DMn_BASHERS_LIST[i].ToLower() == Nick.ToLower())
                    return Constant.DMn_BASHERS_LIST[i] + " is blacklisted!" + Environment.NewLine + "Reason: " +
    "Noobbashers : Deathmatch nilla";
            return "";
        }

        private void InternetTimer_Tick(object sender, EventArgs e)
        {
            InternetTimer.Stop();
            InternetTimer.Interval = TimeSpan.FromMilliseconds(10000);
            if (IsConnectedToInternet())
            {
                InternetHint = "Connected!";
                Internet = Brushes.LimeGreen;
            }
            else
            {
                InternetHint = "No Internet connection!";
                Internet = Brushes.Red;
            }
            InternetTimer.Start();
        }
        private string GetSpace(string S, int Count)
        {
            string Result = S;
            for (int i = 0; i < Count; i++)
                Result = Result + " ";
            return Result;
        }
        async void GetPopInfo(string URI)
        {
            try
            {
                string Data = await HttpGetAsync(URI);
                ServerStatus SS = new ServerStatus();
                SS.Get(Data);
                ESOPop = "ESO: " + SS.ESOPopulation;
                TADPop = "TAD: " + SS.TADPopulation;
                NillaPop = "Nilla: " + SS.NillaPopulation;
            }
            catch
            {
                ESOPop = "ESO: ---";
                TADPop = "TAD: ---";
                NillaPop = "Nilla: ---";
            }
            PopTimer.Start();
        }
        async void GetESOCInfo(string URI)
        {
            try
            {
                string Data = await HttpGetAsync(URI);
                ESOCOnline EO = new ESOCOnline();

                EO.Get(Data);
                List<string> H = new List<string>();
                int MaxU = 0;
                int MaxI = EO.Users.Count.ToString().Length;
                if (EO.UserOnline > 0)
                {
                    for (int i = 0; i < EO.Users.Count; i++)
                        MaxU = Math.Max(MaxU, EO.Users[i].Length);
                    for (int i = 0; i < EO.Users.Count; i++)
                        H.Add(GetSpace((i + 1).ToString(), MaxI - (i + 1).ToString().Length + 2) + GetSpace(EO.Users[i], MaxU - EO.Users[i].Length + 2) + EO.Actions[i]);
                    if (H.Count > 0)
                        ESOCList = string.Join(Environment.NewLine, H.ToArray());
                    else
                        ESOCList = "ESOC Users online: 0";
                }
                ESOC = Brushes.LimeGreen;
            }
            catch
            {
                ESOC = Brushes.Red;
                ESOCList = "ESOC Users online: ---";
            }
            ESOCTimer.Start();
        }
        private int FriendsIndexOf(string Name)
        {

            for (int i = 0; i < Friends.Count; i++)
                if (Name.ToLower() == Friends[i].Name.ToLower())
                    return i;
            return -1;
        }
        async void GetFriendInfo(string Name)
        {

            try
            {

                string Data = await HttpGetAsync("http://www.agecommunity.com/query/query.aspx?md=user&name=" + WebUtility.UrlEncode(Name));
                UserStatus US = new UserStatus(Name);
                US.Get(Data);


                int Index = FriendsIndexOf(Name);
                if (Index != -1)
                {

                    if (US.Status != -1)
                    {
                        Friends[Index].Name = US.Nick;
                        Friends[Index].Status = US.Status;
                        Friends[Index].Icon = US.Icon;
                        CheckFriendSettings();
                        List<string> A = new List<string>();
                        if (LastLoginEnabled && US.LastLogin != "")
                            A.Add("Last login: " + US.LastLogin);
                        if (ClanEnabled && US.Clan != "")
                            A.Add("Clan: " + US.Clan);
                        if (SPEnabled && US.PRYS != "")
                            A.Add(US.PRYS);
                        if (TREnabled && US.PRYT != "")
                            A.Add(US.PRYT);
                        if (DMEnabled && US.PRYD != "")
                            A.Add(US.PRYD);
                        if (SPnEnabled && US.PRS != "")
                            A.Add(US.PRS);
                        if (DMnEnabled && US.PRD != "")
                            A.Add(US.PRD);
                        Friends[Index].Hint = string.Join(Environment.NewLine, A.ToArray());
                    }
                    else
                    {
                        Friends.RemoveAt(Index);
                    }

                }
            }
            catch { }
            FThreadCount--;
            if (FThreadCount == 0)
                FriendsTimer.Start();
        }
        private void FriendsTimer_Tick(object sender, EventArgs e)
        {
            FriendsTimer.Stop();
            FriendsTimer.Interval = TimeSpan.FromMilliseconds(5000);
            FThreadCount = Friends.Count;
            if (Friends.Count == 0)
                FriendsTimer.Start();
            else
                for (int i = 0; i < Friends.Count; i++)
                    GetFriendInfo(Friends[i].Name);
        }
        private void ESOCTimer_Tick(object sender, EventArgs e)
        {
            ESOCTimer.Stop();
            ESOCTimer.Interval = TimeSpan.FromMilliseconds(10000);
            GetESOCInfo("http://eso-community.net/viewonline.php");
        }

        private void PopTimer_Tick(object sender, EventArgs e)
        {
            PopTimer.Stop();
            PopTimer.Interval = TimeSpan.FromMilliseconds(10000);
            GetPopInfo("http://www.agecommunity.com/_server_status_/");
        }


        private async void ESOTimer_Tick(object sender, EventArgs e)
        {
            ESOTimer.Stop();
            ESOTimer.Interval = TimeSpan.FromMilliseconds(10000);
            try
            {

                bool x = await ServerStatus.checkESOAsync();


                if (x)
                {
                    ESOHint = "Status: ESO Servers are online." + Environment.NewLine + "Last update: " + DateTime.Now.ToString();
                    ESO = Brushes.LimeGreen;
                }
                else
                {
                    ESOHint = "Status: ESO Servers are offline." + Environment.NewLine + "Possible cause: Maintenance of servers."
+ Environment.NewLine + "Last update: " + DateTime.Now.ToString();
                    ESO = Brushes.Red;
                }



            }
            catch
            {
                ESOHint = "Status: ESO Servers are offline." + Environment.NewLine + "Possible cause: Maintenance of servers."
                    + Environment.NewLine + "Last update: " + DateTime.Now.ToString();
                ESO = Brushes.Red;
            }
            ESOTimer.Start();
        }
        async Task GetUserInfo(string URI)
        {
            try
            {
                string Data = await HttpGetAsync(URI);

                US.Get(Data);
                Avatar = US.Avatar;
                Clan = US.Clan;
                ClanDesc = US.ClanDescription;
                Online = US.Online;
                LastLogin = US.LastLogin;
                LastUpdate = US.LastUpdate;

                Progress = "Loaded General Info [" + stopWatch.Elapsed.ToString(@"s\.ff") + " secs]...";
            }
            catch { }
        }
        private void BindInfoOverall(Stat S)
        {
            FC1ToolTip = S.FavCivs.fcOverall.p1;
            FC2ToolTip = S.FavCivs.fcOverall.p2;
            FC3ToolTip = S.FavCivs.fcOverall.p3;


            Info = "Last login: " + US.LastLogin + Environment.NewLine +
"Last online status update: " + US.LastUpdate + Environment.NewLine + "Last stats update: " +
S.LastUpdate + Environment.NewLine + "Last game: " + S.LastGame;
            if (S.RankInfo != "")
                Info += Environment.NewLine + S.RankInfo;
            PT = S.RatioPT;
            NB = S.RatioNB;
            PTInfo = "For the last " + S.LastBattles.ToString() + " games" + Environment.NewLine + "Point trading progress: " +
                S.RatioPT.ToString() + " %";
            NBInfo = "For the last " + S.LastBattles.ToString() + " games" + Environment.NewLine + "Noob Basher progress: " +
    S.RatioNB.ToString() + " %";

            PTStatus = GetRatingsCheaterStatus(S.RatioPT, S.LastBattles);
            NBStatus = GetRatingsCheaterStatus(S.RatioNB, S.LastBattles);

            if (S.StoredELO.eOverall > 0)
            {
                ELOImage = "pack://application:,,,/Icons/Up.png";
                ELOToolTip = "+" + S.StoredELO.eOverall.ToString();
            }
            else
                 if (S.StoredELO.eOverall < 0)
            {
                ELOImage = "pack://application:,,,/Icons/Down.png";
                ELOToolTip = S.StoredELO.eOverall.ToString();
            }
            else
            {
                ELOImage = "pack://application:,,,/Icons/Empty.png";
                ELOToolTip = "";
            }


            if (S.StoredMaxELO.eOverall > 0)
            {
                MaxELOImage = "pack://application:,,,/Icons/Up.png";
                MaxELOToolTip = "+" + S.StoredMaxELO.eOverall.ToString();
            }
            else
                 if (S.StoredMaxELO.eOverall < 0)
            {
                MaxELOImage = "pack://application:,,,/Icons/Down.png";
                MaxELOToolTip = S.StoredMaxELO.eOverall.ToString();
            }
            else
            {
                MaxELOImage = "pack://application:,,,/Icons/Empty.png";
                MaxELOToolTip = "";
            }

            if (S.StoredGames.gOverall > 0)
            {
                GImage = "pack://application:,,,/Icons/Up.png";
                GToolTip = "+" + S.StoredGames.gOverall.ToString();
            }
            else
      if (S.StoredGames.gOverall < 0)
            {
                GImage = "pack://application:,,,/Icons/Down.png";
                GToolTip = S.StoredGames.gOverall.ToString();
            }
            else
            {
                GImage = "pack://application:,,,/Icons/Empty.png";
                GToolTip = "";
            }

            if (S.StoredWinPercent.wpOverall > 0)
            {
                WPImage = "pack://application:,,,/Icons/Up.png";
                WPToolTip = "+" + S.StoredWinPercent.wpOverall.ToString();
            }
            else
     if (S.StoredWinPercent.wpOverall < 0)
            {
                WPImage = "pack://application:,,,/Icons/Down.png";
                WPToolTip = S.StoredWinPercent.wpOverall.ToString();
            }
            else
            {
                WPImage = "pack://application:,,,/Icons/Empty.png";
                WPToolTip = "";
            }


            if (S.StoredPR > 0)
            {
                PRImage = "pack://application:,,,/Icons/Up.png";
                PRToolTip = "+" + S.StoredPR.ToString();
            }
            else
    if (S.StoredPR < 0)
            {
                PRImage = "pack://application:,,,/Icons/Down.png";
                PRToolTip = S.StoredPR.ToString();
            }
            else
            {
                PRImage = "pack://application:,,,/Icons/Empty.png";
                PRToolTip = "";
            }

            if (S.StoredMaxPR > 0)
            {
                MaxPRImage = "pack://application:,,,/Icons/Up.png";
                MaxPRToolTip = "+" + S.StoredMaxPR.ToString();
            }
            else
if (S.StoredMaxPR < 0)
            {
                MaxPRImage = "pack://application:,,,/Icons/Down.png";
                MaxPRToolTip = S.StoredMaxPR.ToString();
            }
            else
            {
                MaxPRImage = "pack://application:,,,/Icons/Empty.png";
                MaxPRToolTip = "";
            }

            WinPercent = S.WinPercent.wpOverall.ToString();
            Games = S.Games.gOverall;
            ELO = S.ELO.eOverall;
            PR = S.PR;
            MaxELO = S.MaxELO.eOverall;
            MaxPR = S.MaxPR;
            Rank = S.Rank;
            FC1 = "pack://application:,,,/Flags/" + S.FavCivs.fcOverall.fc1.ToString() + ".png";
            FC2 = "pack://application:,,,/Flags/" + S.FavCivs.fcOverall.fc2.ToString() + ".png";
            FC3 = "pack://application:,,,/Flags/" + S.FavCivs.fcOverall.fc3.ToString() + ".png";
            Streak = S.Streak.sOverall.ToString("+#;-#;0");
            if (S.Streak.sOverall >= 0)
                ColorStreak = Brushes.LimeGreen;
            else
                ColorStreak = Brushes.Maroon;
            MaxStreak = S.MaxStreak.sOverall.ToString("+#;-#;0");
        }

        private string GetRatingsCheaterStatus(int ratio, int games)
        {
            if (games < 25)
                return "Not enough info";
            if (ratio >= 0 && ratio < 76)
                return "No";
            else
                if (ratio > 75 && ratio < 100)
                return "Warning";
            else
                return "Yes";
        }

        private void BindInfo1v1(Stat S)
        {
            FC1ToolTip = S.FavCivs.fc1v1.p1;
            FC2ToolTip = S.FavCivs.fc1v1.p2;
            FC3ToolTip = S.FavCivs.fc1v1.p3;
            Info = "Last login: " + US.LastLogin + Environment.NewLine +
           "Last online status update: " + US.LastUpdate + Environment.NewLine + "Last stats update: " +
           S.LastUpdate + Environment.NewLine + "Last game: " + S.LastGame;
            if (S.RankInfo != "")
                Info += Environment.NewLine + S.RankInfo;
            PT = S.RatioPT;
            NB = S.RatioNB;
            PTInfo = "For the last " + S.LastBattles.ToString() + " games" + Environment.NewLine + "Point trading progress: " +
                S.RatioPT.ToString() + " %";
            NBInfo = "For the last " + S.LastBattles.ToString() + " games" + Environment.NewLine + "Noob Basher progress: " +
    S.RatioNB.ToString() + " %";

            PTStatus = GetRatingsCheaterStatus(S.RatioPT, S.LastBattles);
            NBStatus = GetRatingsCheaterStatus(S.RatioNB, S.LastBattles);
            if (S.StoredELO.e1v1 > 0)
            {
                ELOImage = "pack://application:,,,/Icons/Up.png";
                ELOToolTip = "+" + S.StoredELO.e1v1.ToString();
            }
            else
                       if (S.StoredELO.e1v1 < 0)
            {
                ELOImage = "pack://application:,,,/Icons/Down.png";
                ELOToolTip = S.StoredELO.e1v1.ToString();
            }
            else
            {
                ELOImage = "pack://application:,,,/Icons/Empty.png";
                ELOToolTip = "";
            }


            if (S.StoredMaxELO.e1v1 > 0)
            {
                MaxELOImage = "pack://application:,,,/Icons/Up.png";
                MaxELOToolTip = "+" + S.StoredMaxELO.e1v1.ToString();
            }
            else
                 if (S.StoredMaxELO.e1v1 < 0)
            {
                MaxELOImage = "pack://application:,,,/Icons/Down.png";
                MaxELOToolTip = S.StoredMaxELO.e1v1.ToString();
            }
            else
            {
                MaxELOImage = "pack://application:,,,/Icons/Empty.png";
                MaxELOToolTip = "";
            }

            if (S.StoredGames.g1v1 > 0)
            {
                GImage = "pack://application:,,,/Icons/Up.png";
                GToolTip = "+" + S.StoredGames.g1v1.ToString();
            }
            else
      if (S.StoredGames.g1v1 < 0)
            {
                GImage = "pack://application:,,,/Icons/Down.png";
                GToolTip = S.StoredGames.g1v1.ToString();
            }
            else
            {
                GImage = "pack://application:,,,/Icons/Empty.png";
                GToolTip = "";
            }

            if (S.StoredWinPercent.wp1v1 > 0)
            {
                WPImage = "pack://application:,,,/Icons/Up.png";
                WPToolTip = "+" + S.StoredWinPercent.wp1v1.ToString();
            }
            else
     if (S.StoredWinPercent.wp1v1 < 0)
            {
                WPImage = "pack://application:,,,/Icons/Down.png";
                WPToolTip = S.StoredWinPercent.wp1v1.ToString();
            }
            else
            {
                WPImage = "pack://application:,,,/Icons/Empty.png";
                WPToolTip = "";
            }


            if (S.StoredPR > 0)
            {
                PRImage = "pack://application:,,,/Icons/Up.png";
                PRToolTip = "+" + S.StoredPR.ToString();
            }
            else
    if (S.StoredPR < 0)
            {
                PRImage = "pack://application:,,,/Icons/Down.png";
                PRToolTip = S.StoredPR.ToString();
            }
            else
            {
                PRImage = "pack://application:,,,/Icons/Empty.png";
                PRToolTip = "";
            }

            if (S.StoredMaxPR > 0)
            {
                MaxPRImage = "pack://application:,,,/Icons/Up.png";
                MaxPRToolTip = "+" + S.StoredMaxPR.ToString();
            }
            else
if (S.StoredMaxPR < 0)
            {
                MaxPRImage = "pack://application:,,,/Icons/Down.png";
                MaxPRToolTip = S.StoredMaxPR.ToString();
            }
            else
            {
                MaxPRImage = "pack://application:,,,/Icons/Empty.png";
                MaxPRToolTip = "";
            }
            WinPercent = S.WinPercent.wp1v1.ToString();
            Games = S.Games.g1v1;
            ELO = S.ELO.e1v1;
            PR = S.PR;
            MaxELO = S.MaxELO.e1v1;
            MaxPR = S.MaxPR;
            Rank = S.Rank;
            FC1 = "pack://application:,,,/Flags/" + S.FavCivs.fc1v1.fc1.ToString() + ".png";
            FC2 = "pack://application:,,,/Flags/" + S.FavCivs.fc1v1.fc2.ToString() + ".png";
            FC3 = "pack://application:,,,/Flags/" + S.FavCivs.fc1v1.fc3.ToString() + ".png";
            Streak = S.Streak.s1v1.ToString("+#;-#;0");
            if (S.Streak.s1v1 >= 0)
                ColorStreak = Brushes.LimeGreen;
            else
                ColorStreak = Brushes.Maroon;
            MaxStreak = S.MaxStreak.s1v1.ToString("+#;-#;0");
        }
        private void BindInfoTeam(Stat S)
        {
            FC1ToolTip = S.FavCivs.fcTeam.p1;
            FC2ToolTip = S.FavCivs.fcTeam.p2;
            FC3ToolTip = S.FavCivs.fcTeam.p3;
            Info = "Last login: " + US.LastLogin + Environment.NewLine +
           "Last online status update: " + US.LastUpdate + Environment.NewLine + "Last stats update: " +
           S.LastUpdate + Environment.NewLine + "Last game: " + S.LastGame;
            if (S.RankInfo != "")
                Info += Environment.NewLine + S.RankInfo;
            PT = S.RatioPT;
            NB = S.RatioNB;
            PTInfo = "For the last " + S.LastBattles.ToString() + " games" + Environment.NewLine + "Point trading progress: " +
                S.RatioPT.ToString() + " %";
            NBInfo = "For the last " + S.LastBattles.ToString() + " games" + Environment.NewLine + "Noob Basher progress: " +
    S.RatioNB.ToString() + " %";


            PTStatus = GetRatingsCheaterStatus(S.RatioPT, S.LastBattles);
            NBStatus = GetRatingsCheaterStatus(S.RatioNB, S.LastBattles);
            if (S.StoredELO.eTeam > 0)
            {
                ELOImage = "pack://application:,,,/Icons/Up.png";
                ELOToolTip = "+" + S.StoredELO.eTeam.ToString();
            }
            else
                       if (S.StoredELO.eTeam < 0)
            {
                ELOImage = "pack://application:,,,/Icons/Down.png";
                ELOToolTip = S.StoredELO.eTeam.ToString();
            }
            else
            {
                ELOImage = "pack://application:,,,/Icons/Empty.png";
                ELOToolTip = "";
            }


            if (S.StoredMaxELO.eTeam > 0)
            {
                MaxELOImage = "pack://application:,,,/Icons/Up.png";
                MaxELOToolTip = "+" + S.StoredMaxELO.eTeam.ToString();
            }
            else
                 if (S.StoredMaxELO.eTeam < 0)
            {
                MaxELOImage = "pack://application:,,,/Icons/Down.png";
                MaxELOToolTip = S.StoredMaxELO.eTeam.ToString();
            }
            else
            {
                MaxELOImage = "pack://application:,,,/Icons/Empty.png";
                MaxELOToolTip = "";
            }

            if (S.StoredGames.gTeam > 0)
            {
                GImage = "pack://application:,,,/Icons/Up.png";
                GToolTip = "+" + S.StoredGames.gTeam.ToString();
            }
            else
      if (S.StoredGames.gTeam < 0)
            {
                GImage = "pack://application:,,,/Icons/Down.png";
                GToolTip = S.StoredGames.gTeam.ToString();
            }
            else
            {
                GImage = "pack://application:,,,/Icons/Empty.png";
                GToolTip = "";
            }

            if (S.StoredWinPercent.wpTeam > 0)
            {
                WPImage = "pack://application:,,,/Icons/Up.png";
                WPToolTip = "+" + S.StoredWinPercent.wpTeam.ToString();
            }
            else
     if (S.StoredWinPercent.wpTeam < 0)
            {
                WPImage = "pack://application:,,,/Icons/Down.png";
                WPToolTip = S.StoredWinPercent.wpTeam.ToString();
            }
            else
            {
                WPImage = "pack://application:,,,/Icons/Empty.png";
                WPToolTip = "";
            }


            if (S.StoredPR > 0)
            {
                PRImage = "pack://application:,,,/Icons/Up.png";
                PRToolTip = "+" + S.StoredPR.ToString();
            }
            else
    if (S.StoredPR < 0)
            {
                PRImage = "pack://application:,,,/Icons/Down.png";
                PRToolTip = S.StoredPR.ToString();
            }
            else
            {
                PRImage = "pack://application:,,,/Icons/Empty.png";
                PRToolTip = "";
            }

            if (S.StoredMaxPR > 0)
            {
                MaxPRImage = "pack://application:,,,/Icons/Up.png";
                MaxPRToolTip = "+" + S.StoredMaxPR.ToString();
            }
            else
if (S.StoredMaxPR < 0)
            {
                MaxPRImage = "pack://application:,,,/Icons/Down.png";
                MaxPRToolTip = S.StoredMaxPR.ToString();
            }
            else
            {
                MaxPRImage = "pack://application:,,,/Icons/Empty.png";
                MaxPRToolTip = "";
            }
            WinPercent = S.WinPercent.wpTeam.ToString();
            Games = S.Games.gTeam;
            ELO = S.ELO.eTeam;
            PR = S.PR;
            MaxELO = S.MaxELO.eTeam;
            MaxPR = S.MaxPR;
            Rank = S.Rank;
            FC1 = "pack://application:,,,/Flags/" + S.FavCivs.fcTeam.fc1.ToString() + ".png";
            FC2 = "pack://application:,,,/Flags/" + S.FavCivs.fcTeam.fc2.ToString() + ".png";
            FC3 = "pack://application:,,,/Flags/" + S.FavCivs.fcTeam.fc3.ToString() + ".png";
            Streak = S.Streak.sTeam.ToString("+#;-#;0");
            if (S.Streak.sTeam >= 0)
                ColorStreak = Brushes.LimeGreen;
            else
                ColorStreak = Brushes.Maroon;
            MaxStreak = S.MaxStreak.sTeam.ToString("+#;-#;0");
        }
        async Task GetSPInfo(string Name)
        {
            try
            {
                string URL;
                if (File.Exists(Path.Combine(Paths.GetPlayerDirectoryPath(Name), "Supremacy.html")))
                    URL = "http://aoe3.jpcommunity.com/rating2/player?n=" + WebUtility.UrlEncode(Name) + "&t=age3ySPOverall&m=latestmatch";
                else
                    URL = "http://aoe3.jpcommunity.com/rating2/player?n=" + WebUtility.UrlEncode(Name) + "&t=age3ySPOverall&m=latestmatch" + Constant.URL_FOR_200_GAMES;

                string Data = await HttpGetAsync(URL);
                SP.Get(Data, Name, 0);
                BindInfoOverall(SP);

                Progress = "Loaded TAD Supremacy [" + stopWatch.Elapsed.ToString(@"s\.ff") + " secs]...";
            }
            catch { }
        }
        async Task GetTRInfo(string Name)
        {
            try
            {
                string URL;
                if (File.Exists(Path.Combine(Paths.GetPlayerDirectoryPath(Name), "Treaty.html")))
                    URL = "http://aoe3.jpcommunity.com/rating2/player?n=" + WebUtility.UrlEncode(Name) + "&t=age3yTROverall&m=latestmatch";
                else
                    URL = "http://aoe3.jpcommunity.com/rating2/player?n=" + WebUtility.UrlEncode(Name) + "&t=age3yTROverall&m=latestmatch" + Constant.URL_FOR_200_GAMES;

                string Data = await HttpGetAsync(URL);

                TR.Get(Data, Name, 1);

                Progress = "Loaded TAD Treaty [" + stopWatch.Elapsed.ToString(@"s\.ff") + " secs]...";

            }
            catch { }
        }
        async Task GetDMInfo(string Name)
        {
            try
            {
                string URL;
                if (File.Exists(Path.Combine(Paths.GetPlayerDirectoryPath(Name), "Deathmatch.html")))
                    URL = "http://aoe3.jpcommunity.com/rating2/player?n=" + WebUtility.UrlEncode(Name) + "&t=age3yDMOverall&m=latestmatch";
                else
                    URL = "http://aoe3.jpcommunity.com/rating2/player?n=" + WebUtility.UrlEncode(Name) + "&t=age3yDMOverall&m=latestmatch" + Constant.URL_FOR_200_GAMES;

                string Data = await HttpGetAsync(URL);
                DM.Get(Data, Name, 2);

                Progress = "Loaded TAD Deathmatch [" + stopWatch.Elapsed.ToString(@"s\.ff") + " secs]...";
            }
            catch { }
        }

        async Task GetSPnInfo(string Name)
        {
            try
            {
                string URL;
                if (File.Exists(Path.Combine(Paths.GetPlayerDirectoryPath(Name), "Supremacy_nilla.html")))
                    URL = "http://aoe3.jpcommunity.com/rating2/player?n=" + WebUtility.UrlEncode(Name) + "&t=age3SPOverall&m=latestmatch";
                else
                    URL = "http://aoe3.jpcommunity.com/rating2/player?n=" + WebUtility.UrlEncode(Name) + "&t=age3SPOverall&m=latestmatch" + Constant.URL_FOR_200_GAMES;

                string Data = await HttpGetAsync(URL);
                SPn.Get(Data, Name, 3);

                Progress = "Loaded Nilla Supremacy [" + stopWatch.Elapsed.ToString(@"s\.ff") + " secs]...";
            }
            catch { }
        }

        async Task GetDMnInfo(string Name)
        {
            try
            {
                string URL;
                if (File.Exists(Path.Combine(Paths.GetPlayerDirectoryPath(Name), "Deathmatch_nilla.html")))
                    URL = "http://aoe3.jpcommunity.com/rating2/player?n=" + WebUtility.UrlEncode(Name) + "&t=age3DMOverall&m=latestmatch";
                else
                    URL = "http://aoe3.jpcommunity.com/rating2/player?n=" + WebUtility.UrlEncode(Name) + "&t=age3DMOverall&m=latestmatch" + Constant.URL_FOR_200_GAMES;

                string Data = await HttpGetAsync(URL);

                DMn.Get(Data, Name, 4);

                Progress = "Loaded Nilla Deathmatch [" + stopWatch.Elapsed.ToString(@"s\.ff") + " secs]...";

            }
            catch { }
        }

        async void GetIP()
        {
            try
            {
                string Data = await HttpGetAsync("http://api.sypexgeo.net/");

                IPInfo ipInfo = JsonConvert.DeserializeObject<IPInfo>(Data);
                string city = ipInfo.city.name_en;
                string country = ipInfo.country.name_en;
                AnalyticsHelper.Current.LogEvent(AppVer, country + " - " + city);



            }
            catch { }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            stopWatch.Start();
            tbESO.IsEnabled = false;
            bGet.IsEnabled = false;
            bDetail.IsEnabled = false;
            cbMode.IsEnabled = false;
            cbType.IsEnabled = false;
            Cheater = CheckCheatGroup(tbESO.Text);
            if (Cheater == "")
            {
                tbESO.FontWeight = FontWeights.Normal;
                tbESO.TextDecorations = null;
            }
            else
            {
                tbESO.FontWeight = FontWeights.Bold;
                tbESO.TextDecorations = TextDecorations.Strikethrough;
            }
            US = new UserStatus(tbESO.Text);
            SP = new Stat();
            TR = new Stat();
            DM = new Stat();
            SPn = new Stat();
            DMn = new Stat();
            CheatSP = new CheatDetector();
            CheatTR = new CheatDetector();
            CheatDM = new CheatDetector();
            CheatSPn = new CheatDetector();
            CheatDMn = new CheatDetector();
            CheatColorSP = Brushes.Transparent;
            CheatColorTR = Brushes.Transparent;
            CheatColorDM = Brushes.Transparent;
            CheatColorSPn = Brushes.Transparent;
            CheatColorDMn = Brushes.Transparent;
            CheatMsgSP = "...";
            CheatMsgTR = "...";
            CheatMsgDM = "...";
            CheatMsgSPn = "...";
            CheatMsgDMn = "...";

            CheatUnitsSP = "";
            CheatUnitsTR = "";
            CheatUnitsDM = "";
            CheatUnitsSPn = "";
            CheatUnitsDMn = "";
            iCheat = "Search";
            Progress = "";
            iProgress.Visibility = Visibility.Visible;
            AllVisible = Visibility.Hidden;
            var T1 = GetUserInfo("http://www.agecommunity.com/query/query.aspx?md=user&name=" + WebUtility.UrlEncode(tbESO.Text));
            var T2 = GetSPInfo(tbESO.Text);
            var T3 = GetTRInfo(tbESO.Text);
            var T4 = GetDMInfo(tbESO.Text);
            var T5 = GetSPnInfo(tbESO.Text);
            var T6 = GetDMnInfo(tbESO.Text);

            await Task.WhenAll(T1, T2, T3, T4, T5, T6);

            Cheater = CheckCheatGroup(SP.Name);
            if (Cheater == "")
            {
                tbESO.FontWeight = FontWeights.Normal;
                tbESO.TextDecorations = null;
            }
            else
            {
                tbESO.FontWeight = FontWeights.Bold;
                tbESO.TextDecorations = TextDecorations.Strikethrough;
            }




            if (!Constant.SP_TRADERS_LIST.Contains(SP.Name))
                if (SP.RatioPT == 100 && SP.Games.gOverall > 25)
                {
                    AnalyticsHelper.Current.LogEvent("Blacklist - Point Traders : Supremacy", SP.Name);

                }
            if (!Constant.SP_TRADERS_LIST.Contains(SP.Name))

                if (TR.RatioPT == 100 && TR.Games.gOverall > 25)
                {
                    AnalyticsHelper.Current.LogEvent("Blacklist - Point Traders : Treaty",
                      SP.Name);

                }
            if (!Constant.DM_TRADERS_LIST.Contains(SP.Name))
                if (DM.RatioPT == 100 && DM.Games.gOverall > 25)
                {
                    AnalyticsHelper.Current.LogEvent("Blacklist - Point Traders : Deathmatch",
                      SP.Name);

                }
            if (!Constant.SPn_TRADERS_LIST.Contains(SP.Name))
                if (SPn.RatioPT == 100 && SPn.Games.gOverall > 25)
                {
                    AnalyticsHelper.Current.LogEvent("Blacklist - Point Traders : Supremacy nilla",
                      SP.Name);

                }
            if (!Constant.DMn_TRADERS_LIST.Contains(SP.Name))
                if (DMn.RatioPT == 100 && DMn.Games.gOverall > 25)
                {
                    AnalyticsHelper.Current.LogEvent("Blacklist - Point Traders : Deathmatch nilla",
                     SP.Name);

                }
            if (!Constant.SP_BASHERS_LIST.Contains(SP.Name))
                if (SP.RatioNB == 100 && SP.Games.gOverall > 25)
                {
                    AnalyticsHelper.Current.LogEvent("Blacklist - Noobbashers : Supremacy", SP.Name);

                }
            if (!Constant.TR_BASHERS_LIST.Contains(SP.Name))
                if (TR.RatioNB == 100 && TR.Games.gOverall > 25)
                {
                    AnalyticsHelper.Current.LogEvent("Blacklist - Noobbashers : Treaty",
                     SP.Name);

                }
            if (!Constant.DM_BASHERS_LIST.Contains(SP.Name))
                if (DM.RatioNB == 100 && DM.Games.gOverall > 25)
                {
                    AnalyticsHelper.Current.LogEvent("Blacklist - Noobbashers : Deathmatch", SP.Name);

                }
            if (!Constant.SPn_BASHERS_LIST.Contains(SP.Name))
                if (SPn.RatioNB == 100 && SPn.Games.gOverall > 25)
                {
                    AnalyticsHelper.Current.LogEvent("Blacklist - Noobbashers : Supremacy nilla",
                      SP.Name);

                }
            if (!Constant.DMn_BASHERS_LIST.Contains(SP.Name))
                if (DMn.RatioNB == 100 && DMn.Games.gOverall > 25)
                {
                    AnalyticsHelper.Current.LogEvent("Blacklist - Noobbashers : Deathmatch nilla",
                      SP.Name);

                }
            AnalyticsHelper.Current.LogEvent("TOP names : " + AppVer,
               SP.Name);

            using (RegistryKey AS = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                object AutoSave = AS.GetValue("BackUp");

                if (AutoSave != null)
                {
                    if (DateTime.Now.Date > DateTime.Parse(AutoSave.ToString()))
                    {
                        await Task.Run(() => ZipFile.CreateFromDirectory(Paths.GetAppDirectoryPath(), Path.Combine(Paths.GetBackUpDirectoryPath(), DateTime.Now.ToFileTime() + ".zip"), CompressionLevel.Fastest, true));
                        AS.SetValue("BackUp", DateTime.Now.Date);
                    }
                }
                else
                {
                    await Task.Run(() => ZipFile.CreateFromDirectory(Paths.GetAppDirectoryPath(), Path.Combine(Paths.GetBackUpDirectoryPath(), DateTime.Now.ToFileTime() + ".zip"), CompressionLevel.Fastest, true));
                    AS.SetValue("BackUp", DateTime.Now.Date);
                }
            }
            tbESO.Text = SP.Name;


            stopWatch.Stop();
            Elapsed = stopWatch.Elapsed.ToString();
            iProgress.Visibility = Visibility.Hidden;
            tbESO.IsEnabled = true;
            bGet.IsEnabled = true;
            bDetail.IsEnabled = true;
            cbMode.IsEnabled = true;
            cbType.IsEnabled = true;
            AllVisible = Visibility.Visible;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SP.Name != null)
                switch (cbMode.SelectedIndex)
                {
                    case 0:
                        switch (cbType.SelectedIndex)
                        {
                            case 0: BindInfo1v1(SP); break;
                            case 1: BindInfoTeam(SP); break;
                            case 2: BindInfoOverall(SP); break;
                        }
                        break;

                    case 1:
                        switch (cbType.SelectedIndex)
                        {
                            case 0: BindInfo1v1(TR); break;
                            case 1: BindInfoTeam(TR); break;
                            case 2: BindInfoOverall(TR); break;
                        }
                        break;
                    case 2:
                        switch (cbType.SelectedIndex)
                        {
                            case 0: BindInfo1v1(DM); break;
                            case 1: BindInfoTeam(DM); break;
                            case 2: BindInfoOverall(DM); break;
                        }
                        break;
                    case 3:
                        switch (cbType.SelectedIndex)
                        {
                            case 0: BindInfo1v1(SPn); break;
                            case 1: BindInfoTeam(SPn); break;
                            case 2: BindInfoOverall(SPn); break;
                        }
                        break;
                    case 4:
                        switch (cbType.SelectedIndex)
                        {
                            case 0: BindInfo1v1(DMn); break;
                            case 1: BindInfoTeam(DMn); break;
                            case 2: BindInfoOverall(DMn); break;
                        }
                        break;
                }
        }

        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            string json = JsonConvert.SerializeObject(Friends);
            File.WriteAllText(Path.Combine(FriendPath, "FriendList.json"), json);
            CefSharp.Cef.Shutdown();
            Environment.Exit(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void listView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                LoadItem();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Paths.LocalizeFile(Path.Combine(Paths.GetPlayerDirectoryPath(SP.Name), cbMode.Text + ".html"), SP.Name);
            Detail fDetail = new Detail();
            fDetail.wbDetail.Address = Paths.GetHTMLLocalizedFilePath(SP.Name);
            fDetail.wbDetail.Load(Paths.GetHTMLLocalizedFilePath(SP.Name));
            fDetail.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddItem();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void mainWindow_Closed(object sender, EventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (!pELO.IsOpen)

                pELO.IsOpen = true;
            else
                pELO.IsOpen = false;

        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

            switch (cbMode.SelectedIndex)
            {
                case 0:
                    Process.Start("http://www.agecommunity.com/stats/EntityStats.aspx?loc=en-US&EntityName="
              + WebUtility.UrlEncode(SP.Name) + "&md=ZS_Supremacy&sFlag=2");
                    break;
                case 1:
                    Process.Start("http://www.agecommunity.com/stats/EntityStats.aspx?loc=en-US&EntityName="
              + WebUtility.UrlEncode(SP.Name) + "&md=ZS_Treaty&sFlag=2");
                    break;
                case 2:
                    Process.Start("http://www.agecommunity.com/stats/EntityStats.aspx?loc=en-US&EntityName="
              + WebUtility.UrlEncode(SP.Name) + "&md=ZS_Deathmatch&sFlag=2");
                    break;
                case 3:
                    Process.Start("http://www.agecommunity.com/stats/EntityStats.aspx?loc=en-US&EntityName="
              + WebUtility.UrlEncode(SP.Name) + "&md=ZS_Supremacy&sFlag=0");
                    break;
                case 4:
                    Process.Start("http://www.agecommunity.com/stats/EntityStats.aspx?loc=en-US&EntityName="
              + WebUtility.UrlEncode(SP.Name) + "&md=ZS_Deathmatch&sFlag=0");
                    break;

            }
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://vk.com/xakops");
        }

        private void Image_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/XaKOps/ESO-Assistant");
        }

        private void Image_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://www.twitch.tv/xakops");
        }

        private void Image_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://www.youtube.com/user/xakops");
        }

        private async void Image_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            if (pCheatCheck.IsOpen)
                pCheatCheck.IsOpen = false;
            else
            {
                iCheatCheck.IsEnabled = false;

                CheatSP = new CheatDetector();
                CheatTR = new CheatDetector();
                CheatDM = new CheatDetector();
                CheatSPn = new CheatDetector();
                CheatDMn = new CheatDetector();

                CheatColorSP = Brushes.Transparent;
                CheatColorTR = Brushes.Transparent;
                CheatColorDM = Brushes.Transparent;
                CheatColorSPn = Brushes.Transparent;
                CheatColorDMn = Brushes.Transparent;

                CheatMsgSP = "...";
                CheatMsgTR = "...";
                CheatMsgDM = "...";
                CheatMsgSPn = "...";
                CheatMsgDMn = "...";

                CheatUnitsSP = "";
                CheatUnitsTR = "";
                CheatUnitsDM = "";
                CheatUnitsSPn = "";
                CheatUnitsDMn = "";

                iCheat = "Search";
                var T1 = GetCheatSP();
                var T2 = GetCheatTR();
                var T3 = GetCheatDM();
                var T4 = GetCheatSPn();
                var T5 = GetCheatDMn();

                await Task.WhenAll(T1, T2, T3, T4, T5);

                if (CheatSP.isCheat)
                {
                    CheatColorSP = Brushes.Maroon;
                    CheatMsgSP = "Cheat detected!";
                }
                else
                {
                    CheatColorSP = Brushes.LimeGreen;
                    CheatMsgSP = "No cheat detected!";
                }
                if (CheatTR.isCheat)
                {
                    CheatColorTR = Brushes.Maroon;
                    CheatMsgTR = "Cheat detected!";
                }
                else
                {
                    CheatColorTR = Brushes.LimeGreen;
                    CheatMsgTR = "No cheat detected!";
                }
                if (CheatDM.isCheat)
                {
                    CheatColorDM = Brushes.Maroon;
                    CheatMsgDM = "Cheat detected!";
                }
                else
                {
                    CheatColorDM = Brushes.LimeGreen;
                    CheatMsgDM = "No cheat detected!";
                }
                if (CheatSPn.isCheat)
                {
                    CheatColorSPn = Brushes.Maroon;
                    CheatMsgSPn = "Cheat detected!";
                }
                else
                {
                    CheatColorSPn = Brushes.LimeGreen;
                    CheatMsgSPn = "No cheat detected!";
                }
                if (CheatDMn.isCheat)
                {
                    CheatColorDMn = Brushes.Maroon;
                    CheatMsgDMn = "Cheat detected!";
                }
                else
                {
                    CheatColorDMn = Brushes.LimeGreen;
                    CheatMsgDMn = "No cheat detected!";
                }

                CheatUnitsSP = CheatSP.Units;
                CheatUnitsTR = CheatTR.Units;
                CheatUnitsDM = CheatDM.Units;
                CheatUnitsSPn = CheatSPn.Units;
                CheatUnitsDMn = CheatDMn.Units;

                if (CheatSP.isCheat || CheatTR.isCheat || CheatDM.isCheat ||
          CheatSPn.isCheat || CheatDMn.isCheat)
                {
                    iCheat = "Yes";
                    if (!Constant.MOESBAR_LIST.Contains(SP.Name))
                        AnalyticsHelper.Current.LogEvent("Blacklist - Moesbar",
                        SP.Name);
                }
                else
                    iCheat = "No";
                iCheatCheck.IsEnabled = true;

                pCheatCheck.IsOpen = true;
            }
        }

        async Task GetCheatSP()
        {
            try
            {
                string Data = await HttpGetAsync("http://www.agecommunity.com/stats/EntityStats.aspx?loc=en-US&EntityName=" + WebUtility.UrlEncode(SP.Name) + "&sFlag=2&md=ZS_Supremacy");
                CheatSP.Get(SP.Name, Data);

            }
            catch { }
        }
        async Task GetCheatTR()
        {
            try
            {
                string Data = await HttpGetAsync("http://www.agecommunity.com/stats/EntityStats.aspx?loc=en-US&EntityName=" + WebUtility.UrlEncode(SP.Name) + "&sFlag=2&md=ZS_Treaty");
                CheatTR.Get(SP.Name, Data);

            }
            catch { }
        }

        async Task GetCheatDM()
        {
            try
            {
                string Data = await HttpGetAsync("http://www.agecommunity.com/stats/EntityStats.aspx?loc=en-US&EntityName=" + WebUtility.UrlEncode(SP.Name) + "&sFlag=2&md=ZS_Deathmatch");
                CheatDM.Get(SP.Name, Data);

            }
            catch { }
        }

        async Task GetCheatSPn()
        {
            try
            {
                string Data = await HttpGetAsync("http://www.agecommunity.com/stats/EntityStats.aspx?loc=en-US&EntityName=" + WebUtility.UrlEncode(SP.Name) + "&sFlag=0&md=ZS_Supremacy");
                CheatSPn.Get(SP.Name, Data);

            }
            catch { }
        }

        async Task GetCheatDMn()
        {
            try
            {
                string Data = await HttpGetAsync("http://www.agecommunity.com/stats/EntityStats.aspx?loc=en-US&EntityName=" + WebUtility.UrlEncode(SP.Name) + "&sFlag=0&md=ZS_Deathmatch");
                CheatDMn.Get(SP.Name, Data);

            }
            catch { }
        }

        private void Image_MouseLeftButtonDown_7(object sender, MouseButtonEventArgs e)
        {
            if (!pWin.IsOpen)
                pWin.IsOpen = true;
            else
                pWin.IsOpen = false;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Paths.OpenTAD("age3t.exe");
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Paths.OpenNilla("age3.exe");
        }

        private void Image_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            Paths.OpenTAD("age3y.exe");
        }

        private void Image_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            Paths.OpenTAD("age3p.exe");
        }


        private void TwitchTADTimer_Tick(object sender, EventArgs e)
        {
            TwitchTADTimer.Stop();
            TwitchTADTimer.Interval = TimeSpan.FromMilliseconds(10000);
            GetTwitchStreamsTAD();

        }

        private void TwitchNillaTimer_Tick(object sender, EventArgs e)
        {
            TwitchNillaTimer.Stop();
            TwitchNillaTimer.Interval = TimeSpan.FromMilliseconds(10000);
            GetTwitchStreamsNilla();

        }

        private void TwitchAOEOTimer_Tick(object sender, EventArgs e)
        {
            TwitchAOEOTimer.Stop();
            TwitchAOEOTimer.Interval = TimeSpan.FromMilliseconds(10000);
            GetTwitchStreamsAOEO();

        }

        private void TwitchAOEIIHDTimer_Tick(object sender, EventArgs e)
        {
            TwitchAOEIIHDTimer.Stop();
            TwitchAOEIIHDTimer.Interval = TimeSpan.FromMilliseconds(10000);
            GetTwitchStreamsAOEIIHD();

        }

        private void TwitchAOEIIConqTimer_Tick(object sender, EventArgs e)
        {
            TwitchAOEIIConqTimer.Stop();
            TwitchAOEIIConqTimer.Interval = TimeSpan.FromMilliseconds(10000);
            GetTwitchStreamsAOEIIConq();

        }

        async void GetTwitchStreamsTAD()
        {

            string Data = await HttpGetAsync("https://api.twitch.tv/kraken/streams/?game=" + WebUtility.UrlEncode("Age of Empires III: The Asian Dynasties") + "&live&client_id=9rrpybi820nvoixrr2lkqk19ae8k4ef");
            try
            {
                RootObject root = JsonConvert.DeserializeObject<RootObject>(Data);
                ObservableCollection<TreeItem> S = new ObservableCollection<TreeItem>();

                for (int i = 0; i < root.streams.Count; i++)
                {
                    S.Add(new TreeItem() { Name = root.streams[i].channel.display_name, Followers = root.streams[i].channel.followers, Viewers = root.streams[i].viewers, Status = root.streams[i].channel.status, Length = DateTime.Now - DateTime.Parse(root.streams[i].created_at).ToLocalTime(), URL = root.streams[i].channel.url });
                }
                GamesStream[0].Streams = S;
            }
            catch { }
            TwitchTADTimer.Start();
        }

        async void GetTwitchStreamsNilla()
        {

            string Data = await HttpGetAsync("https://api.twitch.tv/kraken/streams/?game=" + WebUtility.UrlEncode("Age of Empires III") + "&live&client_id=9rrpybi820nvoixrr2lkqk19ae8k4ef");
            try
            {
                RootObject root = JsonConvert.DeserializeObject<RootObject>(Data);
                ObservableCollection<TreeItem> S = new ObservableCollection<TreeItem>();

                for (int i = 0; i < root.streams.Count; i++)
                {
                    S.Add(new TreeItem() { Name = root.streams[i].channel.display_name, Followers = root.streams[i].channel.followers, Viewers = root.streams[i].viewers, Status = root.streams[i].channel.status, Length = DateTime.Now - DateTime.Parse(root.streams[i].created_at).ToLocalTime(), URL = root.streams[i].channel.url });
                }
                GamesStream[1].Streams = S;
            }
            catch { }
            TwitchNillaTimer.Start();
        }

        async void GetTwitchStreamsAOEO()
        {

            string Data = await HttpGetAsync("https://api.twitch.tv/kraken/streams/?game=" + WebUtility.UrlEncode("Age of Empires Online") + "&live&client_id=9rrpybi820nvoixrr2lkqk19ae8k4ef");
            try
            {
                RootObject root = JsonConvert.DeserializeObject<RootObject>(Data);
                ObservableCollection<TreeItem> S = new ObservableCollection<TreeItem>();

                for (int i = 0; i < root.streams.Count; i++)
                {
                    S.Add(new TreeItem() { Name = root.streams[i].channel.display_name, Followers = root.streams[i].channel.followers, Viewers = root.streams[i].viewers, Status = root.streams[i].channel.status, Length = DateTime.Now - DateTime.Parse(root.streams[i].created_at).ToLocalTime(), URL = root.streams[i].channel.url });
                }
                GamesStream[2].Streams = S;
            }
            catch { }
            TwitchAOEOTimer.Start();
        }

        async void GetTwitchStreamsAOEIIHD()
        {

            string Data = await HttpGetAsync("https://api.twitch.tv/kraken/streams/?game=" + WebUtility.UrlEncode("Age of Empires II: HD Edition") + "&live&client_id=9rrpybi820nvoixrr2lkqk19ae8k4ef");
            try
            {
                RootObject root = JsonConvert.DeserializeObject<RootObject>(Data);
                ObservableCollection<TreeItem> S = new ObservableCollection<TreeItem>();

                for (int i = 0; i < root.streams.Count; i++)
                {
                    S.Add(new TreeItem() { Name = root.streams[i].channel.display_name, Followers = root.streams[i].channel.followers, Viewers = root.streams[i].viewers, Status = root.streams[i].channel.status, Length = DateTime.Now - DateTime.Parse(root.streams[i].created_at).ToLocalTime(), URL = root.streams[i].channel.url });
                }
                GamesStream[3].Streams = S;
            }
            catch { }
            TwitchAOEIIHDTimer.Start();
        }

        async void GetTwitchStreamsAOEIIConq()
        {

            string Data = await HttpGetAsync("https://api.twitch.tv/kraken/streams/?game=" + WebUtility.UrlEncode("Age of Empires II: The Conquerors") + "&live&client_id=9rrpybi820nvoixrr2lkqk19ae8k4ef");
            try
            {
                RootObject root = JsonConvert.DeserializeObject<RootObject>(Data);
                ObservableCollection<TreeItem> S = new ObservableCollection<TreeItem>();

                for (int i = 0; i < root.streams.Count; i++)
                {
                    S.Add(new TreeItem() { Name = root.streams[i].channel.display_name, Followers = root.streams[i].channel.followers, Viewers = root.streams[i].viewers, Status = root.streams[i].channel.status, Length = DateTime.Now - DateTime.Parse(root.streams[i].created_at).ToLocalTime(), URL = root.streams[i].channel.url });
                }
                GamesStream[4].Streams = S;
            }
            catch { }
            TwitchAOEIIConqTimer.Start();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (treeView1.SelectedItem != null)
                if (treeView1.SelectedItem is TreeItem)
                    Process.Start((treeView1.SelectedItem as TreeItem).URL);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            TwitchVisibility = "Auto";
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TwitchVisibility = "0";
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Directory.Delete(Paths.GetAppDirectoryPath(), true);
        }

        private void MenuItem_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            using (RegistryKey R = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\Age of Empires 3\\1.0"))
            {
                object PID = R.GetValue("PID");
                object DigitalProductID = R.GetValue("DigitalProductID");
                if (PID != null || DigitalProductID != null)
                {
                    Key = "CD key detected!";
                    KeyColor = Brushes.Maroon;
                    KeyEnabled = true;
                }
                else
                {
                    Key = "No CD key detected!";
                    KeyColor = Brushes.LimeGreen;
                    KeyEnabled = false;
                }
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            using (RegistryKey R = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\Age of Empires 3\\1.0"))
            {
                R.DeleteValue("PID", false);
                R.DeleteValue("DigitalProductID", false);
                Key = "No CD key detected!";
                KeyColor = Brushes.LimeGreen;
                KeyEnabled = false;
            }
        }

        private void CheckFriendSettings()
        {
            using (RegistryKey R = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                object oLastLogin = R.GetValue("oLastLogin");
                object oClan = R.GetValue("oClan");
                object oSP = R.GetValue("oSP");
                object oTR = R.GetValue("oTR");
                object oDM = R.GetValue("oDM");
                object oSPn = R.GetValue("oSPn");
                object oDMn = R.GetValue("oDMn");
                if (oLastLogin != null)
                    LastLoginEnabled = bool.Parse(oLastLogin.ToString());
                else
                {
                    LastLoginEnabled = true;
                    R.SetValue("oLastLogin", true);
                }

                if (oClan != null)
                    ClanEnabled = bool.Parse(oClan.ToString());
                else
                {
                    ClanEnabled = true;
                    R.SetValue("oClan", true);
                }

                if (oSP != null)
                    SPEnabled = bool.Parse(oSP.ToString());
                else
                {
                    SPEnabled = true;
                    R.SetValue("oSP", true);
                }

                if (oTR != null)
                    TREnabled = bool.Parse(oTR.ToString());
                else
                {
                    TREnabled = true;
                    R.SetValue("oTR", true);
                }

                if (oDM != null)
                    DMEnabled = bool.Parse(oDM.ToString());
                else
                {
                    DMEnabled = false;
                    R.SetValue("oDM", false);
                }

                if (oSPn != null)
                    SPnEnabled = bool.Parse(oSPn.ToString());
                else
                {
                    SPnEnabled = false;
                    R.SetValue("oSPn", false);
                }

                if (oDMn != null)
                    DMnEnabled = bool.Parse(oDMn.ToString());
                else
                {
                    DMnEnabled = false;
                    R.SetValue("oDMn", false);
                }
            }
        }

        private void MenuItem_SubmenuOpened_1(object sender, RoutedEventArgs e)
        {
            CheckFriendSettings();
        }

        private void MenuItem_Checked(object sender, RoutedEventArgs e)
        {
            using (RegistryKey R = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                R.SetValue("oLastLogin", true);
            }
        }

        private void MenuItem_Unchecked(object sender, RoutedEventArgs e)
        {
            using (RegistryKey R = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                R.SetValue("oLastLogin", false);
            }
        }

        private void MenuItem_Checked_1(object sender, RoutedEventArgs e)
        {
            using (RegistryKey R = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                R.SetValue("oClan", true);
            }
        }

        private void MenuItem_Unchecked_1(object sender, RoutedEventArgs e)
        {
            using (RegistryKey R = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                R.SetValue("oClan", false);
            }
        }

        private void MenuItem_Checked_2(object sender, RoutedEventArgs e)
        {
            using (RegistryKey R = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                R.SetValue("oSP", true);
            }
        }

        private void MenuItem_Unchecked_2(object sender, RoutedEventArgs e)
        {
            using (RegistryKey R = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                R.SetValue("oSP", false);
            }
        }

        private void MenuItem_Checked_3(object sender, RoutedEventArgs e)
        {
            using (RegistryKey R = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                R.SetValue("oTR", true);
            }
        }

        private void MenuItem_Unchecked_3(object sender, RoutedEventArgs e)
        {
            using (RegistryKey R = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                R.SetValue("oTR", false);
            }
        }

        private void MenuItem_Checked_4(object sender, RoutedEventArgs e)
        {
            using (RegistryKey R = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                R.SetValue("oDM", true);
            }
        }

        private void MenuItem_Unchecked_4(object sender, RoutedEventArgs e)
        {
            using (RegistryKey R = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                R.SetValue("oDM", false);
            }
        }

        private void MenuItem_Checked_5(object sender, RoutedEventArgs e)
        {
            using (RegistryKey R = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                R.SetValue("oSPn", true);
            }
        }

        private void MenuItem_Unchecked_5(object sender, RoutedEventArgs e)
        {
            using (RegistryKey R = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                R.SetValue("oSPn", false);
            }
        }

        private void MenuItem_Checked_6(object sender, RoutedEventArgs e)
        {
            using (RegistryKey R = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                R.SetValue("oDMn", true);
            }
        }

        private void MenuItem_Unchecked_6(object sender, RoutedEventArgs e)
        {
            using (RegistryKey R = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ESO-Assistant"))
            {
                R.SetValue("oDMn", false);
            }
        }

        private void tbESO_TextChanged(object sender, TextChangedEventArgs e)
        {
            bGet.IsEnabled = tbESO.Text != "";
            Cheater = CheckCheatGroup(tbESO.Text);
            if (Cheater == "")
            {
                tbESO.FontWeight = FontWeights.Normal;
                tbESO.TextDecorations = null;
            }
            else
            {
                tbESO.FontWeight = FontWeights.Bold;
                tbESO.TextDecorations = TextDecorations.Strikethrough;
            }

        }

        private void tbESO_KeyDown(object sender, KeyEventArgs e)
        {
            if (tbESO.Text != "" && e.Key == System.Windows.Input.Key.Enter)
            {
                bGet.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            }
        }

        private void MetroWindow_ContentRendered(object sender, EventArgs e)
        {
            AType = WPF.Common.VisibilityAnimation.AnimationType.Fade;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start(Paths.GetBackUpDirectoryPath());
        }

        private void Image_MouseLeftButtonDown_8(object sender, MouseButtonEventArgs e)
        {

            if (US.Clan != "")
            {
                if (!pClan.IsOpen)
                {
                    iClan.IsEnabled = false;
                    GetClanInfo();

                }
                pClan.IsOpen = false;
            }

        }

        async void GetClanInfo()
        {

            string Data = await HttpGetAsync("http://www.agecommunity.com/query/query.aspx?name=" + WebUtility.UrlEncode(US.Clan) + "&md=clan");
            ClanInfo CI = new ClanInfo();
            CI.GetInfo(Data);
            ClanName = CI.Name;
            ClanOwner = CI.Owner;
            ClanDate = CI.DateCreated;
            ClanTag = CI.Tag;
            ClanList.Clear();
            for (int i = 0; i < CI.Users.Count; i++)
                ClanList.Add(CI.Users[i]);

            iClan.IsEnabled = true;
            pClan.IsOpen = true;
        }

        private void Image_MouseLeftButtonDown_9(object sender, MouseButtonEventArgs e)
        {

            if (!pGames.IsOpen)
            {
                iGames.IsEnabled = false;
                GetCivInfo();

                pGames.IsOpen = true;
            }
            else
                pGames.IsOpen = false;

        }


        async void GetCivInfo()
        {
            string URL;
            switch (cbMode.SelectedIndex)
            {
                case 0: URL = "http://agecommunity.com/query/query.aspx?md=supremacy&g=age3y"; break;
                case 1: URL = "http://agecommunity.com/query/query.aspx?md=treaty&g=age3y"; break;
                case 2: URL = "http://agecommunity.com/query/query.aspx?md=deathmatch&g=age3y"; break;
                case 3: URL = "http://agecommunity.com/query/query.aspx?md=supremacy&g=age3"; break;
                default: URL = "http://agecommunity.com/query/query.aspx?md=deathmatch&g=age3"; break;
            }
            string Data = await HttpGetAsync(URL);
            CivStat CS = new CivStat();
            CS.GetStat(Data);
            WonPie.Clear();
            for (int i = 0; i < CS.WonChart.Count; i++)
                WonPie.Add(CS.WonChart[i]);
            UsedPie.Clear();
            for (int i = 0; i < CS.UsedChart.Count; i++)
                UsedPie.Add(CS.UsedChart[i]);
            OverallGames = CS.OverallGames;
            WonP = Math.Round((double)WonPie[0].Value / UsedPie[0].Value * 100);

            UsedP = Math.Round((double)UsedPie[0].Value / OverallGames * 100);
            iGames.IsEnabled = true;
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (WonPie.Count > 0)
            {
                WonP = Math.Round((double)WonPie[(sender as ComboBox).SelectedIndex].Value / UsedPie[(sender as ComboBox).SelectedIndex].Value * 100);
                UsedP = Math.Round((double)UsedPie[(sender as ComboBox).SelectedIndex].Value / OverallGames * 100);
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            switch (cbMode.SelectedIndex)
            {
                case 0:
                    Process.Start("http://aoe3.jpcommunity.com/rating2/player?n=" +
                     WebUtility.UrlEncode(SP.Name) + "&t=age3ySPOverall");
                    break;
                case 1:
                    Process.Start("http://aoe3.jpcommunity.com/rating2/player?n=" +
                     WebUtility.UrlEncode(SP.Name) + "&t=age3yTROverall");
                    break;
                case 2:
                    Process.Start("http://aoe3.jpcommunity.com/rating2/player?n=" +
                    WebUtility.UrlEncode(SP.Name) + "&t=age3yDMOverall");
                    break;
                case 3:
                    Process.Start("http://aoe3.jpcommunity.com/rating2/player?n=" +
                     WebUtility.UrlEncode(SP.Name) + "&t=age3SPOverall");
                    break;
                case 4:
                    Process.Start("http://aoe3.jpcommunity.com/rating2/player?n=" +
                    WebUtility.UrlEncode(SP.Name) + "&t=age3DMOverall");
                    break;
            }
        }

        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    Initial1.Clear();
                    Initial1.Add(new ELOItem { Name = "Player 1" });
                    Initial2.Clear();
                    Initial2.Add(new ELOItem { Name = "Player 1" });
                    break;
                case 1:
                    Initial1.Clear();
                    Initial1.Add(new ELOItem { Name = "Player 1" });
                    Initial1.Add(new ELOItem { Name = "Player 2" });
                    Initial2.Clear();
                    Initial2.Add(new ELOItem { Name = "Player 1" });
                    Initial2.Add(new ELOItem { Name = "Player 2" });
                    break;
                case 2:
                    Initial1.Clear();
                    Initial1.Add(new ELOItem { Name = "Player 1" });
                    Initial1.Add(new ELOItem { Name = "Player 2" });
                    Initial1.Add(new ELOItem { Name = "Player 3" });
                    Initial2.Clear();
                    Initial2.Add(new ELOItem { Name = "Player 1" });
                    Initial2.Add(new ELOItem { Name = "Player 2" });
                    Initial2.Add(new ELOItem { Name = "Player 3" });
                    break;
                case 3:
                    Initial1.Clear();
                    Initial1.Add(new ELOItem { Name = "Player 1" });
                    Initial1.Add(new ELOItem { Name = "Player 2" });
                    Initial1.Add(new ELOItem { Name = "Player 3" });
                    Initial1.Add(new ELOItem { Name = "Player 4" });
                    Initial2.Clear();
                    Initial2.Add(new ELOItem { Name = "Player 1" });
                    Initial2.Add(new ELOItem { Name = "Player 2" });
                    Initial2.Add(new ELOItem { Name = "Player 3" });
                    Initial2.Add(new ELOItem { Name = "Player 4" });
                    break;
            }
            foreach (ELOItem e1 in Initial1)
            {
                if (CalcELO(ELO1, ELO2, Initial1.Count * 2) != 0)
                {
                    e1.ELOChangeUp = (e1.ELOValue + CalcELO(ELO1, ELO2, Initial1.Count * 2)).ToString() + " (+" + CalcELO(ELO1, ELO2, Initial1.Count * 2).ToString() + ")";
                    e1.ELOChangeDown = (e1.ELOValue - CalcELO(ELO2, ELO1, Initial1.Count * 2)).ToString() + " (-" + CalcELO(ELO2, ELO1, Initial1.Count * 2).ToString() + ")";

                }
                else
                {
                    e1.ELOChangeUp = e1.ELOValue.ToString();
                    e1.ELOChangeDown = e1.ELOValue.ToString();
                }
            }
            foreach (ELOItem e2 in Initial2)
            {
                if (CalcELO(ELO1, ELO2, Initial2.Count * 2) != 0)
                {
                    e2.ELOChangeUp = (e2.ELOValue + CalcELO(ELO2, ELO1, Initial1.Count * 2)).ToString() + " (+" + CalcELO(ELO2, ELO1, Initial1.Count * 2).ToString() + ")";
                    e2.ELOChangeDown = (e2.ELOValue - CalcELO(ELO1, ELO2, Initial1.Count * 2)).ToString() + " (-" + CalcELO(ELO1, ELO2, Initial1.Count * 2).ToString() + ")";

                }
                else
                {
                    e2.ELOChangeUp = e2.ELOValue.ToString();
                    e2.ELOChangeDown = e2.ELOValue.ToString();
                }
            }
        }

        private void lAbout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (pAbout.IsOpen)
                pAbout.IsOpen = false;
            else
                pAbout.IsOpen = true;
        }

        private void listView1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

            pFriends.IsOpen = true;
        }

        private void listView1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (pFriends.IsOpen)
                pFriends.IsOpen = false;
            else
                pFriends.IsOpen = true;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((sender as TextBox).Text != "" && e.Key == System.Windows.Input.Key.Enter)
            {
                bAdd.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            }
        }

        private void dgInitial1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Initial1[e.Row.GetIndex()].ELOValue = (int)Math.Round((e.EditingElement as NumericUpDown).Value.Value);

            foreach (ELOItem e1 in Initial1)
            {
                if (CalcELO(ELO1, ELO2, Initial1.Count * 2) != 0)
                {
                    e1.ELOChangeUp= (e1.ELOValue + CalcELO(ELO1, ELO2, Initial1.Count * 2)).ToString() + " (+" + CalcELO(ELO1, ELO2, Initial1.Count * 2).ToString() + ")";
                    e1.ELOChangeDown = (e1.ELOValue - CalcELO(ELO2, ELO1, Initial1.Count * 2)).ToString() + " (-" + CalcELO(ELO2, ELO1, Initial1.Count * 2).ToString() + ")";

                }
                else
                {
                    e1.ELOChangeUp = e1.ELOValue.ToString();
                    e1.ELOChangeDown = e1.ELOValue.ToString();
                }
            }
            foreach (ELOItem e2 in Initial2)
            {
                if (CalcELO(ELO1, ELO2, Initial2.Count * 2) != 0)
                {
                    e2.ELOChangeUp = (e2.ELOValue + CalcELO(ELO2, ELO1, Initial1.Count * 2)).ToString() + " (+" + CalcELO(ELO2, ELO1, Initial1.Count * 2).ToString() + ")";
                    e2.ELOChangeDown = (e2.ELOValue - CalcELO(ELO1, ELO2, Initial1.Count * 2)).ToString() + " (-" + CalcELO(ELO1, ELO2, Initial1.Count * 2).ToString() + ")";

                }
                else
                {
                    e2.ELOChangeUp = e2.ELOValue.ToString();
                    e2.ELOChangeDown = e2.ELOValue.ToString();
                }
            }

        }

        private void dgInitial2_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Initial2[e.Row.GetIndex()].ELOValue = (int)Math.Round((e.EditingElement as NumericUpDown).Value.Value);
            foreach (ELOItem e1 in Initial1)
            {
                if (CalcELO(ELO1, ELO2, Initial1.Count * 2) != 0)
                {
                    e1.ELOChangeUp = (e1.ELOValue + CalcELO(ELO1, ELO2, Initial1.Count * 2)).ToString() + " (+" + CalcELO(ELO1, ELO2, Initial1.Count * 2).ToString() + ")";
                    e1.ELOChangeDown = (e1.ELOValue - CalcELO(ELO2, ELO1, Initial1.Count * 2)).ToString() + " (-" + CalcELO(ELO2, ELO1, Initial1.Count * 2).ToString() + ")";

                }
                else
                {
                    e1.ELOChangeUp = e1.ELOValue.ToString();
                    e1.ELOChangeDown = e1.ELOValue.ToString();
                }
            }
            foreach (ELOItem e2 in Initial2)
            {
                if (CalcELO(ELO1, ELO2, Initial2.Count * 2) != 0)
                {
                    e2.ELOChangeUp = (e2.ELOValue + CalcELO(ELO2, ELO1, Initial1.Count * 2)).ToString() + " (+" + CalcELO(ELO2, ELO1, Initial1.Count * 2).ToString() + ")";
                    e2.ELOChangeDown = (e2.ELOValue - CalcELO(ELO1, ELO2, Initial1.Count * 2)).ToString() + " (-" + CalcELO(ELO1, ELO2, Initial1.Count * 2).ToString() + ")";

                }
                else
                {
                    e2.ELOChangeUp = e2.ELOValue.ToString();
                    e2.ELOChangeDown = e2.ELOValue.ToString();
                }
            }
        }

        private void MenuItem_SubmenuOpened_2(object sender, RoutedEventArgs e)
        {
            using (RegistryKey AS = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\age of empires 3\\1.0"))
            {
                object P = AS.GetValue("setuppath");
                if (P != null)
                    if (File.Exists(Path.Combine(P.ToString(), "Startup", "user.cfg")))
                    {
                        List<string> S = File.ReadAllLines(Path.Combine(P.ToString(), "Startup", "user.cfg")).ToList();
                        miFPS.IsChecked = S.IndexOf("showFPS") != -1;
                        miIntro.IsChecked = S.IndexOf("noIntroCinematics") != -1;
                        miMsecs.IsChecked = S.IndexOf("showMilliseconds") != -1;
                        int z1 = S.FindIndex(x => x.StartsWith("maxZoom"));
                        if (z1 >= 0)
                            udMax.Value = Double.Parse(S[z1].Split('=')[1]);
                        else
                            udMax.Value = 60;
                        int z2 = S.FindIndex(x => x.StartsWith("normalZoom"));
                        if (z2 >= 0)
                            udNorm.Value = Double.Parse(S[z2].Split('=')[1]);
                        else
                            udNorm.Value = 50;
                        int z3 = S.FindIndex(x => x.StartsWith("minZoom"));
                        if (z3 >= 0)
                            udMin.Value = Double.Parse(S[z3].Split('=')[1]);
                        else
                            udMin.Value = 29;
                    }
                    else
                    {
                        udMax.Value = 60;
                        udNorm.Value = 50;
                        udMin.Value = 29;
                    }
                if (File.Exists(Path.Combine(P.ToString(), "Startup", "user.con")))
                {
                    string M = File.ReadAllText(Path.Combine(P.ToString(), "Startup", "user.con"));
                    miRotator.IsChecked = M.Contains("uiWheelRotatePlacedUnit");
                }
            }
        }

        private void miMsecs_Checked(object sender, RoutedEventArgs e)
        {
            using (RegistryKey AS = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\age of empires 3\\1.0"))
            {
                object P = AS.GetValue("setuppath");
                if (P != null)
                    using (StreamWriter w = new StreamWriter(Path.Combine(P.ToString(), "Startup", "user.cfg"),true))
                    {
                        w.WriteLine("showMilliseconds");
                    }                    
            }
        }

        private void miMsecs_Unchecked(object sender, RoutedEventArgs e)
        {
            using (RegistryKey AS = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\age of empires 3\\1.0"))
            {
                object P = AS.GetValue("setuppath");
                if (P != null)
                    if (File.Exists(Path.Combine(P.ToString(), "Startup", "user.cfg")))
                    {
                        List<string> S = File.ReadAllLines(Path.Combine(P.ToString(), "Startup", "user.cfg")).ToList();
                        S.Remove("showMilliseconds");
                        File.WriteAllLines(Path.Combine(P.ToString(), "Startup", "user.cfg"), S.ToArray());
                    }
            }
        }

        private void miIntro_Checked(object sender, RoutedEventArgs e)
        {
            using (RegistryKey AS = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\age of empires 3\\1.0"))
            {
                object P = AS.GetValue("setuppath");
                if (P != null)
                    using (StreamWriter w = new StreamWriter(Path.Combine(P.ToString(), "Startup", "user.cfg"), true))
                    {
                        w.WriteLine("noIntroCinematics");
                    }
            }
        }

        private void miIntro_Unchecked(object sender, RoutedEventArgs e)
        {
            using (RegistryKey AS = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\age of empires 3\\1.0"))
            {
                object P = AS.GetValue("setuppath");
                if (P != null)
                    if (File.Exists(Path.Combine(P.ToString(), "Startup", "user.cfg")))
                    {
                        List<string> S = File.ReadAllLines(Path.Combine(P.ToString(), "Startup", "user.cfg")).ToList();
                        S.Remove("noIntroCinematics");
                        File.WriteAllLines(Path.Combine(P.ToString(), "Startup", "user.cfg"), S.ToArray());
                    }
            }
        }

        private void miFPS_Checked(object sender, RoutedEventArgs e)
        {
            using (RegistryKey AS = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\age of empires 3\\1.0"))
            {
                object P = AS.GetValue("setuppath");
                if (P != null)
                    using (StreamWriter w = new StreamWriter(Path.Combine(P.ToString(), "Startup", "user.cfg"), true))
                    {
                        w.WriteLine("showFPS");
                    }
            }
        }

        private void miFPS_Unchecked(object sender, RoutedEventArgs e)
        {
            using (RegistryKey AS = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\age of empires 3\\1.0"))
            {
                object P = AS.GetValue("setuppath");
                if (P != null)
                    if (File.Exists(Path.Combine(P.ToString(), "Startup", "user.cfg")))
                    {
                        List<string> S = File.ReadAllLines(Path.Combine(P.ToString(), "Startup", "user.cfg")).ToList();
                        S.Remove("showFPS");
                        File.WriteAllLines(Path.Combine(P.ToString(), "Startup", "user.cfg"), S.ToArray());
                    }
            }
        }

        private void miRotator_Checked(object sender, RoutedEventArgs e)
        {
            using (RegistryKey AS = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\age of empires 3\\1.0"))
            {
                object P = AS.GetValue("setuppath");
                if (P != null)
                    using (StreamWriter w = new StreamWriter(Path.Combine(P.ToString(), "Startup", "user.con"), true))
                    {
                        w.WriteLine("map(\"mousez\", \"building\", \"uiWheelRotatePlacedUnit\")");                    
                    }
            }
        }

        private void miRotator_Unchecked(object sender, RoutedEventArgs e)
        {
            using (RegistryKey AS = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\age of empires 3\\1.0"))
            {
                object P = AS.GetValue("setuppath");
                if (P != null)
                    if (File.Exists(Path.Combine(P.ToString(), "Startup", "user.con")))
                    {
                        List<string> S = File.ReadAllLines(Path.Combine(P.ToString(), "Startup", "user.con")).ToList();
                        S.RemoveAt(S.FindIndex(x => x.EndsWith("uiWheelRotatePlacedUnit\")")));
                        File.WriteAllLines(Path.Combine(P.ToString(), "Startup", "user.con"), S.ToArray());
                    }
            }
        }

        private void udMax_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            using (RegistryKey AS = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\age of empires 3\\1.0"))
            {
                object P = AS.GetValue("setuppath");
                if (P != null)
                    if (File.Exists(Path.Combine(P.ToString(), "Startup", "user.cfg")))
                    {
                        List<string> S = File.ReadAllLines(Path.Combine(P.ToString(), "Startup", "user.cfg")).ToList();
                        int z1 = S.FindIndex(x => x.StartsWith("maxZoom="));
                        if (z1 >= 0)
                            S.RemoveAt(z1);
                        S.Add("maxZoom=" + udMax.Value.Value.ToString());
                        File.WriteAllLines(Path.Combine(P.ToString(), "Startup", "user.cfg"), S.ToArray());
                    }
            
            }
        }

        private void udNorm_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            using (RegistryKey AS = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\age of empires 3\\1.0"))
            {
                object P = AS.GetValue("setuppath");
                if (P != null)
                    if (File.Exists(Path.Combine(P.ToString(), "Startup", "user.cfg")))
                    {
                        List<string> S = File.ReadAllLines(Path.Combine(P.ToString(), "Startup", "user.cfg")).ToList();
                        int z2 = S.FindIndex(x => x.StartsWith("normalZoom="));
                        if (z2 >= 0)
                            S.RemoveAt(z2);
                        S.Add("normalZoom=" + udNorm.Value.Value.ToString());
                        File.WriteAllLines(Path.Combine(P.ToString(), "Startup", "user.cfg"), S.ToArray());
                    }

            }
        }

        private void udMin_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            using (RegistryKey AS = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\age of empires 3\\1.0"))
            {
                object P = AS.GetValue("setuppath");
                if (P != null)
                    if (File.Exists(Path.Combine(P.ToString(), "Startup", "user.cfg")))
                    {
                        List<string> S = File.ReadAllLines(Path.Combine(P.ToString(), "Startup", "user.cfg")).ToList();
                        int z3 = S.FindIndex(x => x.StartsWith("minZoom="));
                        if (z3>=0)
                        S.RemoveAt(z3);
                        S.Add("minZoom=" + udMin.Value.Value.ToString());
                        File.WriteAllLines(Path.Combine(P.ToString(), "Startup", "user.cfg"), S.ToArray());
                    }

            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            using (RegistryKey AS = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\age of empires 3\\1.0"))
            {
                object P = AS.GetValue("setuppath");
                if (P != null)
                    if (File.Exists(Path.Combine(P.ToString(), "Startup", "user.cfg")))
                    {
                        udMax.Value = 60;
                        udNorm.Value = 50;
                        udMin.Value = 29;
                        List<string> S = File.ReadAllLines(Path.Combine(P.ToString(), "Startup", "user.cfg")).ToList();
                        int z1 = S.FindIndex(x => x.StartsWith("maxZoom="));
                        if (z1 >= 0)
                            S.RemoveAt(z1);
                        int z2 = S.FindIndex(x => x.StartsWith("normalZoom="));
                        if (z2 >= 0)
                            S.RemoveAt(z2);
                        int z3 = S.FindIndex(x => x.StartsWith("minZoom="));
                        if (z3 >= 0)
                            S.RemoveAt(z3);
                        File.WriteAllLines(Path.Combine(P.ToString(), "Startup", "user.cfg"), S.ToArray());
                    }

            }
        }
    }
}

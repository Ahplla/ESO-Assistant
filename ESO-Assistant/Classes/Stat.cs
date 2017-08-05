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
using System.Globalization;
using System.IO;
using ESO_Assistant.Classes;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ESO_Assistant
{

    struct TELO
    {
        public double e1v1;
        public double eTeam;
        public double eOverall;
    }
    struct TWins
    {
        public int w1v1;
        public int wTeam;
        public int wOverall;
    }
    struct TWinPercent
    {
        public double wp1v1;
        public double wpTeam;
        public double wpOverall;
    }
    struct TStreak
    {
        public int s1v1;
        public int sTeam;
        public int sOverall;
    }
    struct TFavCiv
    {
        public byte fc1;
        public string p1;
        public byte fc2;
        public string p2;
        public byte fc3;
        public string p3;
    }
    struct TFavCivs
    {
        public TFavCiv fc1v1;
        public TFavCiv fcTeam;
        public TFavCiv fcOverall;
    }
    struct TGames
    {
        public int g1v1;
        public int gTeam;
        public int gOverall;
    }
    [DataContract]
    class Stat
    {
        [DataMember]
        private TGames FGames;

        private TWins FWins;
        [DataMember]
        private TWinPercent FWinPercent;
        private TStreak FStreak;
        private TStreak FMaxStreak;
        [DataMember]
        private TELO FELO;
        private TFavCivs FFavCivs;
        [DataMember]
        private TELO FMaxELO;
        [DataMember]
        private double FPR;
        [DataMember]
        private double FMaxPR;
        private string FName;
        private string FRank;
        private string FLastGame;
        private string FLastUpdate;
        private string FRankInfo;
        private int FCountPTGames;
        private int FCountNBGames;
        private int FCountWins;
        private int FRatioPT;
        private int FRatioNB;
        private int FRatioPTWins;
        private int FRatioNBWins;
        private int FLastBattles;

        private bool FERROR;

        private TGames FStoredGames;
        private TWinPercent FStoredWinPercent;
        private TELO FStoredELO;
        private TELO FStoredMaxELO;
        private double FStoredPR;
        private double FStoredMaxPR;

        public TGames StoredGames
        {
            get { return FStoredGames; }
        }

        public bool ERROR
        {
            get { return FERROR; }
        }

        public TWinPercent StoredWinPercent
        {
            get { return FStoredWinPercent; }
        }
        public TELO StoredELO
        {
            get { return FStoredELO; }
        }
        public TELO StoredMaxELO
        {
            get { return FStoredMaxELO; }
        }
        public double StoredPR
        {
            get { return FStoredPR; }
        }
        public double StoredMaxPR
        {
            get { return FStoredMaxPR; }
        }



        public TGames Games
        {
            get { return FGames; }
        }
        public TWinPercent WinPercent
        {
            get { return FWinPercent; }
        }
        public TStreak Streak
        {
            get { return FStreak; }
        }
        public TStreak MaxStreak
        {
            get { return FMaxStreak; }
        }

        public TELO ELO
        {
            get { return FELO; }
        }
        public TFavCivs FavCivs
        {
            get { return FFavCivs; }
        }
        public TELO MaxELO
        {
            get { return FMaxELO; }
        }

        public double PR
        {
            get { return FPR; }
        }

        public double MaxPR
        {
            get { return FMaxPR; }
        }
        public string Name
        {
            get { return FName; }
        }
        public string Rank
        {
            get { return FRank; }
        }
        public string LastGame
        {
            get { return FLastGame; }
        }
        public string LastUpdate
        {
            get { return FLastUpdate; }
        }
        public string RankInfo
        {
            get { return FRankInfo; }
        }



        public int CountPTGames
        {
            get { return FCountPTGames; }
        }
        public int CountNBGames
        {
            get { return FCountNBGames; }
        }
        public int CountWins
        {
            get { return FCountWins; }
        }
        public int RatioPT
        {
            get { return FRatioPT; }
        }
        public int RatioNB
        {
            get { return FRatioNB; }
        }
        public int RatioPTWins
        {
            get { return FRatioPTWins; }
        }
        public int RatioNBWins
        {
            get { return FRatioNBWins; }
        }
        public int LastBattles
        {
            get { return FLastBattles; }
        }

        private static string Pars(string T_, string _T, string Text)
        {
            int a, b;
            string Result = "";
            if (T_ == "" || Text == "" || _T == "")
                return Result;
            a = Text.IndexOf(T_);
            if (a < 0)
                return Result;
            b = Text.IndexOf(_T, a + T_.Length);
            if (b >= 0)
                Result = Text.Substring(a + T_.Length, b - a - T_.Length);
            return Result;
        }

        private string NumPars(string T_, string _T, string Text)
        {
            int a, b;
            string Result = "0";
            if (T_ == "" || Text == "" || _T == "")
                return Result;
            a = Text.IndexOf(T_);
            if (a < 0)
                return Result;
            b = Text.IndexOf(_T, a + T_.Length);
            if (b >= 0)
                Result = Text.Substring(a + T_.Length, b - a - T_.Length);
            if (Result == "")
                Result = "0";
            return Result;
        }

        private bool CheckFastResign(string Text)
        {

            bool Result = false;
            for (int i = 0; i < Constant.FAST_RESIGN_LIST.Length; i++)
                if (Text.Contains(Constant.FAST_RESIGN_LIST[i]))


                    Result = true;
            return Result;
        }


        private int CheckItemForUpdate(string CurrentItem, string KeptItem)
        {
            int Result;
            if (CurrentItem == KeptItem)
                // Добавляем Current, выходим
                Result = 0;
            else
            {
                string CurrentTime = Pars("<td class=\"ts\">", "</td>", CurrentItem);
                string KeptTime = Pars("<td class=\"ts\">", "</td>", KeptItem);
                // Добавляем Current, продолжаем
                if (DateTime.Parse(CurrentTime) > DateTime.Parse(KeptTime))
                    Result = 1;
                else
          // Добавляем Current, выходим
          if (DateTime.Parse(CurrentTime) == DateTime.Parse(KeptTime) &&
            !(CurrentItem.Contains("detail") == false) &&

            KeptItem.Contains("detail"))
                    Result = 0;
                // Добавляем Kept, выходим
                else
                    Result = 2;

            }
            return Result;
        }



        private string FormatDatesBetween(DateTime Date1, DateTime Date2)

        {
            TimeSpan span = Date1 - Date2;



            return String.Format("{0} days, {1} hours, {2} minutes",
                span.Days, span.Hours, span.Minutes);
        }


        private double CalcDelta(double X)
        {
            return -0.00000000003673 * X * X * X * X + 0.000000268594 * X * X * X -
             0.000718416 * X * X + 0.819349 * X - 321.514;
        }

        private double GetSecond(string S)
        {
            return TimeSpan.Parse(S).TotalSeconds;
        }

        private Byte SetCiv(string S)
        {
            Byte Result = 0;
            if (S == "civ-AZ")
                Result = 11;
            if (S == "civ-BR")
                Result = 2;
            if (S == "civ-CH")
                Result = 12;
            if (S == "civ-DE")
                Result = 7;
            if (S == "civ-DU")
                Result = 5;
            if (S == "civ-FR")
                Result = 3;
            if (S == "civ-IN")
                Result = 13;
            if (S == "civ-IR")
                Result = 9;
            if (S == "civ-JP")
                Result = 14;
            if (S == "civ-OT")
                Result = 8;
            if (S == "civ-PT")
                Result = 4;
            if (S == "civ-RU")
                Result = 6;
            if (S == "civ-SI")
                Result = 10;
            if (S == "civ-SP")
                Result = 1;
            return Result;
        }

        private string FormatPR(string PR)
        {
            if (int.TryParse(PR, out int Rating))
            {
                if (Rating < 3)
                    return "Conscript" + " (Level " + Rating.ToString() + ")";
                if (Rating > 2 && Rating < 8)
                    return "Private" + " (Level " + Rating.ToString() + ")";
                if (Rating > 7 && Rating < 11)
                    return "Lance Corporal" + " (Level " + Rating.ToString() + ")";
                if (Rating > 10 && Rating < 14)
                    return "Corporal" + " (Level " + Rating.ToString() + ")";
                if (Rating > 13 && Rating < 17)
                    return "Sergeant" + " (Level " + Rating.ToString() + ")";
                if (Rating > 16 && Rating < 20)
                    return "Master Sergeant" + " (Level " + Rating.ToString() + ")";
                if (Rating > 19 && Rating < 23)
                    return "2nd Lieutenant" + " (Level " + Rating.ToString() + ")";
                if (Rating > 22 && Rating < 26)
                    return "1st Lieutenant" + " (Level " + Rating.ToString() + ")";
                if (Rating > 25 && Rating < 29)
                    return "Captain" + " (Level " + Rating.ToString() + ")";
                if (Rating > 28 && Rating < 32)
                    return "Major" + " (Level " + Rating.ToString() + ")";
                if (Rating > 31 && Rating < 35)
                    return "Lieutenant Colonel" + " (Level " + Rating.ToString() + ")";
                if (Rating > 34 && Rating < 38)
                    return "Colonel" + " (Level " + Rating.ToString() + ")";
                if (Rating > 37 && Rating < 41)
                    return "Brigadier" + " (Level " + Rating.ToString() + ")";
                if (Rating > 40 && Rating < 44)
                    return "Major General" + " (Level " + Rating.ToString() + ")";
                if (Rating > 43 && Rating < 47)
                    return "Lieutenant General" + " (Level " + Rating.ToString() + ")";
                if (Rating > 46 && Rating < 50)
                    return "General" + " (Level " + Rating.ToString() + ")";
                if (Rating > 49)
                    return "Field Marshal" + " (Level " + Rating.ToString() + ")";

            }
            return PR;
        }

        public void Get(string Data, string Login, int GameTypeIndex)
        {
            try
            {
                FERROR = true;
                string GameType = "";
                switch (GameTypeIndex)
                {
                    case 0: GameType = "Supremacy"; break;
                    case 1: GameType = "Treaty"; break;
                    case 2: GameType = "Deathmatch"; break;
                    case 3: GameType = "Supremacy_nilla"; break;
                    case 4: GameType = "Deathmatch_nilla"; break;
                }
                if (Data != "" || !Data.Contains(" not found</span></div>"))
                {
                    FName = Pars("Player : ", "</span>", Data);
                    FLastUpdate = DateTime.Parse(Pars("Last updated time :", "</div>", Data)).ToLocalTime().ToString();
                    string BufFavCiv = Pars("-Overall</td>", "</tr>", Data);
                    if (BufFavCiv != "")
                    {
                        BufFavCiv = Pars("<td class=\"favciv\">", "</td>", BufFavCiv);
                        FFavCivs.fcOverall.fc1 = SetCiv(NumPars("<div class=\"", "\"/>", BufFavCiv));
                        FFavCivs.fcOverall.p1 = Pars("\"/>", "<div class=\"", BufFavCiv);
                        BufFavCiv = BufFavCiv.Remove(0, BufFavCiv.IndexOf("\"/>") + 3);
                        if (FFavCivs.fcOverall.p1 == "")
                            FFavCivs.fcOverall.p1 = BufFavCiv;
                        FFavCivs.fcOverall.fc2 = SetCiv(NumPars("<div class=\"", "\"/>", BufFavCiv));
                        FFavCivs.fcOverall.p2 = Pars("\"/>", "<div class=\"", BufFavCiv);
                        BufFavCiv = BufFavCiv.Remove(0, BufFavCiv.IndexOf("\"/>") + 3);

                        if (FFavCivs.fcOverall.p2 == "")
                            FFavCivs.fcOverall.p2 = BufFavCiv;
                        FFavCivs.fcOverall.fc3 = SetCiv(NumPars("<div class=\"", "\"/>", BufFavCiv));
                        BufFavCiv = BufFavCiv.Remove(0, BufFavCiv.IndexOf("\"/>") + 3);
                        FFavCivs.fcOverall.p3 = BufFavCiv;
                    }
                    BufFavCiv = Pars("-1v1</td>", "</tr>", Data);
                    if (BufFavCiv != "")
                    {
                        BufFavCiv = Pars("<td class=\"favciv\">", "</td>", BufFavCiv);
                        FFavCivs.fc1v1.fc1 = SetCiv(NumPars("<div class=\"", "\"/>", BufFavCiv));
                        FFavCivs.fc1v1.p1 = Pars("\"/>", "<div class=\"", BufFavCiv);
                        BufFavCiv = BufFavCiv.Remove(0, BufFavCiv.IndexOf("\"/>") + 3);

                        if (FFavCivs.fc1v1.p1 == "")
                            FFavCivs.fc1v1.p1 = BufFavCiv;
                        FFavCivs.fc1v1.fc2 = SetCiv(NumPars("<div class=\"", "\"/>", BufFavCiv));
                        FFavCivs.fc1v1.p2 = Pars("\"/>", "<div class=\"", BufFavCiv);
                        BufFavCiv = BufFavCiv.Remove(0, BufFavCiv.IndexOf("\"/>") + 3);

                        if (FFavCivs.fc1v1.p2 == "")
                            FFavCivs.fc1v1.p2 = BufFavCiv;
                        FFavCivs.fc1v1.fc3 = SetCiv(NumPars("<div class=\"", "\"/>", BufFavCiv));
                        BufFavCiv = BufFavCiv.Remove(0, BufFavCiv.IndexOf("\"/>") + 3);
                        FFavCivs.fc1v1.p3 = BufFavCiv;
                    }
                    BufFavCiv = Pars("-Team</td>", "</tr>", Data);
                    if (BufFavCiv != "")
                    {
                        BufFavCiv = Pars("<td class=\"favciv\">", "</td>", BufFavCiv);
                        FFavCivs.fcTeam.fc1 = SetCiv(NumPars("<div class=\"", "\"/>", BufFavCiv));
                        FFavCivs.fcTeam.p1 = Pars("\"/>", "<div class=\"", BufFavCiv);
                        BufFavCiv = BufFavCiv.Remove(0, BufFavCiv.IndexOf("\"/>") + 3);
                        if (FFavCivs.fcTeam.p1 == "")
                            FFavCivs.fcTeam.p1 = BufFavCiv;
                        FFavCivs.fcTeam.fc2 = SetCiv(NumPars("<div class=\"", "\"/>", BufFavCiv));
                        FFavCivs.fcTeam.p2 = Pars("\"/>", "<div class=\"", BufFavCiv);
                        BufFavCiv = BufFavCiv.Remove(0, BufFavCiv.IndexOf("\"/>") + 3);

                        if (FFavCivs.fcTeam.p2 == "")
                            FFavCivs.fcTeam.p2 = BufFavCiv;
                        FFavCivs.fcTeam.fc3 = SetCiv(NumPars("<div class=\"", "\"/>", BufFavCiv));
                        BufFavCiv = BufFavCiv.Remove(0, BufFavCiv.IndexOf("\"/>") + 3);
                        FFavCivs.fcTeam.p3 = BufFavCiv;
                    }

                    string BufELO = Pars("<td>" + GameType.Replace("_nilla", "") + "-Overall</td>", "<td class=\"favciv\">", Data);
                    if (BufELO != "")
                    {
                        FELO.eOverall = Double.Parse(NumPars("<td>", "</td>", BufELO), new CultureInfo("en-us"));
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        FStreak.sOverall = int.Parse(NumPars("<td>", "</td>", BufELO));
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        FMaxStreak.sOverall = int.Parse(NumPars("<td>", "</td>", BufELO));
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        FMaxELO.eOverall = Double.Parse(NumPars("<td>", "</td>", BufELO), new CultureInfo("en-us"));
                    }

                    BufELO = Pars("<td>" + GameType.Replace("_nilla", "") + "-1v1</td>", "<td class=\"favciv\">", Data);
                    if (BufELO != "")
                    {
                        FELO.e1v1 = Double.Parse(NumPars("<td>", "</td>", BufELO), new CultureInfo("en-us"));
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        FStreak.s1v1 = int.Parse(NumPars("<td>", "</td>", BufELO));
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        FWins.w1v1 = int.Parse(NumPars("<td>", "</td>", BufELO));
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        FGames.g1v1 = int.Parse(NumPars("<td>", "</td>", BufELO)) + FWins.w1v1;
                        if (FGames.g1v1 != 0)
                            FWinPercent.wp1v1 = Math.Round((double)FWins.w1v1 / (double)FGames.g1v1 * 100, 2);
                        else
                            FWinPercent.wp1v1 = 0;
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        FMaxStreak.s1v1 = int.Parse(NumPars("<td>", "</td>", BufELO));
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        FMaxELO.e1v1 = Double.Parse(NumPars("<td>", "</td>", BufELO), new CultureInfo("en-us"));
                    }
                    BufELO = Pars("<td>" + GameType.Replace("_nilla", "") + "-Team</td>", "<td class=\"favciv\">", Data);
                    if (BufELO != "")
                    {
                        FELO.eTeam = Double.Parse(NumPars("<td>", "</td>", BufELO), new CultureInfo("en-us"));
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        FStreak.sTeam = int.Parse(NumPars("<td>", "</td>", BufELO));
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        FWins.wTeam = int.Parse(NumPars("<td>", "</td>", BufELO));
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        FGames.gTeam = int.Parse(NumPars("<td>", "</td>", BufELO)) + FWins.wTeam;
                        if (FGames.gTeam != 0)
                            FWinPercent.wpTeam = Math.Round((double)FWins.wTeam / (double)FGames.gTeam * 100, 2);
                        else
                            FWinPercent.wpTeam = 0;
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        FMaxStreak.sTeam = int.Parse(NumPars("<td>", "</td>", BufELO));
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        FMaxELO.eTeam = Double.Parse(NumPars("<td>", "</td>", BufELO), new CultureInfo("en-us"));
                    }
                    BufELO = Pars("<td>" + GameType.Replace("_nilla", "") + "-PR</td>", "<td class=\"favciv\">", Data);
                    if (BufELO != "")
                    {
                        FPR = Double.Parse(NumPars("<td>", "</td>", BufELO), new CultureInfo("en-us"));
                        FRank = FormatPR(Math.Ceiling(FPR).ToString());
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        FWins.wOverall = int.Parse(NumPars("<td>", "</td>", BufELO));
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        FGames.gOverall = int.Parse(NumPars("<td>", "</td>", BufELO)) + FWins.wOverall;
                        if (FGames.gOverall != 0)
                            FWinPercent.wpOverall = Math.Round((double)FWins.wOverall / (double)FGames.gOverall * 100, 2);
                        else
                            FWinPercent.wpOverall = 0;
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        BufELO = BufELO.Remove(0, BufELO.IndexOf("</td>") + 5);
                        FMaxPR = Double.Parse(NumPars("<td>", "</td>", BufELO), new CultureInfo("en-us"));
                    }









                    string BufTable = Pars("<table style=\"margin-top:5px;clear:both\" class=\"alter-table\">", "</table>", Data);

                    BufTable = BufTable.Replace("<a href=\"/rating2/player?n", "<a href=\"http://aoe3.jpcommunity.com/rating2/player?n");
                    BufTable = BufTable.Replace(" class=\"pr\"", "");
                    BufTable = BufTable.Replace(" class=\"detail\"", "");
                    BufTable = BufTable.Replace(" class=\"elo\"", "");
                    BufTable = BufTable.Replace(" style=\"color:#666666\"", "");
                    FLastGame = Pars("<td class=\"ts\">", "</td>", BufTable);
                    DateTime Cur = DateTime.Now;
                    FRankInfo = "";
                    if (FPR < 25)
                        FRankInfo = "";
                    else
                  if (FLastGame != "")
                        if (FPR >= 25 && FPR < 36)
                        {
                            double Sec = (Cur - DateTime.Parse(FLastGame).ToLocalTime()).TotalSeconds;

                            DateTime Change = DateTime.Parse(FLastGame).ToLocalTime().AddSeconds(2 * 604800);
                            if (Sec > 2 * 604800)
                                FRankInfo = "PR has been decreasing for: " + FormatDatesBetween(Cur, Change);
                            else
                                FRankInfo = "Time left until PR starts to decrease: " + FormatDatesBetween(Change, Cur);
                        }
                        else
                        {
                            double Sec = (Cur - DateTime.Parse(FLastGame)).TotalSeconds;
                            DateTime Change = DateTime.Parse(FLastGame).AddSeconds(604800);
                            if (Sec > 604800)
                                FRankInfo = "PR has been decreasing for: " + FormatDatesBetween(Cur, Change);
                            else
                                FRankInfo = "Time left until PR starts to decrease: " + FormatDatesBetween(Change, Cur);
                        }

                    if (FLastGame == "")
                        FLastGame = "not played!";
                    else
                        FLastGame = FormatDatesBetween(Cur, DateTime.Parse(FLastGame).ToLocalTime());




                    /***Добавление новых записей***/


                    if (File.Exists(Path.Combine(Paths.GetPlayerDirectoryPath(FName), GameType + ".html")))
                    {

                        /***Загружаем сохраненные данные***/
                        string BufKept;
                        using (StreamReader sr = new StreamReader(Path.Combine(Paths.GetPlayerDirectoryPath(FName), GameType + ".html"), Encoding.UTF8))
                        {    // Read the stream to a string, and write the string to the console.
                            BufKept = Pars("<table>", "</table>", sr.ReadToEnd());
                        }
                        string BufCurrent = BufTable;


                        /***Копируем шапку текущей таблицы и удаляем шапки в сохраненной и текущей таблицы * **/

                        string BufHead = "<tr>" + Pars("<tr>", "</tr>", BufCurrent) + "</tr>";
                        BufKept = BufKept.Remove(BufKept.IndexOf("<tr>"), BufKept.IndexOf("</tr>") - BufKept.IndexOf("<tr>") + 4);
                        BufCurrent = BufCurrent.Remove(BufCurrent.IndexOf("<tr>"), BufCurrent.IndexOf("</tr>") - BufCurrent.IndexOf("<tr>") + 4);

                        if (BufKept != "")
                        {
                            List<string> Kept = new List<string>();
                            List<string> Current = new List<string>();
                            while (BufKept.Contains("<tr>") || BufCurrent.Contains("<tr>"))
                            {
                                if (BufKept.Contains("<tr>"))
                                {
                                    Kept.Add("<tr>" + Pars("<tr>", "</tr>", BufKept) + "</tr>");
                                    BufKept = BufKept.Remove(BufKept.IndexOf("<tr>"), BufKept.IndexOf("</tr>") - BufKept.IndexOf("<tr>") + 4);

                                }
                                if (BufCurrent.Contains("<tr>"))
                                {
                                    Current.Add("<tr>" + Pars("<tr>", "</tr>", BufCurrent) + "</tr>");
                                    BufCurrent = BufCurrent.Remove(BufCurrent.IndexOf("<tr>"), BufCurrent.IndexOf("</tr>") - BufCurrent.IndexOf("<tr>") + 4);

                                }
                            }


                            int P = 0;
                            List<string> New = new List<string>();

                            for (int i = 0; i < Kept.Count; i++)
                            {
                                // Сравниваем каждую строку Kept с другими из Current, пока не пройдем весь Current
                                if (P <= Current.Count - 1)
                                {
                                    bool exitLoop = false;
                                    for (int j = P; j < Current.Count; j++)
                                    {
                                        switch (CheckItemForUpdate(Current[j], Kept[i]))
                                        {
                                            // Добавляем Current, выходим
                                            case 0:


                                                New.Add(Current[j]);
                                                P = j + 1;
                                                exitLoop = true;
                                                break;

                                            // Добавляем Current, продолжаем
                                            case 1:

                                                New.Add(Current[j]);
                                                P = j + 1;
                                                break;

                                            // Добавляем Kept, выходим
                                            case 2:

                                                New.Add(Kept[i]);
                                                P = j + 1;
                                                break;

                                        }
                                        if (exitLoop)
                                            break;
                                    }
                                }
                                // Если текущий список кончился, то добавляем все, что осталось в Kept
                                else
                                    New.Add(Kept[i]);
                            }

                            BufTable = BufHead + string.Join(Environment.NewLine, New.ToArray()); ;
                        }
                        else
                            BufTable = BufHead + BufCurrent;
                    }

                    string BufCheat = BufTable;
                    FCountPTGames = 0;
                    FCountNBGames = 0;
                    FCountWins = 0;
                    FLastBattles = -1;

                    while (BufCheat.Contains("<tr>"))
                    {

                        string BufPT = Pars("<tr>", "</tr>", BufCheat);
                        FLastBattles++;
                        if (Pars("<td class=\"name\">", "</td>", BufPT).Contains(FName))
                        {
                            FCountWins++;

                            BufPT = BufPT.Remove(0, BufPT.IndexOf("</td>") + 5);
                            BufPT = BufPT.Remove(0, BufPT.IndexOf("</td>") + 5);
                            string BufNBW = Pars("<td>", "</td>", BufPT);
                            string BufCurrentELO = Pars("<span class=\"myname\">", "</span>", BufNBW);
                            BufNBW = BufNBW.Replace("<span class=\"myname\">", "");
                            BufNBW = BufNBW.Replace("</span>", "");
                            int NBW = 0;
                            int N = 0;

                            while (BufNBW.Contains("<p>"))
                            {


                                N++;
                                NBW += int.Parse(Pars("<p>", "</p>", BufNBW));


                                BufNBW = BufNBW.Remove(BufNBW.IndexOf("<p>"), BufNBW.IndexOf("</p>") - BufNBW.IndexOf("<p>") + 3);


                            }

                            BufPT = BufPT.Remove(0, BufPT.IndexOf("</td>") + 5);
                            BufPT = BufPT.Remove(0, BufPT.IndexOf("</td>") + 5);
                            bool FR;
                            if (GameTypeIndex == 0)
                                FR = CheckFastResign(Pars(">", "</td>", BufPT));
                            else
                                FR = false;

                            BufPT = BufPT.Remove(0, BufPT.IndexOf("</td>") + 5);
                            string BufNBL = Pars("<td>", "</td>", BufPT);
                            BufNBL = BufNBL.Replace("<span class=\"myname\">", "");
                            BufNBL = BufNBL.Replace("</span>", "");
                            int NBL = 0;

                            while (BufNBL.Contains("<p>"))
                            {


                                N++;
                                NBL += int.Parse(Pars("<p>", "</p>", BufNBL));
                                BufNBL = BufNBL.Remove(BufNBL.IndexOf("<p>"), BufNBL.IndexOf("</p>") - BufNBL.IndexOf("<p>") + 3);

                            }

                            double deltaR = (16 + (NBL - NBW) * 2 / N * 16 / 400) / 0.92;

                            double ELO = double.Parse(BufCurrentELO) - Math.Max(Math.Min(deltaR, 31), 1);
                            double delta;
                            if (ELO < 1200)
                                delta = 15;
                            else
                                delta = CalcDelta(ELO);

                            BufPT = BufPT.Remove(0, BufPT.IndexOf("</td>") + 5);
                            BufPT = BufPT.Remove(0, BufPT.IndexOf("</td>") + 5);

                            string BufTime = Pars("<td>", "</td>", BufPT);

                            if (deltaR < delta)
                                FCountNBGames++;


                            if (GameTypeIndex != 1)
                            {
                                if (GetSecond(BufTime) < 180 && !FR)
                                    FCountPTGames++;
                            }
                            else
                            {
                                if (GetSecond(BufTime) < 600)
                                    FCountPTGames++;
                            }
                        }
                        BufCheat = BufCheat.Remove(BufCheat.IndexOf("<tr>"), BufCheat.IndexOf("</tr>") - BufCheat.IndexOf("<tr>") + 4);

                    }

                    if (FCountWins == 0)
                    {
                        FRatioPT = 0;
                        FRatioNB = 0;
                        FRatioPTWins = 0;
                        FRatioNBWins = 0;
                    }
                    else
                    {
                        FRatioPT = (int)Math.Round(Math.Min((FCountPTGames / (double)FCountWins * 100.0), 20) * 5, MidpointRounding.AwayFromZero);
                        FRatioNB = (int)Math.Round(Math.Min((FCountNBGames / (double)FCountWins * 100.0), 35) * 100 / 35, MidpointRounding.AwayFromZero);
                        FRatioPTWins = (int)Math.Round(FCountPTGames / (double)FCountWins * 100.0, MidpointRounding.AwayFromZero);
                        FRatioNBWins = (int)Math.Round(FCountNBGames / (double)FCountWins * 100.0, MidpointRounding.AwayFromZero);
                    }
                    BufTable = "<html>" + "<head>" +
            "<link rel=\"stylesheet\" href=\"MyCSS.css\" type=\"text/css\">" +
           "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"/>"
            + "</head>" + "<table>" + BufTable + "</table>" + "</html>";
                    if (File.Exists(Path.Combine(Paths.GetPlayerDirectoryPath(FName), GameType + ".json")))
                    {
                        Stat StoredStat = new Stat();
                        string json = File.ReadAllText(Path.Combine(Paths.GetPlayerDirectoryPath(FName), GameType + ".json"));
                        StoredStat = JsonConvert.DeserializeObject<Stat>(json);

                        FStoredELO.eOverall = Math.Round(-StoredStat.FELO.eOverall + FELO.eOverall, 2);
                        FStoredELO.eTeam = Math.Round(-StoredStat.FELO.eTeam + FELO.eTeam, 2);
                        FStoredELO.e1v1 = Math.Round(-StoredStat.FELO.e1v1 + FELO.e1v1, 2);

                        FStoredGames.gOverall = -StoredStat.FGames.gOverall + FGames.gOverall;
                        FStoredGames.gTeam = -StoredStat.FGames.gTeam + FGames.gTeam;
                        FStoredGames.g1v1 = -StoredStat.FGames.g1v1 + FGames.g1v1;

                        FStoredMaxELO.eOverall = Math.Round(-StoredStat.FMaxELO.eOverall + FMaxELO.eOverall, 2);
                        FStoredMaxELO.eTeam = Math.Round(-StoredStat.FMaxELO.eTeam + FMaxELO.eTeam, 2);
                        FStoredMaxELO.e1v1 = Math.Round(-StoredStat.FMaxELO.e1v1 + FMaxELO.e1v1, 2);

                        FStoredPR = Math.Round(-StoredStat.FPR + FPR, 2);
                        FStoredMaxPR = Math.Round(-StoredStat.FMaxPR + FMaxPR, 2);

                        FStoredWinPercent.wpOverall = Math.Round(-StoredStat.FWinPercent.wpOverall + FWinPercent.wpOverall, 2);
                        FStoredWinPercent.wpTeam = Math.Round(-StoredStat.FWinPercent.wpTeam + FWinPercent.wpTeam, 2);
                        FStoredWinPercent.wp1v1 = Math.Round(-StoredStat.FWinPercent.wp1v1 + FWinPercent.wp1v1, 2);


                    }

                    using (StreamWriter writetext = new StreamWriter(Path.Combine(Paths.GetPlayerDirectoryPath(FName), GameType + ".html")))
                    {
                        writetext.WriteLine(BufTable);
                    }
                    File.WriteAllText(Path.Combine(Paths.GetPlayerDirectoryPath(FName), GameType + ".json"), JsonConvert.SerializeObject(this));


                    FERROR = false;


                }
            }
            catch { }

        }



        public static void GetDetail(string Data, string URL)
        {
            try
            {
                string Buf = Pars("<div id=\"TeamResults\">", "<ul id=\"footerLinks\"><li>", Data);
                Buf = Buf.Replace(" id=\"TeamResultsTable\"", "");
                Buf = Buf.Replace(" class=\"Team1\"", "");
                Buf = Buf.Replace(" class=\"Team2\"", "");
                Buf = Buf.Replace(" class=\"TeamPrompt\"", "");
                Buf = Buf.Replace(" class=\"ResultPrompt\"", "");
                Buf = Buf.Replace(
                  "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr height=\"26\"><td width=\"6\"><img src=\"/images/stats_top_left.gif\"></td><td width=\"100%\" background=\"/images/stats_top.gif\">",
                  "");
                Buf = Buf.Remove(Buf.IndexOf("<p>"), Buf.IndexOf("</p>", Buf.IndexOf("<p>")) -
                  Buf.IndexOf("<p>") + 3);
                Buf = Buf.Remove(
              Buf.IndexOf("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"CivilianPopulationChart\">"),
              Buf.IndexOf("</table>",
             Buf.IndexOf("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"CivilianPopulationChart\">")) - Buf.IndexOf("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"CivilianPopulationChart\">") + 7);
                Buf = Buf.Remove(
               Buf.IndexOf("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">"),
               Buf.IndexOf("</table>",
              Buf.IndexOf("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">")) - Buf.IndexOf("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">") + 7);
                Buf = Buf.Replace("<br />", "");
                Buf = Buf.Replace("<hr>", "");
                Buf = Buf.Replace(
                  "</td><td width=\"6\" valign=\"bottom\"><img src=\"/images/stats_top_right.gif\"></td></tr><tr><td background=\"/images/stats_left.gif\"></td><td>",
                  "");
                Buf = Buf.Replace(
                  "</td><td background=\"/images/stats_right.gif\"></td></tr><tr height=\"6\"><td><img src=\"/images/stats_bottom_left.gif\"></td><td background=\"/images/stats_bottom.gif\"></td><td><img src=\"/images/stats_bottom_right.gif\"></td></tr></table>",
                  "");
                Buf = Buf.Replace("<div id=\"stats\">", "");
                Buf = Buf.Replace(
                  "<div id=\"GameData\"><a name=\"PopulationByAge\"></a>", "");
                Buf = Buf.Replace("Ages</div>", "Ages</span>");
                Buf = Buf.Replace("Military Units</div>",
                  "Military Units</span>");
                Buf = Buf.Replace("Civilian Units</div>",
                  "Civilian Units</span>");
                Buf = Buf.Replace("<div class=\"statstab\">",
                  "<span class=\"nick\">");
                Buf = Buf.Replace("<p><a href=\"#\">Back to Top</a></p>", "");
                Buf = Buf.Remove(Buf.IndexOf("<p> <a href=\"#\">"),
              Buf.IndexOf("</a> </p>", Buf.IndexOf("<p> <a href=\"#\">")) -
              Buf.IndexOf("<p> <a href=\"#\">") + "</a> </p>".Length - 1);
                Buf = Buf.Replace(" id=\"MilitaryPopulationChart\"", "");
                Buf = Buf.Replace(" id=\"Scores\"", "");
                Buf = Buf.Replace(" id=\"CivilianPopulationChart\"", "");
                Buf = Buf.Replace(" id=\"arrayOutput\"", "");
                Buf = Buf.Replace("<a name=\"PopulationOverTime\"></a>", "");
                Buf = Buf.Replace("<a name=\"Scores\"></a>", "");
                Buf = Buf.Replace(" class=\"left-value\"", "");
                Buf = Buf.Replace(" width=\"25%\"", "");
                Buf = Buf.Replace(" width=\"100%\"", "");
                Buf = Buf.Replace(" width=\"30%\" ", "");
                Buf = Buf.Replace(" width=\"60%\" ", "");
                Buf = Buf.Replace(" width=\"19\" height=\"19\"", "");
                Buf = Buf.Replace(" height=\"38\" width=\"38\" ", "");
                Buf = Buf.Replace("<a name=\"PlayerMilUnits\"></a>", "");
                Buf = Buf.Replace("<a name=\"PlayerCivUnits\"></a>", "");
                Buf = Buf.Replace("/stats/EntityStats.aspx",
                  "http://www.agecommunity.com/stats/EnityStats.aspx");

                Buf = Buf.Replace(" class=\"row-a\"", "");
                Buf = Buf.Replace(" class=\"normal-value\"", "");
                Buf = Buf.Replace(" class=\"row-b\"", "");
                Buf = Buf.Replace(
                  " border=\"0\" cellpadding=\"2\" cellspacing=\"0\"", "");
                Buf = Buf.Replace("<div>", "");
                Buf = Buf.Replace("</div>", "");
                Buf = Buf.Replace("<img src=\"/images/Age-I.jpg\">",
                  "<div class=\"age-I\"></div>");
                Buf = Buf.Replace("<img src=\"/images/Age-II.jpg\">",
                  "<div class=\"age-II\"></div>");
                Buf = Buf.Replace("<img src=\"/images/Age-III.jpg\">",
                  "<div class=\"age-III\"></div>");
                Buf = Buf.Replace("<img src=\"/images/Age-IV.jpg\">",
                  "<div class=\"age-IV\"></div>");
                Buf = Buf.Replace("<img src=\"/images/Age-V.jpg\">",
                  "<div class=\"age-V\"></div>");
                Buf = Buf.Replace(
                  "<img src=\"/images/flags/Aztec.gif\"alt=\"Aztec Flag\" />",
                  "<div class=\"civ-AZ\"></div>");
                Buf = Buf.Replace(
                  "<img src=\"/images/flags/Russian.gif\"alt=\"Russian Flag\" />",
                  "<div class=\"civ-RU\"></div>");
                Buf = Buf.Replace(
                  "<img src=\"/images/flags/British.gif\"alt=\"British Flag\" />",
                  "<div class=\"civ-BR\"></div>");
                Buf = Buf.Replace(
                  "<img src=\"/images/flags/Chinese.gif\"alt=\"Chinese Flag\" />",
                  "<div class=\"civ-CH\"></div>");
                Buf = Buf.Replace(
                  "<img src=\"/images/flags/Portuguese.gif\"alt=\"Portuguese Flag\" />",
                  "<div class=\"civ-PT\"></div>");
                Buf = Buf.Replace(
                  "<img src=\"/images/flags/French.gif\"alt=\"French Flag\" />",
                  "<div class=\"civ-FR\"></div>");
                Buf = Buf.Replace(
                  "<img src=\"/images/flags/Dutch.gif\"alt=\"Dutch Flag\" />",
                  "<div class=\"civ-DU\"></div>");
                Buf = Buf.Replace(
                  "<img src=\"/images/flags/Japanese.gif\"alt=\"Japanese Flag\" />",
                  "<div class=\"civ-JP\"></div>");
                Buf = Buf.Replace(
                  "<img src=\"/images/flags/Ottoman.gif\"alt=\"Ottoman Flag\" />",
                  "<div class=\"civ-OT\"></div>");
                Buf = Buf.Replace(
                  "<img src=\"/images/flags/Indian.gif\"alt=\"Indian Flag\" />",
                  "<div class=\"civ-IN\"></div>");
                Buf = Buf.Replace(
                  "<img src=\"/images/flags/German.gif\"alt=\"German Flag\" />",
                  "<div class=\"civ-DE\"></div>");
                Buf = Buf.Replace(
                  "<img src=\"/images/flags/Sioux.gif\"alt=\"Sioux Flag\" />",
                  "<div class=\"civ-SI\"></div>");
                Buf = Buf.Replace(
                  "<img src=\"/images/flags/Iroquois.gif\"alt=\"Iroquois Flag\" />",
                  "<div class=\"civ-IR\"></div>");
                Buf = Buf.Replace(
                  "<img src=\"/images/flags/Spanish.gif\"alt=\"Spanish Flag\" />",
                  "<div class=\"civ-SP\"></div>");

                Buf = Buf.Replace("<span class=\"nick\">", "<div class=\"nick\">");

                Buf = Buf.Replace("Ages</span>", "Ages</div>");
                Buf = Buf.Replace("Military Units</span>",
                  "Military Units</div>");
                Buf = Buf.Replace("Civilian Units</span>",
                  "Civilian Units</div>");

                Buf = Buf.Replace(
                  "<td><span>Team 1:</span> <span>Won</span></td>",
                  "<th>Team 1: Won</th>");
                Buf = Buf.Replace(
                  "<td><span>Team 2:</span> <span>Lost</span></td>",
                  "<th>Team 2: Lost</th>");
                Buf = Buf.Replace(
                  "<thcolspan=\"4\">Units</th><thcolspan=\"2\">Buildings</th>",
                  "<th colspan=\"4\">Units</th><th colspan=\"2\">Buildings</th>");
                Buf = Buf.Replace("<div class=\"nick\">Scores",
                  "<div class=\"nick\">Scores</div>");
                Buf = Buf.Replace("<div class=\"nick\">Military",
                  "<div class=\"nick\">Military</div>");
                Buf = Buf.Replace("<div class=\"nick\">Economy",
                  "<div class=\"nick\">Economy</div>");
                Buf = Buf.Replace("<div class=\"nick\">Misc",
                  "<div class=\"nick\">Misc</div>");
                Buf = Buf.Replace("<th valign=\"top\" rowspan=\"2\">Player</th>",
                  "<th rowspan=\"2\">Player</th>");

                Buf = "<html><head>'<link rel=\"stylesheet\" href=\"Detail.css\" type=\"text/css\"><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"/></head>"
                        + Buf +

           "</html>";

                using (StreamWriter writetext = new StreamWriter(Path.Combine(Paths.GetDetailDirectoryPath(), Pars("GameID=", "&sFlag", URL) + ".html")))
                {
                    writetext.WriteLine(Buf);
                }
            }
            catch
            { }
        }



    }
}
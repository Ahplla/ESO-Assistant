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
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ESO_Assistant.Classes
{
    class CivStat
    {

        private Int64 FSpainBattle;
        private Int64 FSpainWin;
        private Int64 FGermanBattle;
        private Int64 FGermanWin;
        private Int64 FBritBattle;
        private Int64 FBritWin;
        private Int64 FFranceBattle;
        private Int64 FFranceWin;
        private Int64 FPortBattle;
        private Int64 FPortWin;
        private Int64 FRusBattle;
        private Int64 FRusWin;
        private Int64 FOttoBattle;
        private Int64 FOttoWin;
        private Int64 FDutchBattle;
        private Int64 FDutchWin;
        private Int64 FAzziBattle;
        private Int64 FAzziWin;
        private Int64 FIroBattle;
        private Int64 FIroWin;
        private Int64 FSiuBattle;
        private Int64 FSiuWin;
        private Int64 FChinaBattle;
        private Int64 FChinaWin;
        private Int64 FIndBattle;
        private Int64 FIndWin;
        private Int64 FJapBattle;
        private Int64 FJapWin;


        public ObservableCollection<ChartItem> WonChart;
        public ObservableCollection<ChartItem> UsedChart;

        public Int64 OverallGames
        {
            get
            {
                return FJapBattle + FIndBattle + FChinaBattle + FSiuBattle + FIroBattle + FAzziBattle + FDutchBattle + FOttoBattle + FRusBattle + FPortBattle + FFranceBattle +
                  FBritBattle + FGermanBattle + FSpainBattle;
            }
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



        public void GetStat(string Data)
        {
            UsedChart = new ObservableCollection<ChartItem>();
            WonChart = new ObservableCollection<ChartItem>();
            FSpainBattle = Int64.Parse(NumPars("<Spanish>", "</Spanish>", Data));
            FSpainWin = Int64.Parse(NumPars("<SpanishTotalWins>",
             "</SpanishTotalWins>", Data));

            // ---
            FGermanBattle = Int64.Parse(NumPars("<Germans>", "</Germans>", Data));
            FGermanWin = Int64.Parse(NumPars("<GermansTotalWins>",
             "</GermansTotalWins>", Data));

            // ---
            FBritBattle = Int64.Parse(NumPars("<British>", "</British>", Data));
            FBritWin = Int64.Parse(NumPars("<BritishTotalWins>",
             "</BritishTotalWins>", Data));

            // ---
            FFranceBattle = Int64.Parse(NumPars("<French>", "</French>", Data));
            FFranceWin = Int64.Parse(NumPars("<FrenchTotalWins>",
             "</FrenchTotalWins>", Data));

            // ---
            FPortBattle = Int64.Parse(NumPars("<Portuguese>", "</Portuguese>", Data));
            FPortWin = Int64.Parse(NumPars("<PortugueseTotalWins>",
             "</PortugueseTotalWins>", Data));

            // ---
            FRusBattle = Int64.Parse(NumPars("<Russians>", "</Russians>", Data));
            FRusWin = Int64.Parse(NumPars("<RussiansTotalWins>",
             "</RussiansTotalWins>", Data));


            // ---
            FOttoBattle = Int64.Parse(NumPars("<Ottomans>", "</Ottomans>", Data));
            FOttoWin = Int64.Parse(NumPars("<OttomansTotalWins>",
             "</OttomansTotalWins>", Data));

            // ---
            FDutchBattle = Int64.Parse(NumPars("<Dutch>", "</Dutch>", Data));
            FDutchWin = Int64.Parse(NumPars("<DutchTotalWins>",
             "</DutchTotalWins>", Data));

            // ---
            FAzziBattle = Int64.Parse(NumPars("<Aztec>", "</Aztec>", Data));
            FAzziWin = Int64.Parse(NumPars("<AztecTotalWins>",
             "</AztecTotalWins>", Data));

            // ---
            FIroBattle = Int64.Parse(NumPars("<Iroquois>", "</Iroquois>", Data));
            FIroWin = Int64.Parse(NumPars("<IroquoisTotalWins>",
             "</IroquoisTotalWins>", Data));


            // ---
            FSiuBattle = Int64.Parse(NumPars("<Sioux>", "</Sioux>", Data));
            FSiuWin = Int64.Parse(NumPars("<SiouxTotalWins>", "</SiouxTotalWins>", Data));


            // ---
            FChinaBattle = Int64.Parse(NumPars("<Chinese>", "</Chinese>", Data));
            FChinaWin = Int64.Parse(NumPars("<ChineseTotalWins>",
             "</ChineseTotalWins>", Data));


            // ---
            FIndBattle = Int64.Parse(NumPars("<Indian>", "</Indian>", Data));
            FIndWin = Int64.Parse(NumPars("<IndianTotalWins>",
             "</IndianTotalWins>", Data));


            // ---
            FJapBattle = Int64.Parse(NumPars("<Japanese>", "</Japanese>", Data));
            FJapWin = Int64.Parse(NumPars("<JapaneseTotalWins>",
             "</JapaneseTotalWins>", Data));

            UsedChart.Add(new ChartItem { Name = "Spanish", Value = FSpainBattle });
            WonChart.Add(new ChartItem { Name = "Spanish", Value = FSpainWin });
            UsedChart.Add(new ChartItem { Name = "British", Value = FBritBattle });
            WonChart.Add(new ChartItem { Name = "British", Value = FBritWin });
            UsedChart.Add(new ChartItem { Name = "French", Value = FFranceBattle });
            WonChart.Add(new ChartItem { Name = "French", Value = FFranceWin });
            UsedChart.Add(new ChartItem { Name = "Portuguese", Value = FPortBattle });
            WonChart.Add(new ChartItem { Name = "Portuguese", Value = FPortWin });
            UsedChart.Add(new ChartItem { Name = "Dutch", Value = FDutchBattle });
            WonChart.Add(new ChartItem { Name = "Dutch", Value = FDutchWin });
            UsedChart.Add(new ChartItem { Name = "Russians", Value = FRusBattle });
            WonChart.Add(new ChartItem { Name = "Russians", Value = FRusWin });
            UsedChart.Add(new ChartItem { Name = "Ottomans", Value = FOttoBattle });
            WonChart.Add(new ChartItem { Name = "Ottomans", Value = FOttoWin });
            UsedChart.Add(new ChartItem { Name = "Germans", Value = FGermanBattle });
            WonChart.Add(new ChartItem { Name = "Germans", Value = FGermanWin });
            UsedChart.Add(new ChartItem { Name = "Aztecs", Value = FAzziBattle });
            WonChart.Add(new ChartItem { Name = "Aztecs", Value = FAzziWin });
            UsedChart.Add(new ChartItem { Name = "Iroquois", Value = FIroBattle });
            WonChart.Add(new ChartItem { Name = "Iroquois", Value = FIroWin });
            UsedChart.Add(new ChartItem { Name = "Sioux", Value = FSiuBattle });
            WonChart.Add(new ChartItem { Name = "Sioux", Value = FSiuWin });
            UsedChart.Add(new ChartItem { Name = "Chinese", Value = FChinaBattle });
            WonChart.Add(new ChartItem { Name = "Chinese", Value = FChinaWin });
            UsedChart.Add(new ChartItem { Name = "Indians", Value = FIndBattle });
            WonChart.Add(new ChartItem { Name = "Indians", Value = FIndWin });
            UsedChart.Add(new ChartItem { Name = "Japanese", Value = FJapBattle });
            WonChart.Add(new ChartItem { Name = "Japanese", Value = FJapWin });

        }
    }
}

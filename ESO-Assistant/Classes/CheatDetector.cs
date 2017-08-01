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
using System.Collections.Generic;
using System.Diagnostics;

namespace ESO_Assistant.Classes
{
    class CheatDetector
    {


        private bool FisCheat;
        private string FNick;
        private string FUnits;

        public bool isCheat
        {
            get { return FisCheat; }
        }
        public string Nick
        { get { return FNick; } }
        public string Units
        {
            get { return FUnits; }
        }
        private string GetSpace(string S, int Count)
        {
            string Result = S;
            for (int i = 0; i < Count; i++)
                Result = Result + " ";
            return Result;
        }
        private string Pars(string T_, string _T, string Text)
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
        public void Get(string Nick, string Data)
        {
            FNick = Nick;

            FisCheat = false;
            FUnits = "";
            string Buf = Pars("<div id=\"AggregateMilitaryUnit\">",
             "<div id=\"AggregateCivilianUnit\">", Data);
            string tr;
            List<string> U = new List<string>();
            List<string> P = new List<string>();
            List<string> G = new List<string>();
            int MaxU = 0;
            int MaxP = 0;
            for (int i = 0; i < Constant.CHEAT_UNITS.Length; i++)
            {
                if (Buf.Contains(Constant.CHEAT_UNITS[i]))
                {

                    FisCheat = true;
                    tr = Pars(Constant.CHEAT_UNITS[i], "</tr>", Buf);
                    tr = tr.Remove(0, tr.IndexOf("</td>") + 5);
                    P.Add(Pars("<td>", "</td>", tr));
                    MaxP = Math.Max(MaxP, (Pars("<td>", "</td>", tr).Length));
                    tr = tr.Remove(0, tr.IndexOf("</td>") + 5);
                    tr = tr.Remove(0, tr.IndexOf("</td>") + 5);
                    tr = tr.Remove(0, tr.IndexOf("</td>") + 5);
                    tr = tr.Remove(0, tr.IndexOf("</td>") + 5);
                    G.Add(Pars("<td>", "</td>", tr));
                    U.Add(Constant.CHEAT_UNITS[i]);
                    MaxU = Math.Max(MaxU, Constant.CHEAT_UNITS[i].Length);
                }
            }
            List<string> A = new List<string>();
            if (U.Count > 0)
                for (int i = 0; i < U.Count; i++)
                    A.Add(GetSpace(U[i], MaxU - U[i].Length + 2) + "Produced: " +
                      GetSpace(P[i], MaxP - P[i].Length + 2) + "Games Used: " + G[i]);
            if (A.Count > 0)
                FUnits=string.Join(Environment.NewLine, A.ToArray());
        }
    }
}

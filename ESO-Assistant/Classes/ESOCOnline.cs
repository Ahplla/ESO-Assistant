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
using System.Collections.Generic;
using System.Net;
using System;

namespace ESO_Assistant
{
    class ESOCOnline
    {
        private int FUserOnline;
        private List<string> FUsers;
        private List<string> FActions;
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

        public ESOCOnline()
        {
            FUserOnline = 0;
            FUsers = new List<string>();
            FActions = new List<string>();
        }
        public int UserOnline
        {
            get
            { return FUserOnline; }
        }
        public List<string> Users
        {
            get
            {
                return FUsers;
            }
        }
        public List<string> Actions
        {
            get
            {
                return FActions;
            }
        }
        public void Get(string Data)
        {

            string BufTable, BufStr, b1, b2;
            BufTable = Pars("<table class=\"table1\">", "</table>", Data);
            if (BufTable == "")
                throw new Exception("Косяк в есок листе!");
            BufTable = Pars("<tbody>", "</tbody>", BufTable);

            BufTable = BufTable.Replace(" class=\"bg1\"", "");
            BufTable = BufTable.Replace(" class=\"bg2\"", "");
            BufTable = BufTable.Replace("<td colspan=\"3\">No registered users &bull; <a href=\"./viewonline.php?sg=1\">Display guests</a></td>", "");

            BufTable = WebUtility.HtmlDecode(BufTable);
            while (BufTable.Contains("<tr>"))
            {
                BufStr = Pars("<tr>", "</tr>", BufTable);
                FUsers.Add(Pars("\">", "</a>", BufStr));
                FActions.Add(Pars("\">", "</a>", Pars("<td class=\"info\">", "</td>", BufStr)));
                BufTable = BufTable.Remove(BufTable.IndexOf("<tr>"), BufTable.IndexOf("</tr>") - BufTable.IndexOf("<tr>") + 5);
            }
            if (FUsers.Count > 0)
                for (int i = 0; i < FUsers.Count - 1; i++)
                    for (int j = i + 1; j < FUsers.Count; j++)
                        if (string.Compare(FUsers[i], FUsers[j]) > 0)
                        {


                            b1 = FUsers[j];
                            FUsers[j] = FUsers[i];
                            FUsers[i] = b1;
                            b2 = FActions[j];
                            FActions[j] = FActions[i];
                            FActions[i] = b2;
                        }
            FUserOnline = FUsers.Count;




        }
    }
}

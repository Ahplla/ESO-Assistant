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
    class ClanInfo
    {

        private string FTag;
        private string FName;
        private string FOwner;
        private string FDateCreated;
        private ObservableCollection<ClanMember> FUsers = new ObservableCollection<ClanMember>();

        public string Tag
        {
            get { return FTag; }
        }
        public string Name
        {
            get { return FName; }
        }
        public string Owner
        {
            get { return FOwner; }
        }
        public string DateCreated
        {
            get { return FDateCreated; }
        }
        public ObservableCollection<ClanMember> Users
        {
            get { return FUsers; }
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
        public void GetInfo(string Data)
        {
            FTag = Pars("<abbr>", "</abbr>", Data);
            FName = Pars("<name>", "</name>", Data);
            FOwner = Pars("<owner>", "</owner>", Data);
            FDateCreated = DateTime.Parse(Pars("<created>", "</created>", Data)).ToLocalTime().ToString();
            string Buf = Pars("<users>", "</users>", Data);
            List<string> S = new List<string>();
            int i = 0;
            while (Buf.Contains("<u>"))
            {
                i++;
                //   System.Diagnostics.Debug.WriteLine(Pars("<n>", "</n>", Buf));
                FUsers.Add(new ClanMember { Name = Pars("<n>", "</n>", Buf), ID = i });
                Buf = Buf.Remove(Buf.IndexOf("<u>"), Buf.IndexOf("</u>") - Buf.IndexOf("<u>") + 3);
            }
        }
    }
}

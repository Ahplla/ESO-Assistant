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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;

namespace ESO_Assistant.Classes
{
    class Feeds
    {
        public ObservableCollection<Feed> ESOCFeed;
        private string GetTopicTitle(string s)
        {
            if (s.Contains(" • Re: "))
                return s.Substring(s.IndexOf(" • Re: ") + " • Re: ".Length);
            else
                return s.Substring(s.IndexOf(" • ") + " • ".Length);
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
        public void Get(string Data)
        {
            ESOCFeed = new ObservableCollection<Feed>();
            while (Data.Contains("<entry>"))
            {

                string Thread = WebUtility.HtmlDecode(Pars("<entry>", "</entry>", Data));
                string T = Pars("<title type=\"html\"><![CDATA[", "]]></title>", Thread);

                
                var q = ESOCFeed.Where(X => X.Title == GetTopicTitle(T)).FirstOrDefault();
                if (q == null)
                    ESOCFeed.Add(new Feed { Title = GetTopicTitle(T), Author = Pars("<author><name><![CDATA[", "]]></name></author>", Thread), Date = DateTime.Parse(Pars("<published>", "</published>", Thread)).ToLocalTime().ToString(), Link = Pars("<id>", "</id>", Thread) });              
                Data = Data.Remove(Data.IndexOf("<entry>"), Data.IndexOf("</entry>") - Data.IndexOf("<entry>") + 8);
         
            }
        }
    }
}

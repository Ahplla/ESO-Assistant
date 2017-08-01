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
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ESO_Assistant
{
    class ServerStatus
    {
        private string FLastUpdate;
        private string FESOPopulation;
        private string FTADPopulation;
        private string FTWCPopulation;
        private string FNillaPopulation;

        public static async Task<bool> checkESOAsync()
        {
            using (var client = new TcpClient())
            {
                var a = client.ConnectAsync("connection.agecommunity.com", 2300);
                await a;
                return client.Connected;
            }
        }
        public string LastUpdate
        {
            get { return FLastUpdate; }
        }
        public string ESOPopulation
        {
            get { return FESOPopulation; }
        }
        public string TADPopulation
        {
            get { return FTADPopulation; }
        }
        public string NillaPopulation
        {
            get { return FNillaPopulation; }
        }
        public string TWCPopulation
        {
            get { return FTWCPopulation; }
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
        public bool Get(string HTML)
        {
            bool Result = false;
            FLastUpdate = Pars("<span id=\"lbltime\">", "</span>", HTML);
            FLastUpdate = FLastUpdate.Replace("Jan-", "01-");
            FLastUpdate = FLastUpdate.Replace("Feb-", "02-");
            FLastUpdate = FLastUpdate.Replace("Mar-", "03-");
            FLastUpdate = FLastUpdate.Replace("Apr-", "04-");
            FLastUpdate = FLastUpdate.Replace("May-", "05-");
            FLastUpdate = FLastUpdate.Replace("Jun-", "06-");
            FLastUpdate = FLastUpdate.Replace("Jul-", "07-");
            FLastUpdate = FLastUpdate.Replace("Aug-", "08-");
            FLastUpdate = FLastUpdate.Replace("Sep-", "09-");
            FLastUpdate = FLastUpdate.Replace("Oct-", "10-");
            FLastUpdate = FLastUpdate.Replace("Nov-", "11-");
            FLastUpdate = FLastUpdate.Replace("Dec-", "12-");

            FTADPopulation = Pars("Users Online : The Asian Dynasties</td><td>", "</td>", HTML);
            FNillaPopulation = Pars("Users Online : Age3</td><td>", "</td>", HTML);
            FTWCPopulation = Pars("Users Online : War Chiefs</td><td>", "</td>", HTML);
            FESOPopulation = (Convert.ToInt32(FTWCPopulation) + Convert.ToInt32(FTADPopulation) + Convert.ToInt32(FNillaPopulation)).ToString();
            Result = true;
            return Result;

        }
    }
}

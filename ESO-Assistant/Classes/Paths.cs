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
using System.IO;
using System.Reflection;
using System.Text;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows;


namespace ESO_Assistant.Classes
{
    public class Paths
    {

        public static void OpenIPChecker()
        {

            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "IP-Checker.exe")))
            {
                Process process = new Process()
                {
                    StartInfo = new ProcessStartInfo(Path.Combine(Directory.GetCurrentDirectory(), "IP-Checker.exe"))
                    {
                        WorkingDirectory = Directory.GetCurrentDirectory()

                    }
                };

                process.Start();
            }
            else
                MessageBox.Show("IP-Checker not found!");
        }

        public static void OpenGymXP()
        {

            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Gym XP.exe")))
            {
                Process process = new Process()
                {
                    StartInfo = new ProcessStartInfo(Path.Combine(Directory.GetCurrentDirectory(), "Gym XP.exe"))
                    {
                        WorkingDirectory = Directory.GetCurrentDirectory()

                    }
                };

                process.Start();
            }
            else
                MessageBox.Show("Gym XP not found!");
        }

        public static void OpenTAD(string Name)
        {
            if (Process.GetProcessesByName(Name.Split('.')[0]).Length != 0)
                MessageBox.Show("The game is already running!");
            else
            {
                try
                {
                    using (RegistryKey AS = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\age of empires 3 expansion pack 2\\1.0"))
                    {
                        object P = AS.GetValue("setuppath");

                        if (P != null)
                        {
                            Process process = new Process()
                            {
                                StartInfo = new ProcessStartInfo(Path.Combine(P.ToString(), Name))
                                {
                                    WorkingDirectory = P.ToString()
                                }
                            };

                            process.Start();
                        }
                        else
                            MessageBox.Show("Game path not found!");
                    }
                }
                catch
                {
                    MessageBox.Show("Game path not found!");
                }
            }
        }

        public static void OpenNilla(string Name)
        {
            if (Process.GetProcessesByName(Name.Split('.')[0]).Length != 0)
                MessageBox.Show("The game is already running!");
            else
            {
                try
                {
                    using (RegistryKey AS = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\microsoft games\\age of empires 3\\1.0"))
                    {
                        object P = AS.GetValue("setuppath");

                        if (P != null)
                        {
                            Process process = new Process()
                            {
                                StartInfo = new ProcessStartInfo(Path.Combine(P.ToString(), Name))
                                {
                                    WorkingDirectory = P.ToString()
                                }
                            };

                            process.Start();
                        }
                        else
                            MessageBox.Show("Game path not found!");
                    }
                }
                catch
                {
                    MessageBox.Show("Game path not found!");
                }
            }
        }

        public static void LocalizeFile(string Path, string Nick)
        {
            string Buf;
            using (StreamReader sr = new StreamReader(Path, Encoding.UTF8))
            {    // Read the stream to a string, and write the string to the console.
                Buf = sr.ReadToEnd();
            }
            Buf = Buf.Replace("</head>", "</head><body>");
            Buf = Buf.Replace("</table>", "</table><script src = \"MyJS.js\"></script></body>");
            Buf = Buf.Replace("<table>",
             "<div class = \"filter\"><div style = \"display: none\" class = \"stat\" id=\"rstat\"><span>Games: </span><span id = \"fGames\"></span><span> | </span><span id = \"fWins\"></span><span> % | Average length: </span><span id = \"fLength\"></span></div>" +
        "<br><div class=\"stat\"><select id = \"mapId\" name = \"map\">"
        + "<option value=\"\">Select map...</option></select>"
        + "<select id = \"esoId\" name = \"eso\">"
        + "<option value=\"\">Select ESO name...</option>" +
        "</select>" +


           "<select id = \"teamId\" name = \"team\">"
        + "<option value=\"0\">Overall</option>" +
        "<option value=\"1\">1vs1</option><option value=\"2\">Team</option>"
        + "</select>" +
        "<select id = \"civId\" name = \"civ\">"
        + "<option value=\"\">Select civ...</option>" +
        "<option value=\"civ-SP\">Spanish</option>" +
        "<option value=\"civ-DE\">Germans</option>" +
       "<option value=\"civ-BR\">British</option>" +
        "<option value=\"civ-FR\">French</option>" +
        "<option value=\"civ-PT\">Portuguese</option>" +
        "<option value=\"civ-RU\">Russian</option>" +
        "<option value=\"civ-OT\">Ottomans</option>" +
        "<option value=\"civ-DU\">Dutch</option>" +
        "<option value=\"civ-AZ\">Aztecs</option>" +
        "<option value=\"civ-IR\">Iroquois</option>" +
        "<option value=\"civ-SI\">Sioux</option>" +
        "<option value=\"civ-CH\">Chinese</option>" +
        "<option value=\"civ-IN\">Indians</option>" +
        "<option value=\"civ-JP\">Japanese</option>" + "</select>"
        + "<input type=\"text\" id=\"time1Id\" placeholder=\"00:00:00\"/>"
        + "<input type=\"text\" id=\"time2Id\" placeholder=\"00:00:00\"/>"
        + "<button>Clear filter </button></div></div><table id=\"mytab\">");

            using (StreamWriter writetext = new StreamWriter(GetHTMLLocalizedFilePath(Nick)))
            {
                writetext.WriteLine(Buf);
            }
        }



        public static string GetHTMLLocalizedFilePath(string Nick)
        {
            string Result = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ESO-Assistant", "HTML", Nick, "Localized.html");
            return Result;
        }

        public static string GetPlayerDirectoryPath(string Nick)

        {
            string Result = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ESO-Assistant", "HTML", Nick);
            Directory.CreateDirectory(Result);
            try
            {
                using (FileStream fileStream = File.Create(Path.Combine(Result, "MyCSS.css")))
                {
                    Assembly.GetExecutingAssembly().GetManifestResourceStream("ESO_Assistant.HTML.MyCSS.css").CopyTo(fileStream);
                }
                using (FileStream fileStream = File.Create(Path.Combine(Result, "MyJS.js")))
                {
                    Assembly.GetExecutingAssembly().GetManifestResourceStream("ESO_Assistant.HTML.MyJS.js").CopyTo(fileStream);
                }
                using (FileStream fileStream = File.Create(Path.Combine(Result, "civs.png")))
                {
                    Assembly.GetExecutingAssembly().GetManifestResourceStream("ESO_Assistant.HTML.civs.png").CopyTo(fileStream);
                }
            }
            catch { }
            return Result;
        }

        public static string GetAppDirectoryPath()

        {
            string Result = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ESO-Assistant");
            Directory.CreateDirectory(Result);
            return Result;
        }

        public static string GetBackUpDirectoryPath()

        {
            string Result = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ESO-Assistant-BackUps");
            Directory.CreateDirectory(Result);
            return Result;
        }
    }
}

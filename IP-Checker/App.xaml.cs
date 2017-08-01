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
using Microsoft.Shell;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace IP_Checker
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class Apps : Application, ISingleInstanceApp
    {
        private const string Unique = "27b9112c-cea6-4a01-a58f-48a2a3d3f675";

        [STAThread]
       public static void Main()
        {
            if (SingleInstance<Apps>.InitializeAsFirstInstance(Unique))
            {
                var application = new Apps();
                application.InitializeComponent();
                application.Run();

                // Allow single instance code to perform cleanup operations
                SingleInstance<Apps>.Cleanup();
            }
        }

        private void ApplicationStart(object sender, StartupEventArgs e)
        {
            //Disable shutdown when the dialog closes
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            using (RegistryKey HKUninstall = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall"))
            {
                if (HKUninstall.OpenSubKey("WinPcapInst") == null)
                {
                    if (MessageBox.Show("WinPcap is required to capture live network data and currently it is not installed. Do you want to download WinPcap?", "Error - Install WinPcap?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        Process.Start("https://www.winpcap.org/install/");
                    Current.Shutdown(-1);
                }
                else
                {
                    var mainWindow = new MainWindow();
                    //Re-enable normal shutdown mode.
                    Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                    Current.MainWindow = mainWindow;
                    mainWindow.Show();
                }
            }
        }

        #region ISingleInstanceApp Members
        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            // Bring window to foreground
            if (MainWindow.WindowState == WindowState.Minimized)
            {
                MainWindow.WindowState = WindowState.Normal;
            }

            MainWindow.Activate();

            return true;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace try_2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static string[] Args { get; set; }
        public string CurrentDirectory { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            CurrentDirectory = Environment.CurrentDirectory;
            //MessageBox.Show(System.Reflection.Assembly.GetExecutingAssembly().Location);

            if (e.Args.Length <= 0)
            {

                MessageBox.Show(e.Args.Length.ToString() + Environment.NewLine + "CurrentDirectory : " + CurrentDirectory);
                return;
            }
            else
            {
                Args = e.Args;
                String info = "";
                if (Path.IsPathRooted(e.Args[0]))
                {
                    //opened from file explorer
                    info += true.ToString() + Environment.NewLine;
                    CurrentDirectory = Path.GetDirectoryName(e.Args[0]);
                }
                else
                {
                    //opened from command line
                    CurrentDirectory = Environment.CurrentDirectory;
                }

                info += CurrentDirectory + " : " + e.Args.Length.ToString() + " files imported";
                MessageBox.Show(info);

            }
        }
    }
}

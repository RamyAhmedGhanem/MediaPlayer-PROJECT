using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace try_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public static string[] Args { get; set; }//??????why not read in appstart

        public MainWindow()
        {
            InitializeComponent();
            Title = "just do it";
            if (App.Args !=null)
            {
                foreach (string arg in App.Args)
                {
                    ArgsBlock.Text += arg + Environment.NewLine;
                }
            }
            else
            {
                ArgsBlock.Text = "No Arguments";

            }



            //for registering commands
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, NewExecuted, CanNew));//Ctrl+n
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OpenExecuted, CanOpen));//Ctrl+o
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, SaveExecuted, CanSave));//Ctrl+o

            //<ListBox ScrollViewer.HorizontalScrollBarVisibility="Disabled" />
            PlayList.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty,ScrollBarVisibility.Disabled);
        }

        
        #region Command Inputs
        //For Command Inputs
        private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Sender : " + sender.ToString());
            MessageBox.Show("Original Source : " + e.OriginalSource);
        }

        private void CanNew(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("Sender : " + sender.ToString());
            //MessageBox.Show("Original Source : " + e.OriginalSource);
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Video Files|*.avi;*.mp4;*.mkv;*.flv|Audio|*.mp3;*.flac;*.wav;*.wma";
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.None);
            
            if (openFileDialog.ShowDialog() == true)
            {
                String[] fileNames = openFileDialog.FileNames;
                //String allFilesNames = "";
                PlayList.Items.Clear();
                foreach (String fileName in fileNames)
                {
                    
                    PlayList.Items.Add(System.IO.Path.GetFileName(fileName));
                    //allFilesNames += fileName + Environment.NewLine;
                }
                //MessageBox.Show(allFilesNames);
            }
            else
            {
                MessageBox.Show("False");

            }
        }
        private void CanOpen(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Sender : " + sender.ToString());
            MessageBox.Show("Original Source : " + e.OriginalSource);
        }
        private void CanSave(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        //for mouse input
        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle source = e.Source as Rectangle;

            if (source != null)
            {
                source.Fill = Brushes.SlateGray;
            }

            txt1.Text = "Mouse Entered";
        }
        #endregion


        #region Mouse Inputs
        private void OnMouseLeave(object sender, MouseEventArgs e)
        {

            // Cast the source of the event to a Button. 
            Rectangle source = e.Source as Rectangle;

            // If source is a Button. 
            if (source != null)
            {
                source.Fill = Brushes.AliceBlue;
            }

            txt1.Text = "Mouse Leave";
            txt2.Text = "";
            txt3.Text = "";
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            Point pnt = e.GetPosition(mrRec);
            txt2.Text = "Mouse Move: " + pnt.ToString();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle source = e.Source as Rectangle;
            Point pnt = e.GetPosition(mrRec);
            //Point pnt = e.GetPosition(this); postion relative to the window

            txt3.Text = "Mouse Click: " + pnt.ToString();

            if (source != null)
            {
                source.Fill = Brushes.Beige;
            }
        }
        #endregion
    }
}

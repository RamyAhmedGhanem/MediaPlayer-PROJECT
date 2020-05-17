using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
using System.ComponentModel;

namespace MediaPlayerProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread updateThread;
        public string[] FilesNames { get; set; }
        public int CurrentFileIndex { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OpenExecuted, CanOpen));//Ctrl+o

            this.Closed += OnCloingEventHandler;
            updateThread = new Thread(UpdateSeekbarAndTimeElapsed);
        }

        public void UpdateSeekbarAndTimeElapsed()
        {


            while (true)
            {
                //MessageBox.Show("Up");
                if (GetMediaState(myMediaElement) == MediaState.Stop)
                {
                    return;
                }

                if (SeekBar.Dispatcher.CheckAccess())
                {
                    MessageBox.Show("Has access");
                    if (myMediaElement.NaturalDuration.HasTimeSpan)
                    {
                        SeekBar.Value = myMediaElement.Position.TotalSeconds / myMediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                        TimeElapsed.Text = myMediaElement.Position.ToString();
                    }
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (myMediaElement.NaturalDuration.HasTimeSpan)
                        {
                            SeekBar.Maximum = myMediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                            SeekBar.Value = myMediaElement.Position.TotalSeconds;
                            TimeElapsed.Text = myMediaElement.Position.ToString();
                            TotalTime.Text = " /  " + myMediaElement.NaturalDuration.TimeSpan.ToString();
                            if (SeekBar.Value >= SeekBar.Maximum)
                            {
                                if (CurrentFileIndex < FilesNames.Length - 1)
                                {
                                    PlayList.SelectedIndex++;
                                    ChangeMediaSourceAndPlay();
                                    //MessageBox.Show("Finished");
                                }
                                else
                                {
                                    StopMedia();
                                }
                            }
                        }
                    });
                }
                Thread.Sleep(1000);
            }
        }

        private void OnCloingEventHandler(object sender, EventArgs e)
        {

            updateThread.Abort();
            //MessageBox.Show("updateThread Joined and now app is closing");
            //if(updateThread.IsAlive)
            //    updateThread.Abort();
        }

        private void CanOpen(object sender, CanExecuteRoutedEventArgs e)
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
                FilesNames = openFileDialog.FileNames;
                PlayList.Items.Clear();
                foreach (String fileName in FilesNames)
                {

                    PlayList.Items.Add(System.IO.Path.GetFileName(fileName));
                }
            }
            else
            {
                MessageBox.Show("Nothing Selected");

            }
        }




        //private void MutOrUnmute_Click(object sender, RoutedEventArgs e)
        //{
        //    Media.IsMuted = !Media.IsMuted;
        //    Media.Position = new TimeSpan(0, 1, 5);

        //    if (MutOrUnmute.Content.Equals("Mute"))
        //    {
        //        MutOrUnmute.Content = "Unmute";
        //    }
        //    else if (MutOrUnmute.Content.Equals("Unmute"))
        //    {
        //        MutOrUnmute.Content = "Mute";
        //    }
        //}



        private MediaState GetMediaState(MediaElement myMedia)
        {
            FieldInfo hlp = typeof(MediaElement).GetField("_helper", BindingFlags.NonPublic | BindingFlags.Instance);
            object helperObject = hlp.GetValue(myMedia);
            FieldInfo stateField = helperObject.GetType().GetField("_currentState", BindingFlags.NonPublic | BindingFlags.Instance);
            MediaState state = (MediaState)stateField.GetValue(helperObject);
            return state;
        }

        private void PlayOrPauseButton_Click(object sender, RoutedEventArgs e)
        {
            MediaState mediaState = GetMediaState(myMediaElement);
            //MessageBox.Show(mediaState.ToString());
            if (mediaState == MediaState.Play)
            {
                myMediaElement.Pause();
                PlayOrPauseImg.Source = new BitmapImage(new Uri(@"\images\play.png", UriKind.Relative));

                //MessageBox.Show(Media.Position.ToString());
                //Media.Position
                //Media.NaturalDuration.TimeSpan;
                //Media.HasAudio;
            }
            else if (mediaState == MediaState.Pause || mediaState == MediaState.Close || mediaState == MediaState.Stop)
            {
                myMediaElement.Play();
                while (GetMediaState(myMediaElement) != MediaState.Play) ;

                CheckAndStartThraed();

                if (updateThread.ThreadState == ThreadState.Stopped)
                {
                    //updateThread.Start();
                }
                PlayOrPauseImg.Source = new BitmapImage(new Uri(@"\images\pause.png", UriKind.Relative));


            }
            
        }

        private void CheckAndStartThraed()
        {
            if (!updateThread.IsAlive)
            {
                updateThread.Start();
            }
            else
            {
                //updateThread.Abort();
                //updateThread = new Thread(UpdateSeekbarAndTimeElapsed);
                //updateThread.Start();
            }
        }

        private void PlayList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChangeMediaSourceAndPlay();
        }

        private void ChangeMediaSourceAndPlay()
        {
            if (PlayList.SelectedIndex > -1)
            {
                //TotalTime.Text = " /  " + myMediaElement.NaturalDuration.TimeSpan.ToString();
                CurrentFileIndex =  PlayList.SelectedIndex;
                myMediaElement.Source = new Uri(FilesNames[CurrentFileIndex], UriKind.Relative);
                CheckAndStartThraed();
                myMediaElement.Play();
                PlayOrPauseImg.Source = new BitmapImage(new Uri(@"\images\pause.png", UriKind.Relative));
                Title = System.IO.Path.GetFileName(FilesNames[CurrentFileIndex]);
                //Dispatcher.Invoke(() =>
                //{
                //Thread.Sleep(2000);
                //if (myMediaElement.NaturalDuration.HasTimeSpan)
                //{
                //    SeekBar.Maximum = myMediaElement.NaturalDuration.TimeSpan.Seconds;
                //MessageBox.Show(SeekBar.Maximum.ToString());
                //}
                //});
            }
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(myMediaElement.SpeedRatio.ToString());
            myMediaElement.SpeedRatio = myMediaElement.SpeedRatio + 0.1;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StopMedia();
        }

        private void StopMedia()
        {
            myMediaElement.Stop();
            updateThread = new Thread(UpdateSeekbarAndTimeElapsed);
            PlayOrPauseImg.Source = new BitmapImage(new Uri(@"\images\play.png", UriKind.Relative));
        }

        private void BackwardButton_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.SpeedRatio = myMediaElement.SpeedRatio - 0.1;
        }

        private void PlayList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                ChangeMediaSourceAndPlay();
            }
        }

        private void SeekBar_Click(object sender, RoutedEventArgs e)
        {
            MediaState mediaState = GetMediaState(myMediaElement);
            //MessageBox.Show("myMediaElement.Position.TotalSeconds : " + myMediaElement.Position.TotalSeconds.ToString()
            //    + Environment.NewLine + "SeekBar.Value : " + SeekBar.Value.ToString());

            if (mediaState == MediaState.Play || mediaState == MediaState.Pause)
            {
                TimeSpan ts = TimeSpan.FromSeconds(SeekBar.Value);
                //MessageBox.Show(ts.ToString());
                myMediaElement.Position = ts;
                //Double MousePosition = ((MouseButtonEventArgs)(e)).GetPosition(SeekBar).X;
                //this.SeekBar.Value = MousePosition;
                //double ratio = MousePosition / SeekBar.ActualWidth;
                //double ProgressBarValue = ratio * SeekBar.Maximum;
                //double percent = SeekBar.Value * myMediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                //myMediaElement.Position = TimeSpan.FromSeconds(percent);
            }
            //MessageBox.Show(sender.ToString());
        }

        //private void SeekBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{

        //    TimeSpan ts = TimeSpan.FromSeconds(e.NewValue);
        //    myMediaElement.Position = ts;
        //}
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
        }
        //MyMediaElement myMediaElement;

        private void PlayOrPause_Click(object sender, RoutedEventArgs e)
        {

            if (PlayOrPause.Content.Equals("Play"))
            {
                PlayOrPause.Content = "Pause";
                Media.Play();
                //MessageBox.Show(Media.Position.ToString());
                //Media.Position
                //Media.NaturalDuration.TimeSpan;
                //Media.HasAudio;
            }
            else if (PlayOrPause.Content.Equals("Pause"))
            {
                PlayOrPause.Content = "Play";
                Media.Pause();
            }

        }

        private void MutOrUnmute_Click(object sender, RoutedEventArgs e)
        {
            Media.IsMuted = !Media.IsMuted;
            Media.Position = new TimeSpan(0,1,5);
            
            if (MutOrUnmute.Content.Equals("Mute"))
            {
                MutOrUnmute.Content = "Unmute";
            }
            else if (MutOrUnmute.Content.Equals("Unmute"))
            {
                MutOrUnmute.Content = "Mute";
            }
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        //[ContentProperty("MyMediaElement")]


    }
}

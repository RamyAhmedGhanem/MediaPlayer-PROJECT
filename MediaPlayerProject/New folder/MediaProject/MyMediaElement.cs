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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaProject
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MediaProject"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MediaProject;assembly=MediaProject"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:MyMediaElement/>
    ///
    /// </summary>
    public class MyMediaElement : MediaElement, INotifyPropertyChanged
    {
        public TimeSpan CurrentPosition
        {
            get
            {
                return Position;
            }
            set
            {
                Position = value;
                OnPropertyChanged();
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] String caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(caller));

            }
        }



        
        //public new TimeSpan Position
        //{
        //    get { return (TimeSpan)GetValue(PositionDependencyProperty); }
        //    set { SetValue(PositionDependencyProperty, value); }
        //}

        public static readonly DependencyProperty PositionDependencyProperty =
            DependencyProperty.Register("PositionProperty", typeof(TimeSpan),
                typeof(MyMediaElement), new PropertyMetadata(new TimeSpan(0,0,0), new PropertyChangedCallback(PositionChangedCallBack)));

        public static void PositionChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MyMediaElement myMediaElement = d as MyMediaElement;
            PositionChangedCallBack(e);
        }

        private static void PositionChangedCallBack(DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show(e.NewValue.ToString());
        }

        public MyMediaElement()
        {

            //DataContext = this;
        }
        //public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("PositionDependencyProperty", typeof(), typeof());
    }
}

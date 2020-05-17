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

namespace WPF_TRY
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl, INotifyPropertyChanged
    {
        public UserControl1()
        {
            InitializeComponent();
            //MessageBox.Show(this.DataContext.ToString());
            DataContext = this;
            //this.SetText = "Ahmed";
        }
        public Person person { get; set; }/*= new Person() { Age = 26, Name = "Ramy" };*/
        private int _counter;
        public int Counter
        {
            get
            {
                return _counter;
            }
            set
            {
                _counter = value;
                //this.DataContext = Counter;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                MessageBox.Show("Caller : " + caller);//Counter in a case
                MessageBox.Show(PropertyChanged.Method.ToString());
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(caller));

                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
            }
        }


        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }


        //Custom Dependency Properties
        //    In.NET framework, custom dependency properties can also be defined.Follow the steps given below to define custom dependency property in C#.

        //    Declare and register your dependency property with system call register.

        //    Provide the setter and getter for the property.

        //    Define a static handler which will handle any changes that occur globally

        //    Define an instance handler which will handle any changes that occur to that particular instance.
        public static readonly DependencyProperty SetTextProperty =
            DependencyProperty.Register("MyCustomProperty", typeof(String),
                typeof(UserControl1), new PropertyMetadata("begin", new PropertyChangedCallback(DependencyChangeCallBack)));
        //PropertyMetadata argument take a default value for the property as 1st argument


        // Returns:
        //     A dependency property identifier that should be used to set the value of a public static readonly
        //     field in your class. That identifier is then used to reference the dependency
        //     property later, for operations such as setting its value programmatically or
        //     obtaining metadata.
        //public static DependencyProperty Register(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata);


        //
        // Summary:
        //     Initializes a new instance of the System.Windows.PropertyMetadata class with
        //     the specified System.Windows.PropertyChangedCallback implementation reference.
        //
        // Parameters:
        //   propertyChangedCallback:
        //     Reference to a handler implementation that is to be called by the property system
        //     whenever the effective value of the property changes.
        //public PropertyMetadata(PropertyChangedCallback propertyChangedCallback);

        //
        // Summary:
        //     Represents the callback that is invoked when the effective property value of
        //     a dependency property changes.
        //
        // Parameters:
        //   d:
        //     The System.Windows.DependencyObject on which the property has changed value.
        //
        //   e:
        //     Event data that is issued by any event that tracks changes to the effective value
        //     of this property.
        //public delegate void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e);

        public string SetText
        {
            get { return (string)GetValue(SetTextProperty); }
            set { SetValue(SetTextProperty, value); }
        }

        public static void DependencyChangeCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserControl1 uc = d as UserControl1;
            uc.DependencyChangeCallBack(e);
        }
        public void DependencyChangeCallBack(DependencyPropertyChangedEventArgs e)
        {
            //MessageBox.Show(e.Property);

            MessageBox.Show(SetText);
            MessageBox.Show(TBtn.Content?.ToString());
            //TBtn.Content = e.NewValue.ToString();
            //MessageBox.Show(SetText);

        }

        private void TBtn_Click(object sender, RoutedEventArgs e)
        {
            //this.SetValue(SetTextProperty,"Ios");
            //SetText = "IOS";
            Counter++;
            MessageBox.Show(Counter.ToString());

        }








        //
        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("StackPanel_Click");
            e.Handled = true;//to stop bubbling
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button");
        }
        //





        //propdb proerty dependency code snippt


        //public int MyProperty
        //{
        //    get { return (int)GetValue(MyPropertyProperty); }
        //    set { SetValue(MyPropertyProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MyPropertyProperty =
        //    DependencyProperty.Register("MyProperty", typeof(int), typeof(ownerclass), new PropertyMetadata(0));


    }

}

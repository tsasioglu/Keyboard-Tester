using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace KeyboardTester
{
    /// <summary>
    /// Interaction logic for KeyboardKey.xaml
    /// </summary>
    public partial class KeyboardKey
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(KeyboardKey), new PropertyMetadata(String.Empty));

        
        public int PaddingOverride
        {
            get { return (int)GetValue(PaddingOverrideProperty); }
            set { SetValue(PaddingOverrideProperty, value); }
        }

        public static readonly DependencyProperty PaddingOverrideProperty =
            DependencyProperty.Register("PaddingOverride", typeof(int), typeof(KeyboardKey), new PropertyMetadata(0));
        

        public bool DefaultPadding
        {
            get { return (bool)GetValue(DefaultPaddingProperty); }
            set { SetValue(DefaultPaddingProperty, value); }
        }

        public static readonly DependencyProperty DefaultPaddingProperty =
            DependencyProperty.Register("DefaultPadding", typeof(bool), typeof(KeyboardKey), new PropertyMetadata(true));


        public KeyboardKeyModel ViewModel { get; set; }
        
        public KeyboardKey()
        {
            InitializeComponent();
            DataContext = this;
            ViewModel = new KeyboardKeyModel();
        }
      
    }

    public class KeyboardKeyModel : INotifyPropertyChanged
    {
        public string Text { get; set; }

        private bool _pressed;
        public bool Pressed
        {
            get { return _pressed; }
            set
            {
                _pressed = value;
                OnPropertyChanged();
            }
        }

        private bool _previouslyPressed;
        public bool PreviouslyPressed
        {
            get { return _previouslyPressed; }
            set
            {
                _previouslyPressed = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System.Reflection;
using System.Windows.Input;

namespace KeyboardTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            var keyControl = GetKeyControl(e);
            if (keyControl == null)
                return;

            keyControl.ViewModel.Pressed = true;
            keyControl.ViewModel.PreviouslyPressed = true;

            if (keyControl == Return)
            {
                ReturnLower.ViewModel.Pressed = true;
                ReturnLower.ViewModel.PreviouslyPressed = true;
            }

            e.Handled = true;
        }
        
        private void MainWindow_OnKeyUp(object sender, KeyEventArgs e)
        {
            var keyControl = GetKeyControl(e);
            if (keyControl == null)
                return;

            keyControl.ViewModel.Pressed = false;

            if (keyControl == Return)
                ReturnLower.ViewModel.Pressed = false;

            if (keyControl == Snapshot)
                Snapshot.ViewModel.PreviouslyPressed = true;

            e.Handled = true;
        }

        private KeyboardKey GetKeyControl(KeyEventArgs keyEvent)
        {
            if (keyEvent.Key == Key.LeftCtrl && IsModifier(ModifierKeys.Control) && IsNotModifier(ModifierKeys.Alt))
            {
                if (keyEvent.IsRepeat)
                    return null;
                return LeftCtrl;
            }

            if (keyEvent.Key == Key.Enter)
            {
                if(ReadIsExteneded(keyEvent))
                    return NumPadEnter;
                return Return;
            }
            
            if (keyEvent.Key == Key.System)
            {
                if (IsModifier(ModifierKeys.Alt))
                    return LeftAlt;
                if (keyEvent.SystemKey == Key.F10)
                    return F10;
                if (keyEvent.SystemKey == Key.F11)
                    return F11;
                if (keyEvent.SystemKey == Key.F12)
                    return F12;
                if (ReadRealKey(keyEvent) == Key.LeftAlt)
                    return LeftAlt;
                if (ReadRealKey(keyEvent) == Key.RightAlt)
                    return RightAlt;
            }

            var keyControl = GetKeyControlNonStandard(keyEvent);

            if (keyControl == null)
                keyControl = FindName(keyEvent.Key.ToString()) as KeyboardKey;

            return keyControl;
        }

        private static bool ReadIsExteneded(KeyEventArgs keyEvent)
        {
            return (bool)typeof(KeyEventArgs).InvokeMember("IsExtendedKey", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance, null, keyEvent, null); 
        }

        private static Key ReadRealKey(KeyEventArgs keyEvent)
        {
            return (Key)typeof(KeyEventArgs).InvokeMember("RealKey", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance, null, keyEvent, null);
        }

        private static bool IsModifier(ModifierKeys key)
        {
            return (Keyboard.Modifiers & key) == key;
        }

        private static bool IsNotModifier(ModifierKeys key)
        {
            return (Keyboard.Modifiers & key) != key;
        }

        private KeyboardKey GetKeyControlNonStandard(KeyEventArgs keyEvent)
        {
            switch (keyEvent.Key)
            {
                case Key.Oem1:
                    return OemSemicolon;
                case Key.Oem4:
                    return OemOpenBrackets;
                case Key.Oem5:
                    return OemPipe;
                case Key.Oem6:
                    return OemCloseBrackets;
                case Key.Oem7:
                    return OemQuotes;
                default:
                    return null;
            }
        }
    }
}

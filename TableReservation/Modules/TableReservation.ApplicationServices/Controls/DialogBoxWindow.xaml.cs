using Microsoft.Practices.Prism.Commands;
using System.Windows;
using System.Windows.Controls;
using TableReservation.ApplicationServices.DialogBox;

namespace TableReservation.ApplicationServices.Controls
{
    /// <summary>
    /// Interaction logic for DialogBoxWindow.xaml
    /// </summary>
    public partial class DialogBoxWindow : Window
    {
        public DialogBoxWindow()
        {
            InitializeComponent();
        }

       

        public static readonly DependencyProperty DialogTitleProperty = DependencyProperty.Register("DialogTitle", typeof(string), typeof(DialogBoxWindow));
        public string DialogTitle
        {
            get
            {
                return this.GetValue(DialogTitleProperty) as string;
            }
            set
            {
                this.SetValue(DialogTitleProperty, value);
            }
        }

        public static readonly DependencyProperty DialogButtonsProperty = DependencyProperty.Register("DialogButtons", typeof(DialogBoxButtons), typeof(DialogBoxWindow));
        public DialogBoxButtons DialogButtons
        {
            get
            {
                return (DialogBoxButtons)this.GetValue(DialogButtonsProperty);
            }
            set
            {
                this.SetValue(DialogButtonsProperty, value);
            }
        }

        public static readonly DependencyProperty ContentControlProperty = DependencyProperty.Register("Content", typeof(string), typeof(DialogBoxWindow));
        public UserControl ContentControl
        {
            get
            {
                return this.GetValue(ContentControlProperty) as UserControl;
            }
            set
            {
                this.SetValue(ContentControlProperty, value);
            }
        }

        public static readonly DependencyProperty ResultProperty = DependencyProperty.Register("Result", typeof(DialogBoxResult), typeof(DialogBoxWindow));
        public DialogBoxResult Result
        {
            get
            {
                return (DialogBoxResult)this.GetValue(ResultProperty);
            }
            set
            {
                this.SetValue(ResultProperty, value);
            }
        }

        public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register("CancelCommand", typeof(DelegateCommand), typeof(DialogBoxWindow));
        public DelegateCommand CancelCommand
        {
            get
            {
                return (DelegateCommand)this.GetValue(CancelCommandProperty);
            }
            set
            {
                this.SetValue(CancelCommandProperty, value);
            }
        }

        private void OnCancelCommand()
        {
            SystemCommands.CloseWindow(this);
        }
    }
}

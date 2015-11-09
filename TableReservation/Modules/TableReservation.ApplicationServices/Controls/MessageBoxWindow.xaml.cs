using Microsoft.Practices.Prism.Commands;
using System.Windows;
using TableReservation.ApplicationServices.DialogBox;
using TableReservation.ApplicationServices.MessageBox;

namespace TableReservation.ApplicationServices.Controls
{
    /// <summary>
    /// Interaction logic for MessageBoxWindow.xaml
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        public static readonly DependencyProperty MessageTitleProperty = DependencyProperty.Register("MessageTitle", typeof(string), typeof(MessageBoxWindow));
        public string MessageTitle
        {
            get
            {
                return this.GetValue(MessageTitleProperty) as string;
            }
            set
            {
                this.SetValue(MessageTitleProperty, value);
            }
        }

        public static readonly DependencyProperty MessageTextProperty = DependencyProperty.Register("MessageText", typeof(string), typeof(MessageBoxWindow));
        public string MessageText
        {
            get
            {
                return this.GetValue(MessageTextProperty) as string;
            }
            set
            {
                this.SetValue(MessageTextProperty, value);
            }
        }

        public static readonly DependencyProperty ResultProperty = DependencyProperty.Register("Result", typeof(MessageBoxResults), typeof(MessageBoxWindow));
        public MessageBoxResults Result
        {
            get
            {
                return (MessageBoxResults)this.GetValue(ResultProperty);
            }
            set
            {
                this.SetValue(ResultProperty, value);
            }
        }

        public static readonly DependencyProperty MessageButtonsProperty = DependencyProperty.Register("MessageButtons", typeof(MessageBoxButtons), typeof(MessageBoxWindow));
        public MessageBoxButtons MessageButtons
        {
            get
            {
                return (MessageBoxButtons)this.GetValue(MessageButtonsProperty);
            }
            set
            {
                this.SetValue(MessageButtonsProperty, value);
            }
        }

        public static readonly DependencyProperty MessageIconProperty = DependencyProperty.Register("MessageIcon", typeof(MessageBoxIcon), typeof(MessageBoxWindow));
        public MessageBoxIcon MessageIcon
        {
            get
            {
                return (MessageBoxIcon)this.GetValue(MessageIconProperty);
            }
            set
            {
                this.SetValue(MessageIconProperty, value);
            }
        }

        public static readonly DependencyProperty YesCommandProperty = DependencyProperty.Register("YesCommand", typeof(DelegateCommand), typeof(MessageBoxWindow));
        public DelegateCommand YesCommand
        {
            get
            {
                return (DelegateCommand)this.GetValue(YesCommandProperty);
            }
            set
            {
                this.SetValue(YesCommandProperty, value);
            }
        }

        public static readonly DependencyProperty NoCommandProperty = DependencyProperty.Register("NoCommand", typeof(DelegateCommand), typeof(MessageBoxWindow));
        public DelegateCommand NoCommand
        {
            get
            {
                return (DelegateCommand)this.GetValue(NoCommandProperty);
            }
            set
            {
                this.SetValue(NoCommandProperty, value);
            }
        }

        public static readonly DependencyProperty OKCommandProperty = DependencyProperty.Register("OKCommand", typeof(DelegateCommand), typeof(MessageBoxWindow));
        public DelegateCommand OKCommand
        {
            get
            {
                return (DelegateCommand)this.GetValue(OKCommandProperty);
            }
            set
            {
                this.SetValue(OKCommandProperty, value);
            }
        }

        public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register("CancelCommand", typeof(DelegateCommand), typeof(MessageBoxWindow));
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

        public static readonly DependencyProperty CloseWindowCommandProperty = DependencyProperty.Register("CloseWindowCommand", typeof(DelegateCommand), typeof(MessageBoxWindow));
        public DelegateCommand CloseWindowCommand
        {
            get
            {
                return (DelegateCommand)this.GetValue(CloseWindowCommandProperty);
            }
            set
            {
                this.SetValue(CloseWindowCommandProperty, value);
            }
        }

        public MessageBoxWindow()
        {
            InitializeComponent();

            this.YesCommand = new DelegateCommand(OnYesCommand);
            this.NoCommand = new DelegateCommand(OnNoCommand);
            this.OKCommand = new DelegateCommand(OnOKCommand);
            this.CancelCommand = new DelegateCommand(OnCancelCommand);
            this.CloseWindowCommand = new DelegateCommand(OnCloseWindowCommand);

            if (Application.Current.MainWindow.IsLoaded)
            {
                this.Owner = Application.Current.MainWindow;
            }

            this.DataContext = this;
        }

        private void OnYesCommand()
        {
            this.Result = MessageBoxResults.Yes;
            SystemCommands.CloseWindow(this);
        }

        private void OnNoCommand()
        {
            this.Result = MessageBoxResults.No;
            SystemCommands.CloseWindow(this);
        }

        private void OnOKCommand()
        {
            this.Result = MessageBoxResults.OK;
            SystemCommands.CloseWindow(this);
        }

        private void OnCancelCommand()
        {
            this.Result = MessageBoxResults.Cancel;
            SystemCommands.CloseWindow(this);
        }

        private void OnCloseWindowCommand()
        {
            this.Result = MessageBoxResults.Cancel;
            SystemCommands.CloseWindow(this);
        }
    }
}

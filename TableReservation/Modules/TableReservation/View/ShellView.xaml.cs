using Microsoft.Practices.Unity;
using System.Windows.Controls;
using TableReservation.Common.View;
using TableReservation.Common.ViewModel;

namespace TableReservation.View
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : UserControl, IShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        [Dependency]
        public IShellViewModel ViewModel
        {
            get 
            {
                return this.DataContext as IShellViewModel;
            }

            set
            {
                this.DataContext = value;
            }
        }
    }
}

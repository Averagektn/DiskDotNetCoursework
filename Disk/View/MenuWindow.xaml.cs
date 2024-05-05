using Disk.View;
using System.Windows;

namespace Disk
{
    /// <summary>
    ///     Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Windows.OfType<DoctorAuthenticationWindow>().First().Show();
            base.OnClosed(e);
        }
    }
}

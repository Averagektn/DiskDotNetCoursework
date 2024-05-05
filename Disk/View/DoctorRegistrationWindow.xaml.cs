using Disk.ViewModel;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Disk.View
{
    /// <summary>
    /// Interaction logic for DoctorRegistrationWindow.xaml
    /// </summary>
    public partial class DoctorRegistrationWindow : Window
    {
        private readonly DoctorRegistrationViewModel _viewModel = new();

        public DoctorRegistrationWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(PasswordBox.Password.Trim());
            if (inputBytes.Length < 8)
            {
                await _viewModel.ShowPopup("Пароль должен быть длиннее 8 символов");
            }

            byte[] hashBytes = SHA512.HashData(inputBytes);

            var builder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("X2"));
            }

            await _viewModel.PerformRegistration(builder.ToString());
        }

        private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.Hide();
            new DoctorAuthorizationWindow().ShowDialog();
            Application.Current.MainWindow.Show();
        }
    }
}

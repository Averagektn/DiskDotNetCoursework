using Disk.ViewModel;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Disk.View
{
    public partial class DoctorRegistrationWindow : Window
    {
        private readonly DoctorRegistrationViewModel _viewModel = new();

        public DoctorRegistrationWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private async Task<string> GetHashString()
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

            return builder.ToString();
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            var passwordHash = await GetHashString();

            await _viewModel.PerformAuthorization(passwordHash);
        }

        private async void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            var passwordHash = await GetHashString();

            await _viewModel.PerformRegistration(passwordHash);
        }
    }
}

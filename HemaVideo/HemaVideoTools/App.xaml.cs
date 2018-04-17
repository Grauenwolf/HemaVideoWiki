using HemaVideoTools.Properties;
using System;
using System.Windows;

namespace HemaVideoTools
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			LoginViewModel loginViewModel = new LoginViewModel()
			{
				Url = "http://hemavideos.azurewebsites.net",
				EmailAddress = Settings.Default.EmailAddress,
				Password = Settings.Default.Password
			};
			var dialog = new LoginDialog() { DataContext = loginViewModel };

			dialog.ShowDialog();

			if (!loginViewModel.IsLoggedIn)
			{
				Shutdown();
				return;
			}

			Settings.Default.EmailAddress = loginViewModel.EmailAddress;
			Settings.Default.Password = loginViewModel.Password;
			Settings.Default.Save();

			var args = AppDomain.CurrentDomain.SetupInformation.ActivationArguments;

			var window = new MainWindow() { DataContext = new MainViewModel(loginViewModel.ApiClient, args) };
			window.Show();
		}
	}
}
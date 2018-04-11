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

			LoginViewModel loginViewModel = new LoginViewModel() { Url = "http://hemavideos.azurewebsites.net" };
			var dialog = new LoginDialog() { DataContext = loginViewModel };

			dialog.ShowDialog();

			if (!loginViewModel.IsLoggedIn)
			{
				Shutdown();
				return;
			}
			var window = new MainWindow() { DataContext = new MainViewModel(loginViewModel.ApiClient) };
			window.Show();
		}
	}
}
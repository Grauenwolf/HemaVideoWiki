using HemaVideoTools.Services;
using System.Net.Http;
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

			var client = new Client("http://hemavideos.azurewebsites.net", new HttpClient());

			LoginViewModel loginViewModel = new LoginViewModel(client);
			var dialog = new LoginDialog() { DataContext = loginViewModel };

			dialog.ShowDialog();

			if (!loginViewModel.IsLoggedIn)
			{
				this.Shutdown();
				return;
			}
			var window = new MainWindow() { DataContext = new MainViewModel(client) };
			window.Show();
		}
	}
}
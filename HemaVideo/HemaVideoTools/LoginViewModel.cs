using HemaVideoTools.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Tortuga.Sails;

namespace HemaVideoTools
{
	public class LoginViewModel : ViewModelBase
	{
		public Client ApiClient { get => Get<Client>(); set => Set(value); }

		public string EmailAddress { get => Get<String>(); set => Set(value); }
		public string Url { get => Get<String>(); set => Set(value); }
		public string Password { get => Get<String>(); set => Set(value); }
		public bool IsLoggedIn { get => Get<bool>(); set => Set(value); }

		public ICommand LoginCommand => GetCommand<LoginDialog>(async (dialog) => await Login(dialog));

		public async Task Login(LoginDialog dialog)
		{
			var apiClient = new Client(Url, new HttpClient());
			var result = await apiClient.ApiAccountLoginPostAsync(EmailAddress, Password, false);
			if (result)
			{
				IsLoggedIn = true;
				ApiClient = apiClient;
				dialog.Close();
			}
		}
	}
}
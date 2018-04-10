using HemaVideoTools.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Tortuga.Sails;

namespace HemaVideoTools
{
	public class LoginViewModel : ViewModelBase
	{
		private readonly Client m_ApiClient;

		public LoginViewModel(Client apiClient)
		{
			m_ApiClient = apiClient;
		}

		public string EmailAddress { get => Get<String>(); set => Set(value); }
		public string Password { get => Get<String>(); set => Set(value); }
		public bool IsLoggedIn { get => Get<bool>(); set => Set(value); }

		public ICommand LoginCommand => GetCommand<LoginDialog>(async (dialog) => await Login(dialog));

		public async Task Login(LoginDialog dialog)
		{
			var result = await m_ApiClient.ApiAccountLoginPostAsync(EmailAddress, Password, false);
			if (result)
			{
				IsLoggedIn = true;
				dialog.Close();
			}
		}
	}
}
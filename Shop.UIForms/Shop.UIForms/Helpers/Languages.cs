namespace Shop.UIForms.Helpers
{
	using Interfaces;
	using Resources;
	using Xamarin.Forms;

	public static class Languages
	{
		static Languages()
		{
			var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
			Resource.Culture = ci;
			DependencyService.Get<ILocalize>().SetLocale(ci);
		}

		public static string Accept => Resource.Accept;

		public static string Error => Resource.Error;

		public static string EmailMessage => Resource.EmailError;

		public static string PasswordError => Resource.PasswordError;

		public static string LoginError => Resource.LoginError;

		public static string Login => Resource.Login;

		public static string EmailMEmailessage => Resource.Email;

		public static string EmailPlaceHolder => Resource.EmailPlaceholder;

		public static string Password => Resource.Password;

		public static string PasswordPlaceHolder => Resource.PasswordPlaceholder;

		//public static string PasswordMessage => Resource.PasswordMessage;

		public static string Rememberme => Resource.Rememberme;

		//public static string EmailOrPasswordIncorrect => Resource.EmailOrPasswordIncorrect;

	}

}

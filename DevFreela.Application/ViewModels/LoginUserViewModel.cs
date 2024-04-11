namespace DevFreela.Application.ViewModels
{
    public class LoginUserViewModel
    {

        public LoginUserViewModel(string username, string bearerToken, DateTime expirateIn)
        {
            UserName = username;
            BearerToken = bearerToken;
            ExpirateIn = expirateIn;

        }

        public string UserName { get; set; }
        public string BearerToken { get; set; }
        public DateTime ExpirateIn { get; set; }

    }
}

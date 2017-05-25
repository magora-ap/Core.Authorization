namespace Core.Authorization.Bll.Models.Helpers
{
    public class SiteAuthModel
    {
        private string _email;

        public string Email
        {
            get => _email;
            set => _email = value?.ToLowerInvariant();
        }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}

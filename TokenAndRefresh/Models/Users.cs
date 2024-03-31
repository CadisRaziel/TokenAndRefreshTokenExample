namespace TokenAndRefresh.Models
{
    public partial class Users
    {
        public int IdUser { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public virtual ICollection<HistoryRefreshTokens> HistoryRefreshTokens { get; } = new List<HistoryRefreshTokens>();
    }
}

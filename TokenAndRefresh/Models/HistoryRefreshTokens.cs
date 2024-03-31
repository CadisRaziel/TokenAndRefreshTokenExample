namespace TokenAndRefresh.Models
{
    public partial class HistoryRefreshTokens
    {
        public int IdHistoryRefreshToken { get; set; }

        public int? IdUser { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? DateCreation { get; set; }

        public DateTime? DateExpiration { get; set; }

        public bool? isActive { get; set; }

        public virtual Users IdUserNavigation { get; set; }
    }
}

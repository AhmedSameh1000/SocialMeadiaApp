namespace SocialApi.Helper
{
    public class MessageParams
    {
        private const int maxpagesize = 50;
        public int pageNumber { get; set; } = 1;
        private int pagesize = 10;
        public string? UserId { get; set; }
        public int PageSize
        {
            get { return pagesize; }
            set { pagesize = value > maxpagesize ? maxpagesize : value; }
        }
        public string MessageType { get; set; } = "Unread";
    }
}

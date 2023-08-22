namespace SocialApi.Helper
{
    public class UserParams
    {
        public int pageNumber { get; set; } = 1;
		private const int maxpagesize = 50;
		private int pagesize=10;
        public string?	UserId { get; set; }
		public bool likees { get; set; } = false;
		public bool likers { get; set; } = false;
        public int PageSize
		{
			get { return pagesize; }
			set { pagesize = value>maxpagesize?maxpagesize:value; }
		}

	}

}

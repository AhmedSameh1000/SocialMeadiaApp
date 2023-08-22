namespace SocialApi.Helper
{
    public class PaginationHeader
    {
        public PaginationHeader(int currentPage, int itemPerPage, int totalItems, int totalPages)
        {
           this.CurrentPage = currentPage;
           this.ItemPerPage = itemPerPage;
           this.TotalItems = totalItems;
           this.TotalPages = totalPages;
        }

        public int CurrentPage { get; set; }
        public int ItemPerPage { get; set; }
        public int TotalItems { get; set; }  
        public int TotalPages { get; set; }


    }
}


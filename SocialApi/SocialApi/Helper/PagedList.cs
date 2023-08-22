using Microsoft.EntityFrameworkCore;
namespace SocialApi.Helper
{
    public class PagedList<T>:List<T>
    {
        public PagedList(List<T> Items,int count,int pageNumber,int pageSize)
        {
            TotlaCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count/(double)pageSize);
            this.AddRange(Items);
        }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotlaCount { get; set; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> Source,
            int pagenumber,int pagesize)
        {
           var Count=await Source.CountAsync();
            var Items=await Source.Skip((pagenumber-1)*pagesize).Take(pagesize).ToListAsync();
            return new PagedList<T>(Items,Count,pagenumber,pagesize);
        }
    }

}

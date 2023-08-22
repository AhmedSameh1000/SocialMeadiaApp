using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SocialApi.Helper;

namespace ZwajApp.api.Extensions;

public static class Extensions
{
    public static int CalculateAge(this DateTime Date)
    {
        var Age = DateTime.Today.Year - Date.Year;
        if (Date.AddYears(Age) > DateTime.Today) 
        {
            Age--;
        }
        return Age;

    }

    public static void AddPagination(this HttpResponse response , int Currentpage,
        int ItemPerPage,int totalItems,int totalpages)
    {
        var paginationHeader=new PaginationHeader(Currentpage,ItemPerPage,totalItems,totalpages);

        var camelcaseformater = new JsonSerializerSettings();
        camelcaseformater.ContractResolver = new CamelCasePropertyNamesContractResolver();
        response.Headers.Add("pagination", JsonConvert.SerializeObject(paginationHeader,camelcaseformater));
        response.Headers.Add("Access-Control-Expose-Headers", "pagination");       
    }
}

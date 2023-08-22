namespace ZwajApp.api.ViewModels;

public class PhotoForReturnDto
{
    public int id { get; set; }
    public string? UserId { get; set; }
    public string? Url { get; set; }
    public bool isMain { get; set; }
}
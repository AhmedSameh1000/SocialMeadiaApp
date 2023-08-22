using ZwajApp.api.Models;
using ZwajApp.api.ViewModels;

namespace ZwajApp.api.IRepository
{
    public interface IPhotoRepository
    {
        Task<Photo> UploadImageProfile(PhotoForDetailsDto photo);

        Task DeleteImage(int id, string UserId);

        Task<PhotoForReturnDto> ImgProfile(string UserId);
        Task<List<PhotoForReturnDto>> AllPhotos(string UserId);
        Task Deleteimage(int id);
        Task SetImageProfile(int id,string UserId);

    }
}
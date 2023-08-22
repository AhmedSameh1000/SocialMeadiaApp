using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ZwajApp.api.Data;
using ZwajApp.api.IRepository;
using ZwajApp.api.Models;
using ZwajApp.api.ViewModels;

namespace ZwajApp.api.RepositoryEmplmentation
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IWebHostEnvironment _host;
        private readonly HttpContext _httpContext;

        public PhotoRepository(AppDbContext applicationDbContext, IWebHostEnvironment host, IHttpContextAccessor httpContextAccessor)
        {
            appDbContext = applicationDbContext;
            _host = host;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<List<PhotoForReturnDto>> AllPhotos(string UserId)
        {
           var Photos=await appDbContext.Photos.Where(f=>f.UserId == UserId).Select(c=>new PhotoForReturnDto
           {
               id = c.Id,
               UserId = UserId,
               isMain=c.IsMain,
                Url=c.Url,
           }).ToListAsync();
           return  Photos;
        }

        public async Task DeleteImage(int id, string UserId)
        {
            var ImgProfile = await appDbContext.Photos.FirstOrDefaultAsync(f => f.Id == id && f.UserId == UserId);
            appDbContext.Remove(ImgProfile);
            appDbContext.SaveChanges();
        }

        public async Task Deleteimage(int id)
        {
            var image = await appDbContext.Photos.FindAsync(id);
            if (image is null)
                return;
            string RootPath = _host.WebRootPath.Replace("\\\\", "\\");
            var imageNameToDelete = System.IO.Path.GetFileNameWithoutExtension(image.Url);
            var EXT = Path.GetExtension(image.Url);
            var oldImagePath = $@"{RootPath}\imagesprofile\{imageNameToDelete}{EXT}";
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            appDbContext.Remove(image);
            appDbContext.SaveChanges();
        }

    

        public async Task<PhotoForReturnDto> ImgProfile(string UserId)
        {
            var ImgProfile = await appDbContext.Photos.FirstOrDefaultAsync(f => f.IsMain && f.UserId == UserId);
            if (ImgProfile is null)
            {
                return new PhotoForReturnDto()
                {
                    Url = @"https://localhost:7268/imagesprofile/Empty.jpg",
                };
            }

            var Img = new PhotoForReturnDto()
            {
                id = ImgProfile.Id,
                Url = ImgProfile.Url,
                UserId = ImgProfile.UserId
            };
            return Img;
        }

        public async Task SetImageProfile(int id, string UserId)
        {
            var newImageProfile = await appDbContext.Photos.FindAsync(id);
            if (newImageProfile is null)
                return;
            var CurrentImageProfile = await appDbContext.Photos.FirstOrDefaultAsync(p => p.IsMain&&p.UserId==UserId);
            if(CurrentImageProfile is null)
            {
                newImageProfile.IsMain = true;
                appDbContext.SaveChanges();
            }
            else
            {
                CurrentImageProfile.IsMain = false;
                newImageProfile.IsMain = true;
                appDbContext.SaveChanges();
            }

        }

        public async Task<Photo> UploadImageProfile(PhotoForDetailsDto photo)
        {          
            var CurrentImageProfile = appDbContext.Photos.FirstOrDefault(f => f.IsMain&&f.UserId==photo.UserId);
            if (CurrentImageProfile != null)
            {
                CurrentImageProfile.IsMain = false;
            }
            string RootPath = _host.WebRootPath;
            var ImageUrl = "";
            string fileName = Guid.NewGuid().ToString();
            string imageFolderPath = Path.Combine(RootPath, @"imagesprofile");
            string extension = Path.GetExtension(photo.Img.FileName);
            using (FileStream fileStreams = new(Path.Combine(imageFolderPath,
                               fileName + extension), FileMode.Create))
            {
                photo.Img.CopyTo(fileStreams);
            }
            ImageUrl = @$"{_httpContext.Request.Scheme}://{_httpContext.Request.Host}/imagesprofile/" + fileName + extension;
            Photo NewPhoto = new()
            {
                IsMain = true,
                Url = ImageUrl,
                UserId = photo.UserId,
            };

            appDbContext.Photos.Add(NewPhoto);
            appDbContext.SaveChanges();
            return NewPhoto;
        }
    }
}
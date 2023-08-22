using AutoMapper;
using SocialApi.Models;
using SocialApi.ViewModels;
using ZwajApp.api.Extensions;
using ZwajApp.api.IRepository;
using ZwajApp.api.Models;
using ZwajApp.api.ViewModels;

namespace ZwajApp.api.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            _ = CreateMap<User, UserForListDto>()
               .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(c => c.IsMain).Url))
                .ForMember(dest => dest.Age, opt => { opt.MapFrom(src => src.DataOfBirth.Value.CalculateAge()); });

            _ = CreateMap<User, UserForDetailsDto>()
               .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(c => c.IsMain).Url)).
               ForMember(dest => dest.Age, opt => { opt.MapFrom(src => src.DataOfBirth.Value.CalculateAge()); })
               .ForMember(dest => dest.Interestes, opt => { opt.MapFrom(src => src.Interestes); });
            _ = CreateMap<Photo, PhotoForReturnDto>();

            CreateMap<MessageForCreationViewModel, Message>().ReverseMap();

            CreateMap<Message, MessageToReturnDto>()
                .ForMember(dest => dest.senderPhotoUrl, opt => opt.MapFrom(src => src.sender.Photos.FirstOrDefault(c => c.IsMain).Url))
                .ForMember(dest => dest.recipientPhotoUrl, opt => opt.MapFrom(src => src.recipient.Photos.FirstOrDefault(c => c.IsMain).Url));

        }
    }
}
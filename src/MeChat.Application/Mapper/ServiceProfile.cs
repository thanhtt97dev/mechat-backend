using AutoMapper;
using MeChat.Common.Shared.Response;
using MeChat.Domain.Entities;

namespace MeChat.Application.Mapper;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        #region V1

        #region User
        CreateMap<User, Common.UseCases.V1.User.Command.AddUser>().ReverseMap();
        CreateMap<User, Common.UseCases.V1.User.Command.UpdateUser>().ReverseMap();
        CreateMap<User, Common.UseCases.V1.User.Response.User>().ReverseMap();
        CreateMap<PageResult<User>, PageResult<Common.UseCases.V1.User.Response.User>>().ReverseMap();
        CreateMap<User, Common.UseCases.V1.User.Response.UserPublicInfo>()
            .ForMember(des => des.Friends, atc => atc.MapFrom(src => new List<Common.UseCases.V1.User.Response.UserPublicInfo>()))
            .ReverseMap();
        #endregion

        #region Auth
        CreateMap<User, Common.UseCases.V1.Auth.Response.UserInfo>()
            .ForMember(des => des.UserId, atc => atc.MapFrom(src => src.Id))
            .ReverseMap();
        #endregion

        #region Notification
        CreateMap<Notification, Common.UseCases.V1.Notification.Response.Notification>()
            .ForMember(des => des.RequesterName, atc => atc.MapFrom(src => src.Requester!.Fullname == null ? string.Empty : src.Requester!.Fullname))
            .ForMember(des => des.Image, atc => atc.MapFrom(src => src.Requester!.Avatar == null ? string.Empty : src.Requester!.Avatar))
            .ReverseMap();
        #endregion

        #endregion

        #region V2

        #endregion
    }
}

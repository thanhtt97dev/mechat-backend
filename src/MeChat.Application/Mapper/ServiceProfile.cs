using AutoMapper;
using MeChat.Common.Shared.Constants;
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
        CreateMap<User, Common.UseCases.V1.User.Response.UserPublicInfo>().ReverseMap();

        #endregion

        #region Auth
        CreateMap<User, Common.UseCases.V1.Auth.Response.UserInfo>()
            .ForMember(des => des.UserId, atc => atc.MapFrom(src => src.Id))
            .ReverseMap();
        #endregion

        #endregion

        #region V2

        #endregion
    }
}

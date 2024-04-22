using AutoMapper;
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
        #endregion

        #endregion

        #region V2

        #endregion
    }
}

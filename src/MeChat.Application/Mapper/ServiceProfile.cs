using AutoMapper;
using MeChat.Domain.Entities;

namespace MeChat.Application.Mapper;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        #region V1
        CreateMap<User, Common.UseCases.V1.User.Command.AddUser>().ReverseMap();
        CreateMap<User, Common.UseCases.V1.User.Command.UpdateUser>().ReverseMap();
        #endregion

        #region V2

        #endregion
    }
}

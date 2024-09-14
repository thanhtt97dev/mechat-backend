using Google.Apis.Auth;
using MeChat.Application.UseCases.V1.Auth.Utils;
using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages.DomainEvents;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Shared.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;
using MeChat.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace MeChat.Application.UseCases.V1.Auth.QueryHandlers;
public class SignInByGoogleQueryHandler : IQueryHandler<Query.SignInByGoogle, Response.Authenticated>
{
    private readonly IConfiguration configuration;
    private readonly Common.Abstractions.Data.Dapper.IUnitOfWork unitOfWorkDapper;
    private readonly Common.Abstractions.Data.EntityFramework.IUnitOfWork unitOfWorkEF;
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;
    private readonly IRepository<UserSocial> userSocialRepository;
    private readonly AuthUtil authUtil;

    public SignInByGoogleQueryHandler(
        IConfiguration configuration, 
        Common.Abstractions.Data.Dapper.IUnitOfWork unitOfWorkDapper, 
        Common.Abstractions.Data.EntityFramework.IUnitOfWork unitOfWorkEF, 
        IRepositoryBase<Domain.Entities.User, Guid> userRepository, 
        IRepository<UserSocial> userSocialRepository,
        AuthUtil authUtil)
    {
        this.configuration = configuration;
        this.unitOfWorkDapper = unitOfWorkDapper;
        this.unitOfWorkEF = unitOfWorkEF;
        this.userRepository = userRepository;
        this.userSocialRepository = userSocialRepository;
        this.authUtil = authUtil;
    }

    public async Task<Result<Response.Authenticated>> Handle(Query.SignInByGoogle request, CancellationToken cancellationToken)
    {
        //Check Google token
        GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(request.GoogleToken);
        if(payload == null) 
            return Result.UnAuthentication<Response.Authenticated>("Invalid google token!");

        //Check user's email existed
        var user = await unitOfWorkDapper.Users.GetUserByAccountSocial(payload.Subject, AppConstants.Social.Google);
        if(user != null)
        {
            return await authUtil.GenerateToken(user);
        }
            
        //New User
        Domain.Entities.User newUser = new Domain.Entities.User
        {
            Username = null,
            Password = null,
            Fullname = payload.Name,
            RoleId = AppConstants.Role.User,
            Email = payload.Email,
            Avatar = payload.Picture,
            Status = AppConstants.User.Status.Activate,
        };
        userRepository.Add(newUser);
        await unitOfWorkEF.SaveChangeAsync();

        //New UserSocial
        UserSocial userSocial = new UserSocial
        {
            UserId = newUser.Id,
            SocialId = AppConstants.Social.Google,
            AccountSocialId = payload.Subject,
        };
        userSocialRepository.Add(userSocial);
        await unitOfWorkEF.SaveChangeAsync();

        return await authUtil.GenerateToken(newUser);
    }
}

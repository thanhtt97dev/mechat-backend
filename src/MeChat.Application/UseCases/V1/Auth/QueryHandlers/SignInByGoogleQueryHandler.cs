using Google.Apis.Auth;
using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;
using MeChat.Infrastucture.Dapper.Repositories;
using MeChat.Infrastucture.Jwt.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace MeChat.Application.UseCases.V1.Auth.QueryHandlers;
public class SignInByGoogleQueryHandler : IQueryHandler<Query.SignInByGoogle, Response.Authenticated>
{
    private readonly IConfiguration configuration;
    private readonly Common.Abstractions.Data.Dapper.IUnitOfWork unitOfWorkDapper;
    private readonly Common.Abstractions.Data.EntityFramework.IUnitOfWork unitOfWorkEF;
    private readonly IJwtTokenService jwtTokenService;
    private readonly ICacheService cacheService;
    private readonly IRepositoryEnitityBase<Domain.Entities.User, Guid> userRepository;
    private readonly IRepositoryBase<Domain.Entities.UserSocial> userSocialRepository;

    public SignInByGoogleQueryHandler(IConfiguration configuration, Common.Abstractions.Data.Dapper.IUnitOfWork unitOfWorkDapper,
        Common.Abstractions.Data.EntityFramework.IUnitOfWork unitOfWorkEF, IJwtTokenService jwtTokenService, ICacheService cacheService, 
        IRepositoryEnitityBase<Domain.Entities.User, Guid> userRepository, IRepositoryBase<Domain.Entities.UserSocial> userSocialRepository)
    {
        this.configuration = configuration;
        this.unitOfWorkDapper = unitOfWorkDapper;
        this.unitOfWorkEF = unitOfWorkEF;
        this.jwtTokenService = jwtTokenService;
        this.cacheService = cacheService;
        this.userRepository = userRepository;
        this.userSocialRepository = userSocialRepository;
    }

    public async Task<Result<Response.Authenticated>> Handle(Query.SignInByGoogle request, CancellationToken cancellationToken)
    {
        //Check Google token
        GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(request.GoogleToken);
        if(payload == null) 
            return Result.Failure<Response.Authenticated>(null, "Invalid google token!");

        //Check user's email existed
        var user = await unitOfWorkDapper.Users.GetUserByAccountSocial(payload.Subject, SocialConstants.Google);
        if(user != null)
        {
            return await SignIn(user.Id, user.RoldeId, user.Email);
        }
            
        //New User
        Domain.Entities.User newUser = new Domain.Entities.User
        {
            Username = null,
            Password = null,
            Fullname = payload.Name,
            RoldeId = RoleConstant.User,
            Email = payload.Email,
            Avatar = payload.Picture,
            Status = UserConstant.Status.Activate,
            DateCreated = DateTime.Now,
            DateUpdated = DateTime.Now,
        };
        userRepository.Add(newUser);
        await unitOfWorkEF.SaveChangeAsync();

        //New UserSocial
        Domain.Entities.UserSocial userSocial = new Domain.Entities.UserSocial
        {
            UserId = newUser.Id,
            SocialId = SocialConstants.Google,
            AccountSocialId = payload.Subject,
            DateCreated = DateTime.Now,
            DateUpdated = DateTime.Now,
        };
        userSocialRepository.Add(userSocial);
        await unitOfWorkEF.SaveChangeAsync();

        return await SignIn(newUser.Id, newUser.RoldeId, newUser.Email);
    }

    private async Task<Result<Response.Authenticated>> SignIn(Guid id, int role, string? email)
    {
        JwtOption jwtOption = new();
        configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);
        var sessionTime = jwtOption.ExpireMinute + jwtOption.RefreshTokenExpireMinute;

        var refreshToken = jwtTokenService.GenerateRefreshToken();

        var clamims = new List<Claim>
        {
            new Claim(AppConfiguration.Jwt.ID, id.ToString()),
            new Claim(AppConfiguration.Jwt.ROLE, role.ToString()),
            new Claim(AppConfiguration.Jwt.EMAIL, email??string.Empty),
            new Claim(AppConfiguration.Jwt.JTI, refreshToken),
            new Claim(AppConfiguration.Jwt.EXPIRED, DateTime.Now.AddMinutes(jwtOption.ExpireMinute).ToString()),
        };

        var accessToken = jwtTokenService.GenerateAccessToken(clamims);

        var result = new Response.Authenticated
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTime.Now.AddMinutes(sessionTime),
            UserId = id.ToString(),
        };

        //save refresh token into cache
        await cacheService.SetCache(refreshToken, id.ToString(), TimeSpan.FromMinutes(sessionTime));

        return Result.Success(result);
    }
}

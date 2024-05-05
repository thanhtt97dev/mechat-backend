using Google.Apis.Auth;
using MeChat.Common.Abstractions.Data.Dapper;
using MeChat.Common.Abstractions.Data.EntityFramework;
using MeChat.Common.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;
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
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;

    public SignInByGoogleQueryHandler(IConfiguration configuration, Common.Abstractions.Data.Dapper.IUnitOfWork unitOfWorkDapper,
        Common.Abstractions.Data.EntityFramework.IUnitOfWork unitOfWorkEF, IJwtTokenService jwtTokenService, ICacheService cacheService, 
        IRepositoryBase<Domain.Entities.User, Guid> userRepository)
    {
        this.configuration = configuration;
        this.unitOfWorkDapper = unitOfWorkDapper;
        this.unitOfWorkEF = unitOfWorkEF;
        this.jwtTokenService = jwtTokenService;
        this.cacheService = cacheService;
        this.userRepository = userRepository;
    }

    public async Task<Result<Response.Authenticated>> Handle(Query.SignInByGoogle request, CancellationToken cancellationToken)
    {
        //Check Google token
        GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(request.GoogleToken);
        if(payload == null) 
            return Result.Failure<Response.Authenticated>(null, "Invalid google token!");

        //Check user's email existed
        var user = await unitOfWorkDapper.Users.GetUserByEmail(payload.Email);
        if(user != null)
            return await SignIn(user.Id, user.RoldeId, user.Email);

        //New user
        Domain.Entities.User newUser = new Domain.Entities.User
        {
            Username = null,
            Password = null,
            RoldeId = RoleConstant.User,
            Email = payload.Email,
            Avatar = null,
            OAuth2Status = UserConstant.OAuth2.Google,
            Status = UserConstant.Status.Activate,
            DateCreated = DateTime.Now,
            DateUpdated = DateTime.Now,
        };

        userRepository.Add(newUser);
        await unitOfWorkEF.SaveChangeAsync();

        return await SignIn(newUser.Id, newUser.RoldeId, newUser.Email);
    }

    private async Task<Result<Response.Authenticated>> SignIn(Guid id, int role, string? email)
    {
        var clamims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, id.ToString()),
            new Claim(ClaimTypes.Role, role.ToString()),
            new Claim(ClaimTypes.Email, email??string.Empty),
        };

        var accessToken = jwtTokenService.GenerateAccessToken(clamims);
        var refreshToken = jwtTokenService.GenerateRefreshToken();

        JwtOption jwtOption = new();
        configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);

        var sessionTime = jwtOption.ExpireMinute + jwtOption.RefreshTokenExpireMinute;

        var result = new Response.Authenticated
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTime.Now.AddMinutes(sessionTime)
        };

        //save refresh token into cache
        await cacheService.SetCache(refreshToken, id.ToString(), TimeSpan.FromMinutes(sessionTime));

        return Result.Success(result);
    }
}

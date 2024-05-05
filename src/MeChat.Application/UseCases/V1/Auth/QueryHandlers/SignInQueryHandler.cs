using MeChat.Common.Abstractions.Data.Dapper;
using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Constants;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;
using MeChat.Infrastucture.Jwt.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace MeChat.Application.UseCases.V1.Auth.QueryHandlers;
public class SignInQueryHandler : IQueryHandler<Query.SignIn, Response.Authenticated>
{
    private readonly IConfiguration configuration;
    private readonly IUnitOfWork unitOfWork;
    private readonly IJwtTokenService jwtTokenService;
    private readonly ICacheService cacheService;

    public SignInQueryHandler(IConfiguration configuration, IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, ICacheService cacheService)
    {
        this.configuration = configuration;
        this.unitOfWork = unitOfWork;
        this.jwtTokenService = jwtTokenService;
        this.cacheService = cacheService;
    }

    public async Task<Result<Response.Authenticated>> Handle(Query.SignIn request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetUserByUsernameAndPassword(request.Username, request.Password);
        if (user == null)
            return Result.Failure<Response.Authenticated>(null, "Username or Password incorrect!");

        //Check User
        if (user.Status != Common.Constants.UserConstant.Status.Activate)
            return Result.Initialization<Response.Authenticated>(ResponseCodes.UserBanned, "User has been banned!", false, null);

        var clamims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.RoldeId.ToString()),
            new Claim(ClaimTypes.Email, user.Email??string.Empty),
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
        await cacheService.SetCache(refreshToken, user.Id.ToString(), TimeSpan.FromMinutes(sessionTime));

        return Result.Success(result);
    }
}

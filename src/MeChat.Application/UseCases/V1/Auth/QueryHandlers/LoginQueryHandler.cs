using MeChat.Common.Abstractions.Data.Dapper;
using MeChat.Common.Abstractions.Messages;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Shared.Response;
using MeChat.Common.UseCases.V1.Auth;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace MeChat.Application.UseCases.V1.Auth.QueryHandlers;
public class LoginQueryHandler : IQueryHandler<Query.Login, Response.Authenticated>
{
    private readonly IConfiguration configuration;
    private readonly IUnitOfWork unitOfWork;
    private readonly IJwtTokenService jwtTokenService;
    private readonly ICacheService cacheService;

    public LoginQueryHandler(IConfiguration configuration, IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, ICacheService cacheService)
    {
        this.configuration = configuration;
        this.unitOfWork = unitOfWork;
        this.jwtTokenService = jwtTokenService;
        this.cacheService = cacheService;
    }

    public async Task<Result<Response.Authenticated>> Handle(Query.Login request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetUserByUsernameAndPassword(request.Username, request.Password);
        if (user == null)
            return Result.Failure<Response.Authenticated>(null, "Username or Password incorrect!");

        var clamims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.Role, "Admin"),
        };

        var accessToken = jwtTokenService.GenerateAccessToken(clamims);
        var refreshToken = jwtTokenService.GenerateRefreshToken();
        var refreshTokenExpiryTime = int.Parse(configuration["JwtOption:ExpireMinute"] ?? "0");

        var result = new Response.Authenticated
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenExpiryTime)
        };

        //save refresh token into cache
        //await cacheService.SetCache(user.Id.ToString(), refreshToken, TimeSpan.FromMinutes(refreshTokenExpiryTime));
        await cacheService.SetCache(user.Id.ToString(), refreshToken, TimeSpan.FromMinutes(3));

        return Result.Success(result);
    }
}

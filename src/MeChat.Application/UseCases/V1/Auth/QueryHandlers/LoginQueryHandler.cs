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
    private readonly IUnitOfWork unitOfWork;
    private readonly IJwtTokenService jwtTokenService;
    private readonly IConfiguration configuration;
    public LoginQueryHandler(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, IConfiguration configuration)
    {
        this.unitOfWork = unitOfWork;
        this.jwtTokenService = jwtTokenService;
        this.configuration = configuration;
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

        var result = new Response.Authenticated
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTime.Now.AddSeconds(1100)
        };
        return Result.Success(result);
    }
}

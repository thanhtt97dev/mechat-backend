using MeChat.Application.UseCases.V1.Auth.QueryHandlers;
using MeChat.Application.UseCases.V1.Auth.Utils;
using MeChat.Common.Abstractions.Data.Dapper;
using MeChat.Common.Abstractions.Data.Dapper.Repositories;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Shared.Constants;
using MeChat.Common.UseCases.V1.Auth;
using Microsoft.Extensions.Configuration;
using Moq;
using static MeChat.Common.Shared.Constants.AppConstants;
using System.Security.Claims;

namespace MeChat.BusinessLogic.Tests;

public class SignInQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> unitOfWorkMock;
    private readonly Mock<IUserRepository> userRepoMock;
    private readonly Mock<ICacheService> cacheServiceMock;
    private readonly Mock<IJwtService> jwtServiceMock;
    private readonly Mock<IConfiguration> configurationMock;

    private readonly SignInQueryHandler handler;

    public SignInQueryHandlerTests()
    {
        unitOfWorkMock = new Mock<IUnitOfWork>();
        userRepoMock = new Mock<IUserRepository>();
        cacheServiceMock = new Mock<ICacheService>();
        jwtServiceMock = new Mock<IJwtService>();
        configurationMock = new Mock<IConfiguration>();

        unitOfWorkMock.Setup(u => u.Users).Returns(userRepoMock.Object);

        var configValues = new Dictionary<string, string?>
        {
            ["Jwt:ExpireMinute"] = "15",
            ["Jwt:RefreshTokenExpireMinute"] = "30"
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configValues)
            .Build();

        configurationMock.Setup(c => c.GetSection("Jwt")).Returns(configuration.GetSection("Jwt"));

        var authUtil = new AuthUtil(configurationMock.Object, cacheServiceMock.Object, jwtServiceMock.Object);
        handler = new SignInQueryHandler(unitOfWorkMock.Object, authUtil);
    }

    [Fact]
    public async Task Handle_UserNotFound_ReturnsNotFound()
    {
        // Arrange
        var query = new Query.SignIn("testuser", "wrongpass");

        userRepoMock
                .Setup(r => r.GetUserByUsernameAndPassword(query.Username, query.Password))
                .ReturnsAsync((Domain.Entities.User)null!);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.Code == AppConstants.ResponseCodes.NotFound);
    }

    [Fact]
    public async Task Handle_ReturnsAuthenticatedResult_WhenCredentialsAreValid()
    {
        // Arrange
        var user = new Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            Password = "password",
            Email = "test@example.com",
            RoleId = 1,
            Fullname = "Test User",
            Avatar = "avatar.png",
            Status = AppConstants.User.Status.Activate
        };

        unitOfWorkMock.Setup(u => u.Users.GetUserByUsernameAndPassword("testuser", "password"))
                      .ReturnsAsync(user);

        jwtServiceMock.Setup(j => j.GenerateRefreshToken()).Returns("refreshtoken123");
        jwtServiceMock.Setup(j => j.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>()))
                      .Returns("accesstoken123");

        cacheServiceMock.Setup(c => c.SetCache("refreshtoken123", user.Id.ToString(), It.IsAny<TimeSpan>()))
        .Returns(Task.CompletedTask);

        var authUtil = new AuthUtil(configurationMock.Object, cacheServiceMock.Object, jwtServiceMock.Object);
        var handler = new SignInQueryHandler(unitOfWorkMock.Object, authUtil);

        var query = new Query.SignIn("testuser", "password");

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.Code == AppConstants.ResponseCodes.Success);
    }
}
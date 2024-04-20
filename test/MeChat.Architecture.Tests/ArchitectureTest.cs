using FluentAssertions;
using NetArchTest.Rules;

namespace MeChat.Architecture.Tests;
public class ArchitectureTest
{
    private const string DOMAIN = "MeChat";
    private const string API_NAMESPACE = $"{DOMAIN}.API";
    private const string APPLICATION_NAMESPACE = $"{DOMAIN}.Application";
    private const string DOMAIN_NAMESPACE = $"{DOMAIN}.Domain";
    private const string INFRASTUCTRE_Dapper_NAMESPACE = $"{DOMAIN}.Infrastucture.Dapper";
    private const string INFRASTUCTRE_JWT_NAMESPACE = $"{DOMAIN}.Infrastucture.Jwt";
    private const string INFRASTUCTRE_REDIS_NAMESPACE = $"{DOMAIN}.Infrastucture.Redis";
    private const string PERSISTENCE_NAMESPACE = $"{DOMAIN}.Persistence";
    private const string PRESENTATION_NAMESPACE = $"{DOMAIN}.Presentation";

    [Fact]
    public void DomainShouldNotHaveDependencyWithOthrProject()
    {
        // Arrage
        var assembly = Domain.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
        API_NAMESPACE,
        APPLICATION_NAMESPACE,
        INFRASTUCTRE_Dapper_NAMESPACE,
        INFRASTUCTRE_JWT_NAMESPACE,
        INFRASTUCTRE_REDIS_NAMESPACE,
        PERSISTENCE_NAMESPACE,
        PRESENTATION_NAMESPACE
    };

        // Act
        var testResult = Types.InAssembly(assembly).ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
        INFRASTUCTRE_Dapper_NAMESPACE,
        INFRASTUCTRE_JWT_NAMESPACE,
        INFRASTUCTRE_REDIS_NAMESPACE,
        // PersistenceNamespace, // Due to Implement sort multi columns by apply RawQuery with EntityFramework
        PERSISTENCE_NAMESPACE,
        API_NAMESPACE
    };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void InfrastructureDapperShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Infrastucture.Dapper.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            PRESENTATION_NAMESPACE,
            API_NAMESPACE
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void InfrastructureRedisShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Infrastucture.Dapper.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            PRESENTATION_NAMESPACE,
            API_NAMESPACE
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void InfrastructureJwtShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Infrastucture.Dapper.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            PRESENTATION_NAMESPACE,
            API_NAMESPACE
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }


    [Fact]
    public void PersistenceShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Persistence.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
        APPLICATION_NAMESPACE,
        INFRASTUCTRE_Dapper_NAMESPACE,
        INFRASTUCTRE_JWT_NAMESPACE,
        INFRASTUCTRE_REDIS_NAMESPACE,
        PRESENTATION_NAMESPACE,
        API_NAMESPACE
    };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void PresentationShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Presentation.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
        INFRASTUCTRE_Dapper_NAMESPACE,
        INFRASTUCTRE_JWT_NAMESPACE,
        INFRASTUCTRE_REDIS_NAMESPACE,
        APPLICATION_NAMESPACE
    };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
}
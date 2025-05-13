using FluentAssertions;
using NetArchTest.Rules;

namespace MeChat.Architecture.Tests;
public class ArchitectureTest
{
    #region Define namespaces
    private const string DOMAIN = "MeChat";
    private const string API_NAMESPACE = $"{DOMAIN}.API";
    private const string APPLICATION_NAMESPACE = $"{DOMAIN}.Application";
    private const string COMMON_NAMESPACE = $"{DOMAIN}.Common";
    private const string DOMAIN_NAMESPACE = $"{DOMAIN}.Domain";
    private const string INFRASTUCTRE_DAPPER_NAMESPACE = $"{DOMAIN}.Infrastucture.Dapper";
    private const string INFRASTUCTRE_DISTRIBUTED_CACHE_NAMESPACE = $"{DOMAIN}.Infrastucture.DistributedCache";
    private const string INFRASTUCTRE_MESSAGEBROKER_CONSUMER_EMAIL_NAMESPACE = $"{DOMAIN}.Infrastucture.MessageBroker.Consumer.Email";
    private const string INFRASTUCTRE_MESSAGEBROKER_PRODUCER_EMAIL_NAMESPACE = $"{DOMAIN}.Infrastucture.MessageBroker.Producer.Email";
    private const string INFRASTUCTRE_REALTIME_NAMESPACE = $"{DOMAIN}.Infrastucture.RealTime";
    private const string INFRASTUCTRE_SERVICE_NAMESPACE = $"{DOMAIN}.Infrastucture.Service";
    private const string INFRASTUCTRE_STORAGE_NAMESPACE = $"{DOMAIN}.Infrastucture.Storage";
    private const string PERSISTENCE_NAMESPACE = $"{DOMAIN}.Persistence";
    private const string PRESENTATION_NAMESPACE = $"{DOMAIN}.Presentation";
    #endregion

    #region ApplicationShouldNotHaveDependencyOnOtherProjects
    [Fact]
    public void ApplicationShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;

        var x = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            API_NAMESPACE,
            PRESENTATION_NAMESPACE,
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
    #endregion

    #region CommonShouldNotHaveDependencyOnOtherProjects
    [Fact]
    public void CommonShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Common.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            API_NAMESPACE,
            APPLICATION_NAMESPACE,
            INFRASTUCTRE_DAPPER_NAMESPACE,
            INFRASTUCTRE_DISTRIBUTED_CACHE_NAMESPACE,
            INFRASTUCTRE_MESSAGEBROKER_CONSUMER_EMAIL_NAMESPACE,
            INFRASTUCTRE_MESSAGEBROKER_PRODUCER_EMAIL_NAMESPACE,
            INFRASTUCTRE_REALTIME_NAMESPACE,
            INFRASTUCTRE_SERVICE_NAMESPACE,
            INFRASTUCTRE_STORAGE_NAMESPACE,
            PERSISTENCE_NAMESPACE,
            PRESENTATION_NAMESPACE
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
    #endregion

    #region DomainShouldNotHaveDependencyWithOtherProject
    [Fact]
    public void DomainShouldNotHaveDependencyWithOtherProject()
    {
        // Arrage
        var assembly = Domain.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            API_NAMESPACE,
            APPLICATION_NAMESPACE,
            INFRASTUCTRE_DAPPER_NAMESPACE,
            INFRASTUCTRE_DISTRIBUTED_CACHE_NAMESPACE,
            INFRASTUCTRE_MESSAGEBROKER_CONSUMER_EMAIL_NAMESPACE,
            INFRASTUCTRE_MESSAGEBROKER_PRODUCER_EMAIL_NAMESPACE,
            INFRASTUCTRE_REALTIME_NAMESPACE,
            INFRASTUCTRE_SERVICE_NAMESPACE,
            INFRASTUCTRE_STORAGE_NAMESPACE,
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
    #endregion

    #region InfrastructureDapperShouldNotHaveDependencyOnOtherProjects
    [Fact]
    public void InfrastructureDapperShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Infrastucture.Dapper.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            API_NAMESPACE,
            APPLICATION_NAMESPACE,
            INFRASTUCTRE_DISTRIBUTED_CACHE_NAMESPACE,
            INFRASTUCTRE_MESSAGEBROKER_CONSUMER_EMAIL_NAMESPACE,
            INFRASTUCTRE_MESSAGEBROKER_PRODUCER_EMAIL_NAMESPACE,
            INFRASTUCTRE_REALTIME_NAMESPACE,
            INFRASTUCTRE_SERVICE_NAMESPACE,
            INFRASTUCTRE_STORAGE_NAMESPACE,
            PERSISTENCE_NAMESPACE,
            PRESENTATION_NAMESPACE
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
    #endregion

    #region InfrastructureDistributedCacheShouldNotHaveDependencyOnOtherProjects
    [Fact]
    public void InfrastructureDistributedCacheShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Infrastucture.DistributedCache.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            API_NAMESPACE,
            APPLICATION_NAMESPACE,
            INFRASTUCTRE_DAPPER_NAMESPACE,
            INFRASTUCTRE_MESSAGEBROKER_CONSUMER_EMAIL_NAMESPACE,
            INFRASTUCTRE_MESSAGEBROKER_PRODUCER_EMAIL_NAMESPACE,
            INFRASTUCTRE_REALTIME_NAMESPACE,
            INFRASTUCTRE_SERVICE_NAMESPACE,
            INFRASTUCTRE_STORAGE_NAMESPACE,
            PERSISTENCE_NAMESPACE,
            PRESENTATION_NAMESPACE
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
    #endregion

    #region InfrastructureRealTimeShouldNotHaveDependencyOnOtherProjects
    [Fact]
    public void InfrastructureRealTimeShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Infrastucture.RealTime.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            API_NAMESPACE,
            APPLICATION_NAMESPACE,
            INFRASTUCTRE_DAPPER_NAMESPACE,
            INFRASTUCTRE_DISTRIBUTED_CACHE_NAMESPACE,
            INFRASTUCTRE_MESSAGEBROKER_CONSUMER_EMAIL_NAMESPACE,
            INFRASTUCTRE_MESSAGEBROKER_PRODUCER_EMAIL_NAMESPACE,
            INFRASTUCTRE_SERVICE_NAMESPACE,
            INFRASTUCTRE_STORAGE_NAMESPACE,
            PERSISTENCE_NAMESPACE,
            PRESENTATION_NAMESPACE
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
    #endregion

    #region InfrastructureServiceShouldNotHaveDependencyOnOtherProjects
    [Fact]
    public void InfrastructureServiceShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Infrastucture.Service.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            API_NAMESPACE,
            APPLICATION_NAMESPACE,
            INFRASTUCTRE_DAPPER_NAMESPACE,
            INFRASTUCTRE_DISTRIBUTED_CACHE_NAMESPACE,
            INFRASTUCTRE_MESSAGEBROKER_CONSUMER_EMAIL_NAMESPACE,
            INFRASTUCTRE_MESSAGEBROKER_PRODUCER_EMAIL_NAMESPACE,
            INFRASTUCTRE_REALTIME_NAMESPACE,
            INFRASTUCTRE_STORAGE_NAMESPACE,
            PERSISTENCE_NAMESPACE,
            PRESENTATION_NAMESPACE
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
    #endregion

    #region InfrastructureStorageShouldNotHaveDependencyOnOtherProjects
    [Fact]
    public void InfrastructureStorageShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Infrastucture.Storage.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            API_NAMESPACE,
            APPLICATION_NAMESPACE,
            INFRASTUCTRE_DAPPER_NAMESPACE,
            INFRASTUCTRE_DISTRIBUTED_CACHE_NAMESPACE,
            INFRASTUCTRE_MESSAGEBROKER_CONSUMER_EMAIL_NAMESPACE,
            INFRASTUCTRE_MESSAGEBROKER_PRODUCER_EMAIL_NAMESPACE,
            INFRASTUCTRE_REALTIME_NAMESPACE,
            INFRASTUCTRE_SERVICE_NAMESPACE,
            PERSISTENCE_NAMESPACE,
            PRESENTATION_NAMESPACE
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
    #endregion

    #region PersistenceShouldNotHaveDependencyOnOtherProjects
    [Fact]
    public void PersistenceShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Persistence.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            APPLICATION_NAMESPACE,
            INFRASTUCTRE_DAPPER_NAMESPACE,
            INFRASTUCTRE_DISTRIBUTED_CACHE_NAMESPACE,
            INFRASTUCTRE_REALTIME_NAMESPACE,
            INFRASTUCTRE_SERVICE_NAMESPACE,
            INFRASTUCTRE_STORAGE_NAMESPACE,
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
    #endregion

    #region PresentationShouldNotHaveDependencyOnOtherProjects
    [Fact]
    public void PresentationShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = Presentation.AssemblyReference.Assembly;

        var otherProjects = new[]
        {
            API_NAMESPACE,
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
    #endregion

}
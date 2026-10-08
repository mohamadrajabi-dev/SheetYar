using SheetYar.Application;
using SheetYar.Domain;
using SheetYar.Infrastructure;
using Xunit;

namespace SheetYar.Backend.Tests;

public sealed class ArchitectureSmokeTests
{
    [Fact]
    public void LayerMarkersBelongToDistinctAssemblies()
    {
        var assemblies = new[]
        {
            typeof(DomainAssemblyMarker).Assembly,
            typeof(ApplicationAssemblyMarker).Assembly,
            typeof(InfrastructureAssemblyMarker).Assembly,
        };

        Assert.Equal(3, assemblies.Distinct().Count());
    }
}

using Fun001;
using JetBrains.Annotations;
using Xunit;

namespace Fun001.Tests;

[TestSubject(typeof(FileWordProvider))]
public class FileWordProviderTest(FileWordProviderFixture fixture) : IClassFixture<FileWordProviderFixture>
{
    [Fact]
    public void Should_Return_Word_From_File()
    {
        // Arrange
        var provider = new FileWordProvider(fixture.Path);

        // Act
        var result = provider.GetWord();

        // Assert
        Assert.Contains(result, fixture.Words);
    }
    
    [Fact]
    public void Should_Not_Return_Other_Word()
    {
        // Arrange
        var provider = new FileWordProvider(fixture.Path);

        // Act
        var result = provider.GetWord();

        // Assert
        Assert.DoesNotContain("OtherWord", fixture.Words);
    }
}

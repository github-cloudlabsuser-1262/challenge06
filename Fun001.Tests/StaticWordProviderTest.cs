using JetBrains.Annotations;
using Xunit;

namespace Fun001.Tests;

[TestSubject(typeof(StaticWordProvider))]
public class StaticWordProviderTest
{
    [Fact]
    public void Should_Return_Static_Word()
    {
        // Arrange
        var provider = new StaticWordProvider("ExpectedWord");

        // Act
        var result = provider.GetWord();

        // Assert
        Assert.Equal("ExpectedWord", result);
    }
    
    [Fact]
    public void Should_Not_Return_Other_Word()
    {
        // Arrange
        var provider = new StaticWordProvider("ExpectedWord");

        // Act
        var result = provider.GetWord();

        // Assert
        Assert.NotEqual("OtherWord", result);
    }
}

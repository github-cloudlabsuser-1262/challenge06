using JetBrains.Annotations;
using Xunit;

namespace Fun001.Tests;

[TestSubject(typeof(RandomWordProvider))]
public class RandomWordProviderTest
{
    [Fact]
    public void Should_Return_Random_Word()
    {
        // Arrange
        var provider = new RandomWordProvider(new[] { "ExpectedWord" });

        // Act
        var result = provider.GetWord();

        // Assert
        Assert.Equal("ExpectedWord", result);
    }
    
    [Fact]
    public void Should_Not_Return_Other_Word()
    {
        // Arrange
        var provider = new RandomWordProvider(new[] { "ExpectedWord" });

        // Act
        var result = provider.GetWord();

        // Assert
        Assert.NotEqual("OtherWord", result);
    }
    
    [Fact]
    public void Should_Return_Random_Word_From_List()
    {
        // Arrange
        var provider = new RandomWordProvider(new[] { "ExpectedWord", "OtherWord" });

        // Act
        var result = provider.GetWord();

        // Assert
        Assert.Contains(result, new[] { "ExpectedWord", "OtherWord" });
    }
    
    [Fact]
    public void Should_Return_Random_Word_From_List_With_Many_Words()
    {
        // Arrange
        var provider = new RandomWordProvider(new[] { "ExpectedWord", "OtherWord", "AnotherWord" });

        // Act
        var result = provider.GetWord();

        // Assert
        Assert.Contains(result, new[] { "ExpectedWord", "OtherWord", "AnotherWord" });
    }
}

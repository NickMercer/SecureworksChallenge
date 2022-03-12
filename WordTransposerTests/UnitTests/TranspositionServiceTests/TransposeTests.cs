using Xunit;
using FluentAssertions;
using WordTransposer.TextManipulation;

namespace WordTransposerTests.UnitTests.TranspositionServiceTests;

public class TransposeTests
{
    [Theory]
    [InlineData("desserts", "stressed")]
    [InlineData("live", "evil")]
    [InlineData("pupils", "slipup")]
    [InlineData("we'd", "d'ew")]
    [InlineData("12345", "54321")]
    [InlineData("$%&'()*+", "+*)('&%$")]
    [InlineData("äÙÌⓩℬ", "ℬⓩÌÙä")]
    public void LowercaseString_TransposesCorrectly(string word, string expected)
    {
        //Arrange
        //Passed in Argument

        //Act
        var actual = TranspositionService.Transpose(word);

        //Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("Desserts", "stresseD")]
    [InlineData("liVE", "EVil")]
    [InlineData("PuPils", "sliPuP")]
    [InlineData("wE'd", "d'Ew")]
    public void StringWithCapitals_TransposesCorrectly(string word, string expected)
    {
        //Arrange
        //Passed in Argument

        //Act
        var actual = TranspositionService.Transpose(word);

        //Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void EmptyString_ReturnsEmptyString()
    {
        //Arrange
        var word = string.Empty;

        //Act
        var actual = TranspositionService.Transpose(word);

        //Assert
        actual.Should().Be(string.Empty);
    }

    [Fact]
    public void NullString_ReturnsNullString()
    {
        //Arrange
        string word = null;

        //Act
        var actual = TranspositionService.Transpose(word);

        //Assert
        actual.Should().BeNull();
    }

    //
    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData("\r\n")]
    public void StringIsWhiteSpace_ReturnsEmptyString(string word)
    {
        //Arrange
        //Passed by argument

        //Act
        var actual = TranspositionService.Transpose(word);

        //Assert
        actual.Should().Be(string.Empty);
    }
}

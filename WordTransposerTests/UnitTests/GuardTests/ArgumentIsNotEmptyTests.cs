using System;
using Xunit;
using FluentAssertions;
using WordTransposer.InputHandling;

namespace WordTransposerTests.UnitTests.GuardTests;

public class ArgumentIsNotEmptyTests
{
    [Fact]
    public void StringHasText_DoesNotThrow()
    {
        //Arrange
        var str = "a string";

        //Act
        Action act = () => Guard.ArgumentIsNotEmpty(str);

        //Assert
        act.Should().NotThrow();
    }

    [Theory]
    [InlineData("asdfas")]
    [InlineData("Special Characters: ^#(!@& #$")]
    [InlineData("Numbers: 147715123")]
    [InlineData("Unknown Unicode Characters: ")]
    [InlineData("Unicode Characters: äÙÌⓩℬǥ‰♲")]
    [InlineData("Emoji: 😀♥🎸🎵✞★🌠")]
    public void StringHasNonEnglishText_DoesNotThrow(string str)
    {
        //Arrange
        //Passed by Argument

        //Act
        Action act = () => Guard.ArgumentIsNotEmpty(str);

        //Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void StringIsEmpty_ThrowsArgumentException()
    {
        //Arrange
        var str = string.Empty;

        //Act
        Action act = () => Guard.ArgumentIsNotEmpty(str);

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData("\r\n")]
    public void StringIsWhiteSpace_ThrowsArgumentException(string str)
    {
        //Arrange
        //Passed by argument

        //Act
        Action act = () => Guard.ArgumentIsNotEmpty(str);

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void StringIsNull_ThrowsArgumentException()
    {
        //Arrange
        var str = string.Empty;

        //Act
        Action act = () => Guard.ArgumentIsNotEmpty(str);

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void StringIsNull_DoesNotThrowNullReferenceException()
    {
        //Arrange
        string str = null;

        //Act
        Action act = () => Guard.ArgumentIsNotEmpty(str);

        //Assert
        act.Should().NotThrow<NullReferenceException>();
    }

    //See note on similar test in ArgumentIsProvidedTests.cs
    [Fact]
    public void ExceptionIsThrown_SurfacesCorrectMessage()
    {
        //Arrange
        var str = string.Empty;

        //Act
        Action act = () => Guard.ArgumentIsNotEmpty(str);

        //Assert
        var expectedMessage = "Word Transposer requires a single argument that contains a file or folder path to one or more .txt files.";
        act.Should().Throw<Exception>().WithMessage(expectedMessage);
    }
}

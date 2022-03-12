using System;
using WordTransposer;
using Xunit;
using FluentAssertions;
using System.IO;

namespace WordTransposerTests.UnitTests.GuardTests;

public class PathTypeIsValidTests
{
    [Theory]
    [InlineData(PathType.Directory)]
    [InlineData(PathType.File)]
    public void ValidPathType_DoesNotThrow(PathType type)
    {
        //Arrange
        var fakePath = "FakePath.txt";

        //Act
        Action act = () => Guard.PathTypeIsValid(type, fakePath);

        //Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void PathTypeError_ThrowsFileNotFoundException()
    {
        //Arrange
        var type = PathType.Error;
        var fakePath = "FakePath.txt";

        //Act
        Action act = () => Guard.PathTypeIsValid(type, fakePath);

        //Assert
        act.Should().Throw<FileNotFoundException>();
    }

    [Fact]
    public void ParameterNotAnEnumValue_ThrowsFileNotFoundException()
    {
        //Arrange
        var type = (PathType)1000; //Create an invalid enum value 
        var fakePath = "FakePath.txt";

        //Act
        Action act = () => Guard.PathTypeIsValid(type, fakePath);

        //Assert
        act.Should().Throw<FileNotFoundException>();
    }

    //See note on similar test in ArgumentIsProvidedTests.cs
    [Fact]
    public void ExceptionIsThrown_SurfacesCorrectMessage()
    {
        //Arrange
        var type = PathType.Error;
        var fakePath = "FakePath.txt";

        //Act
        Action act = () => Guard.PathTypeIsValid(type, fakePath);

        //Assert
        var expectedMessage = $"Could not find file or folder at location {fakePath}.";
        act.Should().Throw<Exception>().WithMessage(expectedMessage);
    }
}

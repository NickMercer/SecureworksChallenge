using Xunit;
using FluentAssertions;
using WordTransposer;
using System;

namespace WordTransposerTests.UnitTests.GuardTests;

public class ArgumentIsProvidedTests
{
    [Fact]
    public void OneArgumentProvided_DoesNotThrow()
    {
        //Arrange
        var args = new string[]
        {
            "argument"
        };

        //Act
        Action act = () => Guard.ArgumentIsProvided(args);

        //Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void MultipleArgumentsProvided_DoesNotThrow()
    {
        //Arrange
        var args = new string[]
        {
            "argument0",
            "argument1"
        };

        //Act
        Action act = () => Guard.ArgumentIsProvided(args);

        //Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void NoArgumentsProvided_ThrowsArgumentException()
    {
        //Arrange
        var args = new string[] { };

        //Act
        Action act = () => Guard.ArgumentIsProvided(args);

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ParameterIsNull_ThrowsArgumentException()
    {
        //Arrange
        string[] args = null;

        //Act
        Action act = () => Guard.ArgumentIsProvided(args);

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    //This test is a bit redundant with the previous one, but I just want to be careful that the program doesn't throw any errors except the specific ones I've set up messages for.
    //For security, I wouldn't want the program giving implementation details through an error that shouldn't be public knowledge.
    [Fact]
    public void ParameterIsNull_DoesNotThrowNullReferenceException()
    {
        //Arrange
        string[] args = null;

        //Act
        Action act = () => Guard.ArgumentIsProvided(args);

        //Assert
        act.Should().NotThrow<NullReferenceException>();
    }

    //That's why I'm checking that the exact message is shown when this method throws as well. 
    //I'm actually not sure if this is a good test or not. I could see the argument that it's an implementation detail that shouldn't be coupled to the tests.
    //But for security reasons, I could also see a specific requirement given for what messages should come back, and the message given when an exception is thrown is technically an output of the method.
    //I'd love to hear someone's opinion on this. Perhaps it just depends on the requirements and the circumstances?
    //In any case, for this challenge, I'm going to test my error message outputs.
    [Fact]
    public void ExceptionIsThrown_SurfacesCorrectMessage()
    {
        //Arrange
        var args = new string[] { };

        //Act
        Action act = () => Guard.ArgumentIsProvided(args);

        //Assert
        var expectedMessage = "Word Transposer requires a single argument that contains a file or folder path to one or more .txt files.";
        act.Should().Throw<Exception>().WithMessage(expectedMessage);
    }
}

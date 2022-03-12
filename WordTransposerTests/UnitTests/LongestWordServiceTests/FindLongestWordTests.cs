using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using WordTransposer;

namespace WordTransposerTests.UnitTests.LongestWordServiceTests;

//Tests for the FindLongestWord method in the LongestWordService.
//Here I do test that the inputs in the lines enumerable, but I don't test the size of the enumerable in memory. 
//That is left for the integration testing where files of various sizes are passed in.
//I'm making this choice because the enumerable itself is just an interface, any tests on the object being passed in I am considering
//as being resposible to be tested in the calling code. Here I just want to test what this method does with what it's given.
public class FindLongestWordTests
{
    [Fact]
    public void SingleLongestWord_ReturnsLongestWord()
    {
        //Arrange
        var lines = new List<string>
        {
            "this is a test line",
            "the longest word is on the next line",
            "ThisIsALongWordBecauseThereAreNoSpaces",
            "so it will return that one"
        };

        var sut = new LongestWordService();

        //Act
        var actual = sut.FindLongestWord(lines);

        //Assert
        var expected = "ThisIsALongWordBecauseThereAreNoSpaces";
        actual.Should().Be(expected);
    }

    [Fact]
    public void MultipleLongestWords_ReturnsFirstFound()
    {
        //Arrange
        var lines = new List<string>
        {
            "there are two eight letter words",
            "in this testcase sentence"
        };

        var sut = new LongestWordService();

        //Act
        var actual = sut.FindLongestWord(lines);

        //Assert
        var expected = "testcase";
        actual.Should().Be(expected);
    }

    [Fact]
    public void SomeLinesWithoutWords_ReturnsLongestWord()
    {
        //Arrange
        var lines = new List<string>
        {
            "",
            "#*@$",
            "most of these lines have nothing",
            "123415515",
            " ",
            "\t",
            "\\",
            "\r\n",
            "\n"
        };

        var sut = new LongestWordService();

        //Act
        var actual = sut.FindLongestWord(lines);

        //Assert
        var expected = "nothing";
        actual.Should().Be(expected);
    }

    [Fact]
    public void NoLinesWithWords_ReturnsEmptyString()
    {
        //Arrange
        var lines = new List<string>
        {
            "",
            ""
        };

        var sut = new LongestWordService();

        //Act
        var actual = sut.FindLongestWord(lines);

        //Assert
        var expected = string.Empty;
        actual.Should().Be(expected);
    }

    [Fact]
    public void EmptyInput_ReturnsEmptyString()
    {
        //Arrange
        var lines = new List<string>();

        var sut = new LongestWordService();

        //Act
        var actual = sut.FindLongestWord(lines);

        //Assert
        var expected = string.Empty;
        actual.Should().Be(expected);
    }

    [Fact]
    public void NullInput_ReturnsEmptyString()
    {
        //Arrange
        IEnumerable<string> lines = null;

        var sut = new LongestWordService();

        //Act
        var actual = sut.FindLongestWord(lines);

        //Assert
        var expected = string.Empty;
        actual.Should().Be(expected);
    }

    [Fact]
    public void AllValidDelimiters_FindsCorrectWord()
    {
        //Arrange
        var lines = new List<string>
        {
            "this is\tseparated\nin    various  ways"
        };

        var sut = new LongestWordService();

        //Act
        var actual = sut.FindLongestWord(lines);

        //Assert
        var expected = "separated";
        actual.Should().Be(expected);
    }

    [Fact]
    public void InputWithNumbers_IgnoresNumbers()
    {
        //Arrange
        var lines = new List<string>
        {
            "this word has nums193102",
            "but the longest is that"
        };

        var sut = new LongestWordService();

        //Act
        var actual = sut.FindLongestWord(lines);

        //Assert
        var expected = "longest";
        actual.Should().Be(expected);
    }

    [Fact]
    public void InputWithSpecialCharacters_IgnoresSpecialCharacters()
    {
        //Arrange
        var lines = new List<string>
        {
            "this word has these!#\"$%&'()*+,-./:;<=>?@[\\]^_`{|}~",
            "but the longest is that"
        };

        var sut = new LongestWordService();

        //Act
        var actual = sut.FindLongestWord(lines);

        //Assert
        var expected = "longest";
        actual.Should().Be(expected);
    }

    [Fact]
    public void LongestSetOfLettersIsNotWord_IgnoresNonWord()
    {
        //Arrange
        var lines = new List<string>
        {
            "this is long and letter based but is not a word BD1A9D3C-E474-489E-B5AD-19AD57C20FA6",
            "but the longest is that"
        };

        var sut = new LongestWordService();

        //Act
        var actual = sut.FindLongestWord(lines);

        //Assert
        var expected = "longest";
        actual.Should().Be(expected);
    }

    [Fact]
    public void LongestWordIncludesApostrophe_WordWithApostropheReturned()
    {
        //Arrange
        var lines = new List<string>
        {
            "longest word has is an apostrophe's word"
        };

        var sut = new LongestWordService();

        //Act
        var actual = sut.FindLongestWord(lines);

        //Assert
        var expected = "apostrophe's";
        actual.Should().Be(expected);
    }

    [Fact]
    public void LongestWordIsATieWithApostropheWord_ApostropheIsNotCounted()
    {
        //Arrange
        var lines = new List<string>
        {
            "this'll not be the longest word."
        };

        var sut = new LongestWordService();

        //Act
        var actual = sut.FindLongestWord(lines);

        //Assert
        var expected = "longest";
        actual.Should().Be(expected);
    }
}

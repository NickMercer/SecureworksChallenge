using System;
using System.IO;
using Xunit;
using FluentAssertions;

namespace WordTransposerTests.IntegrationTests;

public class TransposeLongestWordTests
{
    public TransposeLongestWordTests()
    {
        //I can't include an empty directory in the project files and have it included in the build, so this setup has to happen to make sure the empty directory tests work.
        var emptyTestDirectoryPath = Path.Combine(Environment.CurrentDirectory, "TestCases", "EmptyDirectoryTest");
        if (!Directory.Exists(emptyTestDirectoryPath))
        {
            Directory.CreateDirectory(emptyTestDirectoryPath);
        }
    }

    [Fact]
    public void GivenValidFilePath_DoesNotThrow()
    {
        //Arrange
        var filePath = Path.Combine(Environment.CurrentDirectory, "TestCases", "SimpleWordsTest.txt");
        
        //Act
        Action act = () => Program.TransposeLongestWord(new string[] { filePath });

        //Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void GivenValidDirectoryPath_DoesNotThrow()
    {
        //Arrange
        var directoryPath = Path.Combine(Environment.CurrentDirectory, "TestCases", "EmptyDirectoryTest");

        //Act
        Action act = () => Program.TransposeLongestWord(new string[] { directoryPath });

        //Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void GivenInvalidFilePath_ThrowsFileNotFoundException()
    {
        //Arrange
        var filePath = Path.Combine(Environment.CurrentDirectory, "TestCases", "ThisFileDoesNotExist.txt");

        //Act
        Action act = () => Program.TransposeLongestWord(new string[] { filePath });

        //Assert
        act.Should().Throw<FileNotFoundException>();
    }

    [Fact]
    public void GivenInvalidDirectoryPath_ThrowsFileNotFoundException()
    {
        //Arrange
        var directoryPath = Path.Combine(Environment.CurrentDirectory, "TestCases", "ThisDirectoryDoesNotExist");

        //Act
        Action act = () => Program.TransposeLongestWord(new string[] { directoryPath });

        //Assert
        act.Should().Throw<FileNotFoundException>();
    }

    [Fact]
    public void GivenNonPathString_ThrowsFileNotFoundException()
    {
        //Arrange
        var filePath = "Notsureq2\\what thisisBut1tsn0tafilepath.txt";

        //Act
        Action act = () => Program.TransposeLongestWord(new string[] { filePath });

        //Assert
        act.Should().Throw<FileNotFoundException>();
    }

    [Fact]
    public void GivenEmptyPath_ThrowsArgumentException()
    {
        //Arrange
        var directoryPath = "";

        //Act
        Action act = () => Program.TransposeLongestWord(new string[] { directoryPath });

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GivenNullArgs_ThrowsArgumentException()
    {
        //Arrange
        string[] args = null;

        //Act
        Action act = () => Program.TransposeLongestWord(args);

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GivenNoArgs_ThrowsArgumentException()
    {
        //Arrange
        string[] args = new string[] { };

        //Act
        Action act = () => Program.TransposeLongestWord(args);

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GivenEmptyDirectory_ReturnsCorrectly()
    {
        //Arrange
        var directoryPath = Path.Combine(Environment.CurrentDirectory, "TestCases", "EmptyDirectoryTest");
        var actual = new StringWriter();
        Console.SetOut(actual);

        //Act
        Program.TransposeLongestWord(new string[] { directoryPath });

        //Assert
        var expected = new string[]
        {
            "Longest Word: No Longest Word",
            "Transposed Word: N/A"
        };
        actual.ToString().Should().ContainAll(expected);
    }

    [Fact]
    public void GivenEmptyFile_ReturnsCorrectly()
    {
        //Arrange
        var filePath = Path.Combine(Environment.CurrentDirectory, "TestCases", "EmptyTestFile.txt");
        var actual = new StringWriter();
        Console.SetOut(actual);

        //Act
        Program.TransposeLongestWord(new string[] { filePath });

        //Assert
        var expected = new string[]
        {
            "Longest Word: No Longest Word",
            "Transposed Word: N/A"
        };
        actual.ToString().Should().ContainAll(expected);
    }

    [Fact]
    public void SimpleWordsTest_ReturnsCorrectly()
    {
        //Arrange
        var filePath = Path.Combine(Environment.CurrentDirectory, "TestCases", "SimpleWordsTest.txt");
        var actual = new StringWriter();
        Console.SetOut(actual);

        //Act
        Program.TransposeLongestWord(new string[] { filePath });

        //Assert
        var expected = new string[]
        {
            "Longest Word: penultimate",
            "Transposed Word: etamitlunep"
        };
        actual.ToString().Should().ContainAll(expected);
    }

    [Fact]
    public void WordsWithInvalidCharactersTest_ReturnsCorrectly()
    {
        //Arrange
        var filePath = Path.Combine(Environment.CurrentDirectory, "TestCases", "WordsWithInvalidCharactersTest.txt");
        var actual = new StringWriter();
        Console.SetOut(actual);

        //Act
        Program.TransposeLongestWord(new string[] { filePath });

        //Assert
        var expected = new string[]
        {
            "Longest Word: hippopotomonstrosesquipedaliophobia",
            "Transposed Word: aibohpoiladepiuqsesortsnomotopoppih"
        };
        actual.ToString().Should().ContainAll(expected);
    }

    [Fact]
    public void TiedWordLengthTest_ReturnsCorrectly()
    {
        //Arrange
        var filePath = Path.Combine(Environment.CurrentDirectory, "TestCases", "TiedWordLengthTest.txt");
        var actual = new StringWriter();
        Console.SetOut(actual);

        //Act
        Program.TransposeLongestWord(new string[] { filePath });

        //Assert
        var expected = new string[]
        {
            "Longest Word: penultimate",
            "Transposed Word: etamitlunep"
        };
        actual.ToString().Should().ContainAll(expected);
    }

    [Fact]
    public void DirectoryWithFilesTest_ReturnsCorrectly()
    {
        //Arrange
        var directoryPath = Path.Combine(Environment.CurrentDirectory, "TestCases", "DirectoryWithFilesTest");
        var actual = new StringWriter();
        Console.SetOut(actual);

        //Act
        Program.TransposeLongestWord(new string[] { directoryPath });

        //Assert
        var expected = new string[]
        {
            "Longest Word: ThisIsTheLongestWord",
            "Transposed Word: droWtsegnoLehTsIsihT"
        };
        actual.ToString().Should().ContainAll(expected);
    }

    [Fact]
    public void DirectoryWithEmptyFiles_ReturnsCorrectly()
    {
        //Arrange
        var directoryPath = Path.Combine(Environment.CurrentDirectory, "TestCases", "DirectoryWithEmptyFilesTest");
        var actual = new StringWriter();
        Console.SetOut(actual);

        //Act
        Program.TransposeLongestWord(new string[] { directoryPath });

        //Assert
        var expected = new string[]
        {
            "Longest Word: No Longest Word",
            "Transposed Word: N/A"
        };
        actual.ToString().Should().ContainAll(expected);
    }
}

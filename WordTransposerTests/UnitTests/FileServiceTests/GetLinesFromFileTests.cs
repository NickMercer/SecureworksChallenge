using System;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using System.IO;
using WordTransposer.InputHandling;

namespace WordTransposerTests.UnitTests.FileServiceTests;

public class GetLinesFromFileTests
{
    [Fact]
    public void FileHasOneLines_ReturnsLines()
    {
        //Arrange
        var filePath = @"C:\TestFile.txt";
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            { filePath, new MockFileData("some text") }
        });

        //Act
        var actual = FileService.GetLinesFromFile(fileSystem, filePath);

        //Assert
        var expected = new List<string>
        {
            "some text"
        }.AsEnumerable();

        actual.Should().Equal(expected);
    }

    [Fact]
    public void FileHasMultipleLines_ReturnsLines()
    {
        //Arrange
        var filePath = @"C:\TestFile.txt";
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            { filePath, new MockFileData("some text\nsecond line") }
        });

        //Act
        var actual = FileService.GetLinesFromFile(fileSystem, filePath);

        //Assert
        var expected = new List<string>
        {
            "some text",
            "second line"
        }.AsEnumerable();

        actual.Should().Equal(expected);
    }

    [Fact]
    public void FileHasSomeEmptyLines_ReturnsLines()
    {
        //Arrange
        var filePath = @"C:\TestFile.txt";
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            { filePath, new MockFileData("some text\n\n\nother line") }
        });

        //Act
        var actual = FileService.GetLinesFromFile(fileSystem, filePath);

        //Assert
        actual.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void FileIsEmpty_ReturnsEmptyEnumerable()
    {
        //Arrange
        var filePath = @"C:\TestFile.txt";
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            { filePath, new MockFileData("") }
        });

        //Act
        var actual = FileService.GetLinesFromFile(fileSystem, filePath);

        //Assert
        actual.Should().BeEmpty();
    }

    [Fact]
    public void FileDoesNotExist_ThrowsFileNotFoundException()
    {
        //Arrange
        var filePath = @"C:\TestFile.txt";
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>());

        //Act
        Action act = () => FileService.GetLinesFromFile(fileSystem, filePath);

        //Assert
        act.Should().Throw<FileNotFoundException>();
    }

    [Fact]
    public void FileSystemIsNull_ThrowsArgumentNullException()
    {
        //Arrange
        var filePath = @"C:\TestFile.txt";
        IFileSystem fileSystem = null;

        //Act
        Action act = () => FileService.GetLinesFromFile(fileSystem, filePath);

        //Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void FilePathIsNull_ThrowsArgumentNullException()
    {
        //Arrange
        string filePath = null;
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>());

        //Act
        Action act = () => FileService.GetLinesFromFile(fileSystem, filePath);

        //Assert
        act.Should().Throw<ArgumentNullException>();
    }
}

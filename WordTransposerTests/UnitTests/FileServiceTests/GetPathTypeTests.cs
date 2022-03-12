using WordTransposer;
using Xunit;
using FluentAssertions;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using System.IO.Abstractions;
using System;

namespace WordTransposerTests.UnitTests.FileServiceTests;

public class GetPathTypeTests
{
    [Theory]
    [InlineData(@"C:\TestFile.txt")] //Windows path
    [InlineData(@"Users/TestUser/TestFile.txt")] //Mac path
    [InlineData(@"home/TestFile.txt")] //Linux path
    public void FilePath_ReturnsPathTypeAsFile(string filePath)
    {
        //Arrange
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            { filePath, new MockFileData("some text") }
        });

        //Act
        var actual = FileService.GetPathType(fileSystem, filePath);

        //Assert
        actual.Should().Be(PathType.File);
    }

    [Theory]
    [InlineData(@"C:\TestFile\")] //Windows path
    [InlineData(@"C:\TestFile")] //Windows path no trailing backslash
    [InlineData(@"Users/TestUser/TestFile/")] //Mac path
    [InlineData(@"Users/TestUser/TestFile")] //Mac path no trailing slash
    [InlineData(@"home/TestFile/")] //Linux path
    [InlineData(@"home/TestFile")] //Linux path no trailing slash
    public void DirectoryPath_ReturnsPathTypeAsDirectory(string directoryPath)
    {
        //Arrange
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            { directoryPath, new MockDirectoryData() }
        });

        //Act
        var actual = FileService.GetPathType(fileSystem, directoryPath);

        //Assert
        actual.Should().Be(PathType.Directory);
    }


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData("\r\n")]
    public void PathIsNullOrWhiteSpace_ReturnsPathTypeAsError(string filePath)
    {
        //Arrange   
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>());

        //Act 
        var actual = FileService.GetPathType(fileSystem, filePath);

        //Assert
        actual.Should().Be(PathType.Error);
    }

    [Theory]
    [InlineData(@"C:\TestFile.txt")] //Windows path
    [InlineData(@"Users/TestUser/TestFile.txt")] //Mac path
    [InlineData(@"home/TestFile.txt")] //Linux path
    public void FileDoesntExist_ReturnsPathTypeAsError(string filePath)
    {
        //Arrange
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>());

        //Act
        var actual = FileService.GetPathType(fileSystem, filePath);

        //Assert
        actual.Should().Be(PathType.Error);
    }

    [Theory]
    [InlineData(@"C:\TestFile\")] //Windows path
    [InlineData(@"C:\TestFile")] //Windows path no trailing backslash
    [InlineData(@"Users/TestUser/TestFile/")] //Mac path
    [InlineData(@"Users/TestUser/TestFile")] //Mac path no trailing slash
    [InlineData(@"home/TestFile/")] //Linux path
    [InlineData(@"home/TestFile")] //Linux path no trailing slash
    public void DirectoryDoesntExist_ReturnsPathTypeAsError(string directoryPath)
    {
        //Arrange
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>());

        //Act
        var actual = FileService.GetPathType(fileSystem, directoryPath);

        //Assert
        actual.Should().Be(PathType.Error);
    }

    [Fact]
    public void FileSystemIsNull_ThrowsArgumentNullException()
    {
        //Arrange   
        IFileSystem fileSystem = null;
        var filePath = "TestFile.txt";

        //Act 
        Action act = () => FileService.GetPathType(fileSystem, filePath);

        //Assert
        act.Should().Throw<ArgumentNullException>();
    }
}

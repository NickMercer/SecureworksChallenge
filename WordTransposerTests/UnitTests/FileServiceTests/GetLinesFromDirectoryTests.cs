using System;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using WordTransposer.InputHandling;

namespace WordTransposerTests.UnitTests.FileServiceTests;

public class GetLinesFromDirectoryTests
{
    [Fact]
    public void NoFilesInDirectory_ReturnsEmpty()
    {
        //Arrange
        var directoryPath = @"C:\TestDirectory";
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>());
        fileSystem.Directory.CreateDirectory(directoryPath);

        //Act
        var actual = FileService.GetLinesFromDirectory(fileSystem, directoryPath);

        //Assert
        actual.Should().BeEmpty();
    }

    [Fact]
    public void OneFileInDirectory_ReturnsLines()
    {
        //Arrange
        var directoryPath = @"C:\TestDirectory";
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>());
        fileSystem.Directory.CreateDirectory(directoryPath);

        var filePath = "TestFile.txt";
        var fileContents = "this is a test\nwith two lines";
        fileSystem.File.WriteAllText(fileSystem.Path.Combine(directoryPath, filePath), fileContents);

        //Act
        var actual = FileService.GetLinesFromDirectory(fileSystem, directoryPath);

        //Assert
        var expected = new List<string>
        {
            "this is a test",
            "with two lines"
        }.AsEnumerable();

        actual.Should().Equal(expected);
    }

    [Fact]
    public void MultipleFilesInDirectory_ReturnsLines()
    {
        //Arrange
        var directoryPath = @"C:\TestDirectory";
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>());
        fileSystem.Directory.CreateDirectory(directoryPath);

        var file1Path = "TestFile1.txt";
        var file1Contents = "this is a test\nwith two lines";
        fileSystem.File.WriteAllText(fileSystem.Path.Combine(directoryPath, file1Path), file1Contents);

        var file2Path = "TestFile2.txt";
        var file2Contents = "this is a test\nwith two lines\nand some other stuff";
        fileSystem.File.WriteAllText(fileSystem.Path.Combine(directoryPath, file2Path), file2Contents);

        var file3Path = "TestFile3.txt";
        var file3Contents = "just one line here";
        fileSystem.File.WriteAllText(fileSystem.Path.Combine(directoryPath, file3Path), file3Contents);

        //Act
        var actual = FileService.GetLinesFromDirectory(fileSystem, directoryPath);

        //Assert
        var expected = new List<string>
        {
            "this is a test",
            "with two lines",
            "this is a test",
            "with two lines",
            "and some other stuff",
            "just one line here"
        }.AsEnumerable();

        actual.Should().Equal(expected);
    }

    [Fact]
    public void EmptyFilesInDirectory_ReturnsEmpty()
    {
        //Arrange
        var directoryPath = @"C:\TestDirectory";
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>());
        fileSystem.Directory.CreateDirectory(directoryPath);

        var file1Path = "TestFile1.txt";
        var file1Contents = "";
        fileSystem.File.WriteAllText(fileSystem.Path.Combine(directoryPath, file1Path), file1Contents);

        var file2Path = "TestFile2.txt";
        var file2Contents = string.Empty;
        fileSystem.File.WriteAllText(fileSystem.Path.Combine(directoryPath, file2Path), file2Contents);

        var file3Path = "TestFile3.txt";
        var file3Contents = "";
        fileSystem.File.WriteAllText(fileSystem.Path.Combine(directoryPath, file3Path), file3Contents);

        //Act
        var actual = FileService.GetLinesFromDirectory(fileSystem, directoryPath);

        //Assert
        actual.Should().BeEmpty();
    }

    [Fact]
    public void WhiteSpaceFilesInDirectory_ReturnsEmpty()
    {
        //Arrange
        var directoryPath = @"C:\TestDirectory";
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>());
        fileSystem.Directory.CreateDirectory(directoryPath);

        var file1Path = "TestFile1.txt";
        var file1Contents = " ";
        fileSystem.File.WriteAllText(fileSystem.Path.Combine(directoryPath, file1Path), file1Contents);

        var file2Path = "TestFile2.txt";
        var file2Contents = "\n";
        fileSystem.File.WriteAllText(fileSystem.Path.Combine(directoryPath, file2Path), file2Contents);

        var file3Path = "TestFile3.txt";
        var file3Contents = "\t";
        fileSystem.File.WriteAllText(fileSystem.Path.Combine(directoryPath, file3Path), file3Contents);

        //Act
        var actual = FileService.GetLinesFromDirectory(fileSystem, directoryPath);

        //Assert
        actual.Should().BeEmpty();
    }

    [Fact]
    public void FileSystemIsNull_ThrowsArgumentNullException()
    {
        //Arrange
        var directoryPath = @"C:\TestDirectory";
        IFileSystem fileSystem = null;

        //Act
        Action act = () => FileService.GetLinesFromDirectory(fileSystem, directoryPath);

        //Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void DirectoryPathIsNull_ThrowsArgumentNullException()
    {
        //Arrange
        string directoryPath = null;
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>());

        //Act
        Action act = () => FileService.GetLinesFromDirectory(fileSystem, directoryPath);

        //Assert
        act.Should().Throw<ArgumentNullException>();
    }
}

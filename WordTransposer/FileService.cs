using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace WordTransposer;

//I'm using an abstraction of the file system to facilitate unit testing.
public static class FileService
{
    /// <summary>
    /// Given a path on the system, returns whether it is a file or a folder path.
    /// </summary>
    /// <param name="fileSystem">The file system to check</param>
    /// <param name="pathArgument">The path to check</param>
    /// <returns>PathType value for the given path</returns>
    public static PathType GetPathType(IFileSystem fileSystem, string pathArgument)
    {
        if(fileSystem == null)
        {
            //Without this, the method would throw a NullReferenceException anyway, but for this challenge I'm trying to be explicit
            //So I'll have it throw a specific exception saying fileSystem needs to be passed in.
            throw new ArgumentNullException("File system cannot be null");
        }

        if (fileSystem.Directory.Exists(pathArgument))
        {
            return PathType.Directory;
        }

        if(fileSystem.File.Exists(pathArgument))
        {
            return PathType.File;
        }

        return PathType.Error;
    }

    /// <summary>
    /// Returns an enumerable of lines from every file in a directory without loading them all into memory.
    /// </summary>
    /// <param name="fileSystem">The file system to check</param>
    /// <param name="directoryPath">Directory to read</param>
    /// <returns>Enumerable of lines from the directory's files</returns>
    public static IEnumerable<string> GetLinesFromDirectory(IFileSystem fileSystem, string directoryPath)
    {
        IEnumerable<string> directoryLines = Enumerable.Empty<string>();

        foreach (var filePath in fileSystem.Directory.GetFiles(directoryPath))
        {
            var lines = GetLinesFromFile(fileSystem, filePath);
            directoryLines.Concat(lines);
        }

        return directoryLines;
    }

    /// <summary>
    /// Returns an enumerable of lines from a file without loading them all into memory.
    /// </summary>
    /// <param name="fileSystem">The file system to check</param>
    /// <param name="filePath">File to read</param>
    /// <returns>Enumerable of lines from the file</returns>
    public static IEnumerable<string> GetLinesFromFile(IFileSystem fileSystem, string filePath)
    {
        return fileSystem.File.ReadLines(filePath);
    }
}

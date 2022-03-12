using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordTransposer;
public static class FileService
{
    /// <summary>
    /// Given a path on the system, returns whether it is a file or a folder path.
    /// </summary>
    /// <param name="pathArgument">The path to check</param>
    /// <returns>PathType value for the given path</returns>
    public static PathType GetPathType(string pathArgument)
    {
        if (Directory.Exists(pathArgument))
        {
            return PathType.Directory;
        }

        if(File.Exists(pathArgument))
        {
            return PathType.File;
        }

        return PathType.Error;
    }

    /// <summary>
    /// Returns an enumerable of lines from every file in a directory without loading them all into memory.
    /// </summary>
    /// <param name="directoryPath">Directory to read</param>
    /// <returns>Enumerable of lines from the directory's files</returns>
    public static IEnumerable<string> GetLinesFromDirectory(string directoryPath)
    {
        IEnumerable<string> directoryLines = Enumerable.Empty<string>();

        foreach (var filePath in Directory.GetFiles(directoryPath))
        {
            var lines = GetLinesFromFile(filePath);
            directoryLines.Concat(lines);
        }

        return directoryLines;
    }

    /// <summary>
    /// Returns an enumerable of lines from a file without loading them all into memory.
    /// </summary>
    /// <param name="filePath">File to read</param>
    /// <returns>Enumerable of lines from the file</returns>
    public static IEnumerable<string> GetLinesFromFile(string filePath)
    {
        return File.ReadLines(filePath);
    }
}

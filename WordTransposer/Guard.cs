using System;
using System.IO;

namespace WordTransposer;

public static class Guard
{
    private static string _argumentExceptionMessage = "Word Transposer requires a single argument that contains a file or folder path to one or more .txt files.";

    /// <summary>
    /// Throws Exception if string array does not contain at least one element.
    /// </summary>
    /// <param name="args">Argument array to validate</param>
    /// <exception cref="ArgumentException">Thrown if args array is null or empty</exception>
    public static void ArgumentIsProvided(string[] args)
    {
        if(args == null || args.Length == 0)
        {
            throw new ArgumentException(_argumentExceptionMessage);
        }
    }

    /// <summary>
    /// Throws exception if string is null or only whitespace characters
    /// </summary>
    /// <param name="arg">String to validate</param>
    /// <exception cref="ArgumentException">Thrown if string is null or only whitespace characters</exception>
    public static void ArgumentIsNotEmpty(string arg)
    {
        if (string.IsNullOrWhiteSpace(arg))
        {
            throw new ArgumentException(_argumentExceptionMessage);
        }
    }

    /// <summary>
    /// Throws exception if the PathType is PathType.Error
    /// </summary>
    /// <param name="pathType">PathType of the filePath</param>
    /// <param name="filePath">Path being validated.</param>
    /// <exception cref="FileNotFoundException">Thrown if path type is PathType.Error</exception>
    public static void PathTypeIsValid(PathType pathType, string filePath)
    {
        if(pathType != PathType.File && pathType != PathType.Directory)
        {
            var errorMessage = $"Could not find file or folder at location {filePath}.";
            throw new FileNotFoundException(errorMessage);
        }
    }
}

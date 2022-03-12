using System;
using System.IO;

namespace WordTransposer;

public static class Guard
{
    private static string _argumentExceptionMessage = "Word Transposer requires a single argument that contains a file or folder path to one or more .txt files.";

    public static void ArgumentIsProvided(string[] args)
    {
        if(args.Length == 0)
        {
            throw new ArgumentException(_argumentExceptionMessage);
        }
    }

    public static void ArgumentIsNotEmpty(string arg)
    {
        if (string.IsNullOrWhiteSpace(arg))
        {
            throw new ArgumentException(_argumentExceptionMessage);
        }
    }

    public static void PathTypeIsValid(PathType pathType, string filePath)
    {
        if(pathType == PathType.Error)
        {
            var errorMessage = $"Could not find file or folder at location {filePath}.";
            throw new FileNotFoundException(errorMessage);
        }
    }
}

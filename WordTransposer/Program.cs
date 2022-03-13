using System;
using System.IO.Abstractions;
using WordTransposer.InputHandling;
using WordTransposer.TextManipulation;

public class Program
{
    public static void Main(string[] args)
    {
        TransposeLongestWord(args);
    }

    //I'm just wrapping the whole program in a method so I can test it in the test suite
    public static void TransposeLongestWord(string[] args)
    {
        //Validate Input is a file/directory path.
        Guard.ArgumentIsProvided(args);

        var fileSystem = new FileSystem();
        var pathArgument = fileSystem.Path.GetFullPath(args[0]);
        Guard.ArgumentIsNotEmpty(pathArgument);

        //Figure out if we have a file or a directory.
        var pathType = FileService.GetPathType(fileSystem, pathArgument);
        Guard.PathTypeIsValid(pathType, pathArgument);

        //Find the longest word in the file/directory
        var lines = pathType switch
        {
            PathType.File => FileService.GetLinesFromFile(fileSystem, pathArgument),
            PathType.Directory => FileService.GetLinesFromDirectory(fileSystem, pathArgument),

            //This should be completely unreachable because of the guard clause, but the switch expression needs it defined because the enum value for PathType.Error exists.
            _ => throw new InvalidOperationException($"Invalid PathType for {pathArgument}")
        };

        var longestWord = LongestWordService.FindLongestWord(lines);
        var transposedWord = TranspositionService.Transpose(longestWord);

        if (string.IsNullOrWhiteSpace(longestWord))
        {
            longestWord = "No Longest Word";
            transposedWord = "N/A";
        }

        Console.WriteLine(longestWord);
        Console.WriteLine(transposedWord);
    }
}
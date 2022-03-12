using System;
using System.IO;
using WordTransposer;

public class Program
{
    public static void Main(string[] args)
    {
        //Validate Input is a file/directory path.
        Guard.ArgumentIsProvided(args);

        var pathArgument = Path.GetFullPath(args[0]);
        Guard.ArgumentIsNotEmpty(pathArgument);

        //Figure out if we have a file or a directory.
        var pathType = FileService.GetPathType(pathArgument);
        Guard.PathTypeIsValid(pathType, pathArgument);

        //Find the longest word in the file/directory
        var lines = pathType switch
        {
            PathType.File => FileService.GetLinesFromFile(pathArgument),
            PathType.Directory => FileService.GetLinesFromDirectory(pathArgument),

            //This should be completely unreachable because of the guard clause, but the switch expression needs it defined because the enum value for error exists.
            _ => throw new InvalidOperationException($"Invalid PathType for {pathArgument}") 
        };

        var longestWord = LongestWordService.FindLongestWord(lines);
        var transposedWord = TranspositionService.Transpose(longestWord);

        Console.WriteLine($"Longest Word: {longestWord}");
        Console.WriteLine($"Transposed Word: {transposedWord}");
    }
}
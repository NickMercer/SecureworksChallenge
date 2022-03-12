using System;
using System.IO;
using WordTransposer;

public class Program
{
    public static void Main(string[] args)
    {
        //Validate Input is a file/folder path.
        Guard.ArgumentIsProvided(args);
        
        var pathArgument = args[0];

        Guard.ArgumentIsNotEmpty(pathArgument);

        //Figure out if we have a file or a folder.
        var pathType = PathType.Error;

        if(pathArgument.Contains('.') && File.Exists(pathArgument))
        {
            pathType = PathType.File;
        }
        else if(Directory.Exists(pathArgument))
        {
            pathType = PathType.Folder;
        }

        Guard.PathTypeIsValid(pathType, pathArgument);

        //Find longest word in file/folder
        var longestWord = string.Empty;
        var longestWordService = new LongestWordService();
        
        if(pathType == PathType.File)
        {
            var lines = File.ReadLines(pathArgument);
            longestWord = longestWordService.FindLongestWord(lines);
        }
        else if(pathType == PathType.Folder)
        {
            var filePaths = Directory.GetFiles(pathArgument);

            foreach (var filePath in filePaths)
            {
                var lines = File.ReadLines(filePath);
                var longestWordInFile = longestWordService.FindLongestWord(lines);

                if(longestWordInFile.Length > longestWord.Length)
                {
                    longestWord = longestWordInFile;
                }
            }
        }

        //Transpose the word
        var longestWordChars = longestWord.ToCharArray();
        Array.Reverse(longestWordChars);

        var transposedWord = longestWordChars.ToString();

        Console.WriteLine($"Longest Word: {longestWord}");
        Console.WriteLine($"Transposed Word: {transposedWord}");
    }
}
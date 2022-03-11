using System;

public class Program
{
    public static void Main(string[] args)
    {
        if(args.Length == 0)
        {
            throw new ArgumentException("Word Transposer requires a single argument that contains a file or folder path to one or more .txt files.");
        }

        var pathArgument = args[0];


    }
}
using System.Collections.Generic;
using System.Linq;

namespace WordTransposer.TextManipulation;

public static class LongestWordService
{
    /// <summary>
    /// Searches through an enumerable of strings for the longest word made up of letters and apostrophes. Apostrophes are not included in the length of words.
    /// </summary>
    /// <param name="lines">input of words, separated by spaces, tabs, or newlines</param>
    /// <returns>longest valid word in the input enumerable</returns>
    public static string FindLongestWord(IEnumerable<string> lines)
    {
        if (lines == null)
        {
            return string.Empty;
        }

        var longestWord = string.Empty;
        var longestWordLength = 0; //This is necessary because we want to return words with apostrophes but not count the apostrophes.
        var wordDelimiters = new char[] { ' ', '\n', '\t' }; //We're assuming 'words' are separated by spaces, newlines, or tabs.

        foreach (var line in lines)
        {
            var words = line.Split(wordDelimiters);

            foreach (var word in words)
            {
                var filteredWord = SanitizeWord(word);
                var wordIsLonger = filteredWord.Length > longestWordLength;

                if (IsValidWord(word) && wordIsLonger)
                {
                    longestWord = word;
                    longestWordLength = filteredWord.Length;
                }
            }
        }

        return longestWord;
    }

    /// <summary>
    /// Returns a string with only the characters that are letters.
    /// </summary>
    /// <param name="word">string to pass in</param>
    /// <returns>string filtered to include only letters A-Z and a-z.</returns>
    private static string SanitizeWord(string word)
    {
        var wordLetters = word.Where(c => char.IsLetter(c)).ToArray();
        return new string(wordLetters);
    }

    /// <summary>
    /// Returns whether or not a word is made only with valid characters (alphabet or apostrophe)
    /// </summary>
    /// <param name="word">word to validate</param>
    /// <returns>true if valid word, false otherwise</returns>
    private static bool IsValidWord(string word)
    {
        return word.All(c => char.IsLetter(c) || c == '\'');
    }
}

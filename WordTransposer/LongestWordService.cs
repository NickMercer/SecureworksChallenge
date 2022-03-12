using System;
using System.Collections.Generic;
using System.Linq;

namespace WordTransposer;

public class LongestWordService
{
    public string FindLongestWord(IEnumerable<string> lines)
    {
        var longestWord = string.Empty;
        var wordDelimiters = new char[] { ' ', '\n', '\t' }; //We're assuming 'words' are separated by spaces, newlines, or tabs.
        
        foreach (var line in lines)
        {
            var words = line.Split(wordDelimiters);
        
            foreach (var word in words)
            {
                var filteredWord = SanitizeWord(word);

                if (filteredWord.Length > longestWord.Length)
                {
                    longestWord = filteredWord;
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
    private string SanitizeWord(string word)
    {
        var wordLetters = word.Where(c => Char.IsLetter(c)).ToArray();
        return new string(wordLetters);
    }
}

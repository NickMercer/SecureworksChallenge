using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordTransposer;
public static class TranspositionService
{
    /// <summary>
    /// Reverses the given word and returns it. 
    /// Note: Cannot reverse strings with Emoji in them correctly. Limitation in C#'s array reverse function.
    /// </summary>
    /// <param name="word">word to transpose</param>
    /// <returns>transposed word</returns>
    public static string Transpose(string word)
    {
        if(word == null)
        {
            return null;
        }

        if(string.IsNullOrWhiteSpace(word))
        {
            return string.Empty;
        }

        var wordChars = word.ToCharArray();
        Array.Reverse(wordChars);
        return new string(wordChars);
    }
}

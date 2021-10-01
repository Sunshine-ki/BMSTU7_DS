using System;
using System.Collections.Generic;

namespace Enigma
{
    public static class Constants
    {
        static public readonly List<char> alphabet = new List<char>() { 'q','w','e','r','t','y','u','i','o','p','a','s','d','f',
                                                                        'g','h','j','k','l','z','x','c','v','b','n','m',' ','.',
                                                                        'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F',
                                                                        'G','H','J','K','L','Z','X','C','V','B','N','M','?','!'};
        static public readonly int MAX_INDEX_VALUE = alphabet.Count; // Должно быть четное.
    }
}

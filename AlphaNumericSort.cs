using System;


public class AlphaNumericSort
{
    public static void Main(String[] args)
    {

    }

    //given two strings s1 and s2, return true if s1 is "smaller" than s2, or false otherwise
    //description of sorting algorithm:
    //create array of tokens for each string. A token is either a letter (a to z) or a number
    //Compare both strings' tokens one by one. If at some point they have different tokens in the same place,
    //follow these rules: if both tokens are numbers, the string with the lower number is smaller (i.e. 1 < 2)
    //if both tokens are letters, the string with the lower letter is smaller (i.e. a < b)
    //numbers are always considered smaller than letters (i.e. 342 < a)
    //If, while checking, one string runs out of tokens while the other still has more, the one with fewer tokens is smaller
    //If both have the same tokens, the one that has more leading zeroes in its first numeric token is smaller
    //Otherwise, both strings are exactly equal, return false
    //
    static bool alphanumericLess(string s1, string s2)
    {
        int a = 0; //indexer for s1
        int b = 0; //indexer for s2

        List<string> tokens1 = new List<string>();
        List<string> tokens2 = new List<string>();
        Console.WriteLine("start");
        //create token lists for both s1 and s2
        while (true)
        {
            if (a < s1.Length)
            {
                //check if we have a numeric token
                if (s1[a] >= '0' && s1[a] <= '9')
                {
                    string number = "";
                    while (a < s1.Length && s1[a] >= '0' && s1[a] <= '9')
                    {
                        number += s1[a];
                        a++;
                    }
                    tokens1.Add(number);
                }
                else //otherwise, it's a letter
                {
                    tokens1.Add(s1[a].ToString());
                    a++;
                }
            }

            //do the exact same thing as above to create the tokens of s2
            if (b < s2.Length)
            {
                if (s2[b] >= '0' && s2[b] <= '9')
                {
                    string number = "";
                    while (b < s2.Length && s2[b] >= '0' && s2[b] <= '9')
                    {
                        number += s2[b];
                        b++;
                    }
                    tokens2.Add(number);
                }
                else
                {
                    tokens2.Add(s2[b].ToString());
                    b++;
                }
            }

            //when you reach the end of both strings, break the loop
            if (a == s1.Length && b == s2.Length) break;
        }

        //Check each strings tokens 1 by 1. Also check if s1 or s2 runs out of tokens
        for (int i = 0; i < tokens1.Count; i++)
        {
            //check if s2 is out of tokens. If so, s2 is automatically smaller than s1
            if (i == tokens2.Count) return false;

            //if tokens1[i] is a number
            if (tokens1[i][0] >= '0' && tokens1[i][0] <= '9')
            {
                //if token2[i] is NOT a number, then s1 is automatically smaller than s2
                if (tokens2[i][0] < '0' || tokens2[i][0] > '9') return true;
                //otherwise, tokens2[i] IS a number, so you have to compare values
                int value1 = Int32.Parse(tokens1[i]);
                int value2 = Int32.Parse(tokens2[i]);
                if (value1 < value2) return true;
                if (value1 > value2) return false;
            }
            //if tokens1[i] is NOT a number
            else
            {
                //if tokens2[i] IS a number, s2 is automatically smaller than s1
                if (tokens2[i][0] >= '0' && tokens2[i][0] <= '9') return false;
                //otherwise, tokens2[i] is NOT a number, so compare the letters
                if (tokens1[i][0] < tokens2[i][0]) return true;
                if (tokens1[i][0] > tokens2[i][0]) return false;
            }

            //check if s1 is on its last token, but s2 still has more. If so, s1 is smaller
            if (i == tokens1.Count - 1 && i < tokens2.Count - 1) return true;
        }

        //If function hasn't returned by now, that means both strings have equivalent tokens
        //time to check which one has the most leading zeroes first
        for (int i = 0; i < tokens1.Count; i++)
        {
            //if a token is a number, start checking leading zeroes
            if (tokens1[i][0] >= '0' && tokens1[i][0] <= '9')
            {
                //reuse a and b from before
                a = 0;
                b = 0;
                bool finishedA = false;
                bool finishedB = false;
                while (!finishedA || !finishedB)
                {
                    if (a < tokens1[i].Length && tokens1[i][a] == '0')
                    {
                        a++;
                    }
                    else finishedA = true;
                    if (b < tokens2[i].Length && tokens2[i][b] == '0')
                    {
                        b++;
                    }
                    else finishedB = true;
                }
                if (a > b) return true;
                if (a < b) return false;
            }
        }

        //If function STILL hasn't returned anything, s1 and s2 are actually equal
        return false;
    }
}

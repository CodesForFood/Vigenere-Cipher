using System;
using System.Collections.Generic;

namespace VigenereCipher.Source
{
    public class Cipher
    {
        public static string IO
        {
            get { return Console.ReadLine(); }
            set { Console.WriteLine(value); }
        }

        private List<string> EncryptedWholeText = new List<string>();
        private int[] Keyword { get; set; }


        public void GetKeyword()
        {
            IO = "What's the keyword?";
            string input = IO.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                IO = "The keyword can't be blank.";
                GetKeyword();
            }
            else
            {
                try
                {
                    input.ToCharArray().CopyTo(Keyword = new int[input.Length], 0);
                    Console.Clear();
                    IO = "Keyword Saved";
                }
                catch (Exception ex)
                {
                    Error(ex, "GetKeyword");                    
                }
            }            
        }

        //Encryption Methods      
        public void EncryptText(bool isEncrypt)
        {
            try
            {
                EncryptedWholeText.Clear();
                foreach (var line in FileLoader.WholeText)
                {
                    int counter = 0;
                    List<char> encryptedLine = new List<char>();

                    for (int i = 0; i < line.Length; i++)
                    {
                        //Allows even looping of keyword
                        if ((i != 0) && (i % Keyword.Length == 0)) counter++;

                        char original = line[i];                        
                        int keyNum = GetKeyNum(i, counter);

                        //Skips \r and \n
                        if (original == 13 || original == 10) continue;

                        char encrypted = EncryptChar(original, keyNum, isEncrypt);                                              

                        encryptedLine.Add(encrypted);                       
                    }

                    EncryptedWholeText.Add(new string(encryptedLine.ToArray()));
                }

                FileLoader.WriteFile(EncryptedWholeText);
            }
            catch (Exception ex)
            {
                Error(ex, "EncryptText");
            }
        }

        // y = x - kc
        //keyCharPosition = mainLoopPosition - (lengthOfKeyWord * timesIteratedThroughKeyWord)
        private int GetKeyNum(int loopPos, int counter)
        {
            int keyCharPosition = loopPos - (Keyword.Length * counter);
            return Keyword[keyCharPosition];
        }

        //ASCII
        private char EncryptChar(int oldNum, int keyNum, bool isEncrypt)
        {
            int newNum;

            if (isEncrypt)
            {
                newNum = oldNum + keyNum;

                if (newNum > 127)
                    newNum -= 93;
            }
            else
            {
                newNum = oldNum - keyNum;

                if (newNum < 33)
                    newNum += 93;
            }

            char newChar = (char)newNum;
            return newChar;
        }

        public static void Error(Exception ex, string method)
        {
            IO = "In Method: " + method;
            IO = ex.Message;
            IO = ex.StackTrace;
        }
    }
}

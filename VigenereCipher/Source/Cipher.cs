using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereCipher.Source
{
    public class Cipher
    {
        //Auto Functions!?!?!?
        //I'm probably just lazy
        public static string IO
        {
            get { return Console.ReadLine(); }
            set { Console.WriteLine(value); }
        }

        public string[] WholeText { get; set; }
        private List<string> EncryptedWholeText = new List<string>();
        private int[] Keyword { get; set; }

        public Cipher(string[] text) { WholeText = text; }

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
                try { input.ToCharArray().CopyTo(Keyword = new int[input.Length], 0); }
                catch (Exception ex) { Error(ex, "GetKeyword"); }

                Console.Clear();
                IO = "Keyword Saved";
            }            
        }

        //Encryption Methods      
        public void EncryptText(bool isEncrypt)
        {
            try
            {
                EncryptedWholeText.Clear();
                foreach (var line in WholeText)
                {
                    int counter = 0;
                    List<char> encryptedLine = new List<char>();

                    for (int i = 0; i < line.Length; i++)
                    {
                        //Allows even looping of keyword
                        if (i != 0 && i % Keyword.Length == 0)
                            counter++;

                        char original = line[i];                        
                        int keyNum = GetKeyNum(i, counter);

                        //Skips \r and \n, as there were problems converting them
                        if (original == 13 || original == 10) continue;

                        char encrypted = EncryptChar(original, keyNum, isEncrypt);                                              

                        encryptedLine.Add(encrypted);                       
                    }

                    EncryptedWholeText.Add(new string(encryptedLine.ToArray()));
                }

                FileLoader.WriteFile(EncryptedWholeText, isEncrypt);
            }
            catch (Exception ex)
            {
                Error(ex, "EncryptText");
            }
        }

        //ASCII
        private char EncryptChar(char oldChar, int keyNum, bool isEncrypt)
        {
            int oldNum = oldChar;
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

        // y = x - kc
        //keyCharPosition = mainLoopPosition - (lengthOfKeyWord * timesIteratedThroughKeyWord)
        private int GetKeyNum(int loopPos, int counter)
        {
            int keyCharPosition = loopPos - (Keyword.Length * counter);
            return Keyword[keyCharPosition];
        }

        public static void Error(Exception ex, string method)
        {
            IO = "In Method: " + method;
            IO = ex.Message;
            IO = ex.StackTrace;
        }
    }
}

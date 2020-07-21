using System;
using VigenereCipher.Source;

namespace VigenereCipher
{
    class Program
    {
        public static string MainMenu = "=================================================  \n" +
            "<1>Encrypt File \n" +
            "<2>Decrypt File \n" +
            "<3>View File \n" +
            "<4>Pick another directory/file \n" +
            "<99>Exit \n" +
            "=================================================";

        private static FileLoader fileLoader;
        private static Cipher cipher;

        static void Main(string[] args)
        {
            fileLoader = new FileLoader();
            cipher = new Cipher(fileLoader.WholeText);

            RunProgram();

            Console.ReadKey();
        }


        static void RunProgram()
        {
            Cipher.IO = MainMenu;

            string input = Cipher.IO;

            if (int.TryParse(input, out int choice))
            {
                switch (choice)
                {

                    case 1:
                        cipher.GetKeyword();
                        cipher.EncryptText(true);
                        cipher.WholeText = fileLoader.ReReadText();
                        RunProgram();
                        break;
                    case 2:
                        cipher.GetKeyword();
                        cipher.EncryptText(false);
                        cipher.WholeText = fileLoader.ReReadText();
                        RunProgram();
                        break;
                    case 3:
                        fileLoader.PrintFile();
                        RunProgram();
                        break;
                    case 4:
                        Console.Clear();
                        fileLoader.SetDirectory();
                        cipher.WholeText = fileLoader.WholeText;
                        RunProgram();
                        break;
                    case 99:
                        Cipher.IO = "Peace out...";
                        break;
                    default:
                        Cipher.IO = "Not a valid option, try again";
                        break;
                }
            }
            else
            {
                Cipher.IO = "Invalid choice try again";
                RunProgram();
            }


        }


    }
}

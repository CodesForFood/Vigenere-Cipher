using System;
using VigenereCipher.Source;


namespace VigenereCipher
{
    class Program
    {
        public static string MainMenu =
            $"======================{(FileLoader.IsFileSet ? "File Selected" : "File Not Selected")}===========================  \n" +
            "<1>Select A File \n" +
            "<2>Encrypt File \n" +
            "<3>Decrypt File \n" +
            "<4>View File \n" +            
            "<99>Exit \n" +
            "=================================================";

    

        static void Main(string[] args)
        {
            Cipher cipher = new Cipher();
            RunProgram(cipher);

            Console.ReadKey();
        }


        static void RunProgram(Cipher cipher)
        {         
            Cipher.IO = MainMenu;

            string input = Cipher.IO;

            if (int.TryParse(input, out int choice))
            { 
                if(!FileLoader.IsFileSet && (choice != 99 && choice != 1)) { choice = 404; }

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        FileLoader.SetFile();
                        RunProgram(cipher);
                        break;
                    case 2:
                        if (FileLoader.WholeText == null) { }
                        cipher.GetKeyword();
                        cipher.EncryptText(true);
                        FileLoader.ReadFile();
                        RunProgram(cipher);
                        break;
                    case 3:
                        cipher.GetKeyword();
                        cipher.EncryptText(false);
                        FileLoader.ReadFile();
                        RunProgram(cipher);
                        break;
                    case 4:
                        FileLoader.PrintFile();
                        RunProgram(cipher);
                        break;
                    case 404:
                        Cipher.IO = "Please select a file.";
                        RunProgram(cipher);
                        break;
                    case 99:
                        Cipher.IO = "Peace out...";
                        break;
                    default:
                        Cipher.IO = "Not a valid option, try again";
                        RunProgram(cipher); 
                        break;
                }
            }
            else
            {
                Cipher.IO = "Invalid choice try again";
                RunProgram(cipher);
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VigenereCipher.Source
{
    public static class FileLoader
    {
        private static FileInfo CurrentFile { get; set; }
        public static string[] WholeText { get; private set; }

        public static bool IsFileSet
        {
            get { return (CurrentFile != null) && CurrentFile.Exists; }
        } 

        public static void SetFile()
        {
            Cipher.IO = "Enter the name and path of the file. Include the extension.";

            string inputFile = Cipher.IO;

            try
            {
                
                if ((CurrentFile = new FileInfo(inputFile)).Exists)
                {
                    Cipher.IO = "File succefully loaded";
                    ReadFile();
                }
                else
                {
                    Cipher.IO = "404: File can not be found";
                    SetFile();
                }
            }
            catch(Exception ex)
            {
                Cipher.Error(ex, "SetFile");
                SetFile();
            }
        }

        public static void ReadFile()
        {
            using (StreamReader read = new StreamReader(CurrentFile.FullName))
            {
                string whole = read.ReadToEnd();

                string[] delimitedText = whole.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                WholeText = delimitedText;
            }
        }

        public static void PrintFile()
        {
            foreach (var line in WholeText) Cipher.IO = line;
        }

        public static void WriteFile(List<string> fullText)
        {
            try
            {
                using (StreamWriter write = new StreamWriter(CurrentFile.FullName, false, Encoding.Unicode))
                {
                    foreach (var line in fullText) write.WriteLine(line);
                }
                Cipher.IO = "Wrote the file: " + CurrentFile.FullName;
            }
            catch(Exception ex)
            {
                Cipher.Error(ex, "Write File");
            }            
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VigenereCipher.Source
{
    public class FileLoader
    {
        public static DirectoryInfo CurrentDir { get; private set; }
        public static FileInfo CurrentFile {  get; private set; }
        public string[] WholeText { get; private set; }

        public FileLoader() { SetDirectory(); }  
        
        public string[] ReReadText()
        {
            ReadFile();
            return WholeText;
        }

        public  void SetDirectory()
        {
            Cipher.IO = "What is the directory of the plain text or encrypted file?";
            string inputDir = Cipher.IO;

            try
            {
                CurrentDir = new DirectoryInfo(inputDir);

                if (CurrentDir.Exists) { SetFile(); }
                else
                {
                    Cipher.IO = "404: Directory can not be found.";
                    SetDirectory();
                }
            }
            catch(Exception ex)
            {
                Cipher.IO = ex.Message + ": Inside SetDiretory";
                SetDirectory();
            }                   
        }

        private void SetFile()
        {
            Cipher.IO = "Looking in: " + CurrentDir.FullName;
            Cipher.IO = "What is the name of the file? Include the extension.";

            string inputFile = Cipher.IO;

            try
            {
                if ((CurrentFile = new FileInfo(Path.Combine(CurrentDir.FullName, inputFile))).Exists)
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
                Cipher.IO = ex.Message + ": Inside SetFile";
                SetFile();
            }

           
        }

        private void ReadFile()
        {
            using (StreamReader read = new StreamReader(CurrentFile.FullName))
            {
                string whole = read.ReadToEnd();

                string[] delimitedText = whole.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                WholeText = delimitedText;
            }
        }

        public void PrintFile()
        {
            foreach (var line in WholeText) Cipher.IO = line;
        }

        public static void WriteFile(List<string> fullText, bool isEncrypt)
        {
            /* This puts the de/encrypted text into a new file
             * Replace references of CurrentFile with testPath
            string testPath = "";
            if (isEncrypt)
                testPath = Path.Combine(CurrentDir.FullName, "enc-" + CurrentFile.Name);
            else
                testPath = Path.Combine(CurrentDir.FullName, "dec-" + CurrentFile.Name);
            */

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

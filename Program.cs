// Coder - Abhijit Biswas

using System;
using System.IO;
using System.Collections.Generic;

namespace CLOC
{
    class Program
    {
        private static string targetDirectory = "";
        private static string targetExtenstion = "*.";
        private static int totalFile = 0;
        private static int totalFolder = 0;
        private static string process = ""; 
        static List<string> folderName = new List<string>();
        static List<string> fileName = new List<string>();

        static void Main(string[] args)
        {
            Console.Title = "CLOC";
            
            Console.WriteLine("Creator - Abhijit Biswas");
            
            Console.Write("Directory Path : ");
            targetDirectory = Console.ReadLine();

            Console.Write("File Extenstion : ");
            targetExtenstion += Console.ReadLine();

            Console.Write("Show Process (Y/Any Key) : ");
            process = Console.ReadKey().KeyChar.ToString();

            if(process.ToUpper() != "Y")
            {
                Console.WriteLine("\nPLEASE WAIT");
            }
            else
            {
                Console.WriteLine("");
            }

            int[] result = CLOCS(targetDirectory, targetExtenstion);

            if(process.ToUpper() == "Y")
            {
                Console.WriteLine("Count Conplete......");
                Console.WriteLine("");
            }

            int totalLine = result[0] + result[1];

            Console.WriteLine("\nTotal Line : " + result[0] + " Total Blank Space : " + result[1] + " Total Sum : " + totalLine + " Total Folder : " + 
            totalFolder + " Total File : " + totalFile);

            Console.Write("\nShow Folder Names(Y/Any Key) : ");

            if(Console.ReadKey().KeyChar.ToString().ToUpper() == "Y")
            {
                Console.WriteLine("");
                
                foreach(string folder in folderName)
                {
                    Console.WriteLine(folder);

                }
            }

            Console.Write("\nShow File Names(Y/Any Key) : ");

            if(Console.ReadKey().KeyChar.ToString().ToUpper() == "Y")
            {
                Console.WriteLine("");

                foreach(string file in fileName)
                {
                    Console.WriteLine(file);
                }
            }

            Console.Write("\nPress Any Key To Exit...");
            Console.ReadKey();
        }

        
        private static int[] CLOCS(string path, string extension)
        {
            path = DirectoryExist(path);

            if(path != "")
            {
                List<string> subFolder = new List<string>();
                List<string> files = new List<string>();
                int[] result = new int[2] {0, 0};

                files = FilesFind(path, extension);

                totalFile += files.Count;

                if(files.Count > 0)
                {
                    foreach(string file in files)
                    {
                        fileName.Add(file.Replace(targetDirectory, ""));

                        int[] tempResult = CountLine(file);
                        result[0] += tempResult[0];
                        result[1] += tempResult[1];
                        
                        if(process.ToString().ToUpper() == "Y")
                        {
                            Console.WriteLine("\nCounting : " + file + " -> " + "Code Line : " + tempResult[0] + " " + "Blank Line : " + tempResult[1]);
                        }
                    }
                }

                subFolder = DirectoriesFind(path);
                totalFolder += subFolder.Count;
                
                if(process.ToUpper() == "Y")
                {
                    Console.WriteLine("\nSearching : " + path + " New Directory Found : " + subFolder.Count);
                }

                foreach(string folder in subFolder)
                {
                    if(process.ToUpper() == "Y")
                    {
                        Console.WriteLine("\nScanning : " + folder);
                    }

                    folderName.Add(folder.Replace(targetDirectory, ""));
                    
                    int[] tempResult = CLOCS(folder, extension);
                    result[0] += tempResult[0];
                    result[1] += tempResult[1];
                }

                return result;
            }
            else
            {
                return new int[2] {0,0};
            }
        }

        private static string DirectoryExist(string path)
        {
            if(Directory.Exists(path) == false)
            {
                targetDirectory = (Directory.Exists(Directory.GetCurrentDirectory() + path) == true)? Directory.GetCurrentDirectory() + path : "";
                return targetDirectory;
            }
            else
            {
                return path;
            }
        }

        private static List<string> DirectoriesFind(string path)
        {
            return new List<string>(Directory.EnumerateDirectories(path));
        }

        private static List<string> FilesFind(string path, string extenstion)
        {
            return new List<string>(Directory.EnumerateFiles(path, extenstion));
        }

        private static int[] CountLine(string path)
        {
            int line = 0;
            int blankLine = 0;

            using(var reader = File.OpenText(path))
            {
                while(true)
                {
                    string lineText = reader.ReadLine();

                    if(lineText == null)
                    {
                        break;
                    }
                    else if(lineText == "")
                    {
                        blankLine++;
                    }
                    else
                    {
                        line++;
                    }
                }
            }

            return new int[2] {line, blankLine};
        }
    }
}

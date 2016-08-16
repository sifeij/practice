using System;
using System.IO;
using System.Linq;
using static System.Console;

namespace Introduction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WriteLine("***************** No Linq *****************");
            ShowLargeFilesWithoutLinq();
            WriteLine(" ");
            WriteLine("***************** Linq *****************");
            ShowLargeFileWithLinq();
            WriteLine(" ");
            WriteLine("***************** Lambda *****************");
            ShowLargeFileWithLambda();
            WriteLine(" ");
        }

        static void ShowLargeFileWithLambda()
        {
            var allFiles = new DirectoryInfo(Path).GetFiles();
            var numOfFiles = NumOfFiles < allFiles.Count()
                                ? NumOfFiles
                                : allFiles.Count();
            var files = allFiles
                        .OrderByDescending(f => f.Length)
                        .Take(numOfFiles);
               
            foreach (var file in files)
            {
                WriteLine(
                    $"{file.Name,-50} : {file.Length,10:N0}");
            }
        }

        static void ShowLargeFileWithLinq()
        {
            var files = from file in new DirectoryInfo(Path).GetFiles()
                        orderby file.Length descending
                        select file;
            var numOfFiles = NumOfFiles < files.Count()
                                ? NumOfFiles
                                : files.Count();   
            foreach (var file in files.Take(numOfFiles))
            {
                WriteLine(
                    $"{file.Name,-50} : {file.Length,10:N0}");
            }
        }

        static void ShowLargeFilesWithoutLinq()
        {
            var directory = new DirectoryInfo(Path);
            FileInfo[] files = directory.GetFiles();
            Array.Sort(files, new FileInfoComparer());
            var numOfFiles = NumOfFiles < files.Length
                                ? NumOfFiles
                                : files.Length;
            WriteLine($"Showing top {numOfFiles} in {Path}");
            for (int i = 0; i < numOfFiles; i++)
            {
                FileInfo file = files[i];
                Console.WriteLine(
                    $"{file.Name,-50} : {file.Length,10:N0}");
            }
        }

        const string Path = @"../../../../Downloads"; // "/Users/sifeijiang/Downloads"
        const int NumOfFiles = 5;
    }
}

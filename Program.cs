using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace FileExplorer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File explorer [Version 1.0");
            Console.WriteLine("Copyright (c) 2018 Krusto Stoianov.  All rights reserved.");

            var input = new List<string>() { "","",""};
            var Path = @"D:\";

            var Directory = (DirectoryInfo)null;

            while(input[0].ToLower() != "exit")
            {
                Console.Write(Path+">");
                input = Console.ReadLine().Split(new char[] { ' ' },StringSplitOptions.RemoveEmptyEntries).ToList();

                if (input[0] == "cd")
                {
                    if (input[1] == "../")
                    {

                    }
                    else
                    {
                        if (input[1].StartsWith("./"))
                        {
                            int length = Path.Length;
                            Path += input[1];
                            Path = Path.Replace('.', ' ');
                            Path = Path.Replace('/', ' ');
                            Path = string.Join("",Path.Split(new char[] { ' ' },StringSplitOptions.RemoveEmptyEntries));
                        }
                        else
                        {
                            Path = input[1];
                        }
                    }
                }

                if (input[0] == "ls")
                {
                    Directory = new DirectoryInfo(Path);

                    var RawFiles = new List<FileInfo>();
                    var Directories = new List<DirectoryInfo>();

                    RawFiles = Directory.GetFiles().ToList();
                    Directories = Directory.GetDirectories().ToList();
                    Console.WriteLine();

                    for (int i = 0; i < Directories.Count; i++)
                    {

                        string Pattern = "\\w+[a-zA-Z1-9].+";
                        string Name = new Regex(Pattern).Match(Directories[i].FullName).Value;
                        if (Name.Length != 0)
                        {
                            Console.SetCursorPosition(4, Console.CursorTop);
                            Console.WriteLine("DIR");

                            Console.SetCursorPosition(25, Console.CursorTop - 1);
                            Console.WriteLine(Name);
                        }
                    }
                    for (int i = 0; i < RawFiles.Count; i++)
                    {
                        string Pattern = "\\w+[a-zA-Z1-9. ]+";
                        string Name = new Regex(Pattern).Match(RawFiles[i].FullName).Value;

                        Console.SetCursorPosition(4, Console.CursorTop);
                        Console.WriteLine("FILE");

                        Console.SetCursorPosition(10, Console.CursorTop - 1);
                        Console.WriteLine($"{string.Format("{0:D}", (RawFiles[i].Length/(1024)))} ");

                        Console.SetCursorPosition(19, Console.CursorTop - 1);
                        Console.WriteLine("Kb");

                        Console.SetCursorPosition(25, Console.CursorTop-1);
                        Console.WriteLine(Name);

                    }

                }

                if (input[0] == "cls")
                {
                    Console.Clear();
                }
            }

        }
    }
}

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
            Console.WriteLine("File explorer [Version 1.0]");
            Console.WriteLine("Copyright (c) 2018 Krusto Stoianov.  All rights reserved.");
            Console.WriteLine();
            var input = new List<string>() { "", "", "" };
            var Path = @"D:\program ";

            var Directory = (DirectoryInfo)null;

            while (input[0].ToLower() != "exit")
            {
                Console.Write(Path + ">");
                input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                Directory = new DirectoryInfo(Path);

                if (input[0] == "cd")
                {
                    try {
                        if (input[1].StartsWith("./"))
                        {
                            if (input[1].Length != 0 && !input[1].EndsWith("./"))
                            {
                                int length = Path.Length;
                                if (!Path.EndsWith("\\"))
                                {
                                    Path += "\\";
                                }
                                Path += input[1];
                                Path = Path.Replace('.', ' ');
                                Path = Path.Replace('/', ' ');
                                Path = string.Join("", Path.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                                Path += " ";
                            }
                        }
                        else if (input[1] == "../")
                        {
                            Path = Path.Replace(Directory.Name,"");
                            Path = Path.Remove(Path.Length-2,1) ;
                        }
                        else
                        {
                            if(new DirectoryInfo(Path + "\\" + input[1]).Exists)
                            {
                                Path += "\\" + input[1];
                                Path = Path.Replace('/', ' ');
                                Path = string.Join("", Path.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)) + " ";
                            }
                        }
                    } catch
                    {
                    }
                }
                if (input[0] == "ls")
                {

                    var RawFiles = new List<FileInfo>();
                    var Directories = new List<DirectoryInfo>();

                    RawFiles = Directory.GetFiles().ToList();
                    Directories = Directory.GetDirectories().ToList();
                    Console.WriteLine();

                    for (int i = 0; i < Directories.Count; i++)
                    {

                        if (Directories[i].Name.Length != 0)
                        {
                            Console.SetCursorPosition(4, Console.CursorTop);
                            Console.WriteLine("DIR");

                            Console.SetCursorPosition(25, Console.CursorTop - 1);
                            Console.WriteLine(Directories[i].Name);
                        }
                    }
                    for (int i = 0; i < RawFiles.Count; i++)
                    {

                        Console.SetCursorPosition(4, Console.CursorTop);
                        Console.WriteLine("FILE");

                        Console.SetCursorPosition(10, Console.CursorTop - 1);
                        Console.WriteLine($"{string.Format("{0:D}", (RawFiles[i].Length / (1024)))} ");

                        Console.SetCursorPosition(19, Console.CursorTop - 1);
                        Console.WriteLine("Kb");

                        Console.SetCursorPosition(25, Console.CursorTop - 1);
                        Console.WriteLine(RawFiles[i].Name);

                    }

                }
                if (input[0] == "copy")
                {
                    string file = "";
                    try
                    {
                        file = Directory.GetFiles().Where(x => x.FullName.Contains(input[1])).ElementAt(0).FullName;
                        try
                        {
                            string destinationFolder = input[2];

                            if (new DirectoryInfo(destinationFolder).Exists)
                            {
                                var currentFile = new FileInfo(file);
                                currentFile.CopyTo(destinationFolder + "\\" + currentFile.Name);
                                Console.WriteLine("Operation successful!");
                            }
                            else
                            {
                                Console.WriteLine("Destination folder does not exist!");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Wrong destination path parameter!");
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"Wrong file name parameter!");
                    }


                }
                if (input[0] == "cut")
                {
                    string file = "";
                    try
                    {
                        file = Directory.GetFiles().Where(x => x.FullName.Contains(input[1])).ElementAt(0).FullName;
                        try
                        {
                            string destinationFolder = input[2];

                            if (new DirectoryInfo(destinationFolder).Exists)
                            {
                                var currentFile = new FileInfo(file);
                                currentFile.MoveTo(destinationFolder + "\\" + currentFile.Name);
                                Console.WriteLine("Operation successful!");
                            }
                            else
                            {
                                Console.WriteLine("Destination folder does not exist!");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Wrong destination path parameter!");
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"Wrong file name parameter!");
                    }


                }
                if (input[0] == "remove")
                {
                    string file = "";
                    try
                    {
                        file = Directory.GetFiles().Where(x => x.FullName.Contains(input[1])).ElementAt(0).FullName;
                        try
                        {
                            File.Delete(file);
                            Console.WriteLine("Opration successfull!");
                        }
                        catch
                        {
                            Console.WriteLine("Operation unsuccessfull!");
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"Wrong file name parameter!");
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

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;

namespace ConsoleApp8
{
    static class Menu
    {
        public static int strela(int minposition, int maxposition)
        {
            int poz = minposition;
            ConsoleKeyInfo key = Console.ReadKey();
            while (key.Key != ConsoleKey.Enter)
            {
                Console.SetCursorPosition(minposition, poz);
                Console.WriteLine("   ");
                if (key.Key == ConsoleKey.UpArrow)
                {
                    poz--;
                    if (poz < minposition)
                    {
                        poz = maxposition;
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    poz++;
                    if (poz > maxposition)
                    {
                        poz = minposition;
                    }

                }
                else if(key.Key == ConsoleKey.Escape)
                {
                    poz = -1;
                    break;
                }
                else if (key.Key == ConsoleKey.F1) { Environment.Exit(0);}
             
                Console.SetCursorPosition(0, poz);
                Console.WriteLine(" ->");
                key = Console.ReadKey();
                
            }
            Console.Clear();
            return poz;
        }
    }
    static class Papka
    {
        private static void ShowPapka(string path)
        {
            while(true)
            {
                Console.Clear();
                string[] paths = Directory.GetDirectories(path);
                string[] file = Directory.GetFiles(path);
                string[] type = paths.Concat(file).ToArray();
                List<DateTime> data = new List<DateTime>();
                foreach (string path2 in paths)
                {
                    Console.WriteLine("    " + Path.GetFileName(path2));
                    data.Add(Directory.GetCreationTime(path2));
                }
                foreach (string file2 in file)
                {
                    Console.WriteLine("    " + Path.GetFileName(file2));
                    data.Add(Directory.GetCreationTime(file2));
                }

                for (int i = 0; i < data.Count; i++)
                {
                    Console.SetCursorPosition(50, i);
                    Console.WriteLine(data[i]);
                }
                for (int i = 0; i < type.Length; i++)
                {
                    Console.SetCursorPosition(100, i);
                    Console.WriteLine(Path.GetExtension(type[i]));
                }

                int poz = Menu.strela(0, type.Length - 1);
                if (poz == -1)
                {
                    try
                    {
                        string b = Path.GetDirectoryName(path);
                        ShowPapka(b);
                    }
                    catch
                    {
                        ShowDisk();
                    }
                }
                else
                {
                    if (type[poz].Contains("."))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = type[poz],
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        ShowPapka(paths[poz]);
                    }
                }
            }
        }
        private static string ShowDisk()
        {
            Console.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            List<string> strDrive = new List<string>();
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine("   Имя диска: " + drive.Name + ToGB(drive.AvailableFreeSpace)+ "  ГБ" +  "  Свободно из  " + ToGB(drive.TotalSize) + "  ГБ" );
                
                strDrive.Add(drive.Name);
            }
            Console.WriteLine("\n   Чтобы зевершить программу нажмите F1");
            int poz = Menu.strela(0, strDrive.Count - 1);
            return strDrive[poz];
        }
        private static long ToGB(long byteCount)
        {
            return byteCount / 1024 / 1024 / 1024;
        }
        public static void ShowProvodnik()
        {
            string path = ShowDisk();
            ShowPapka(path);
        }
    }

}

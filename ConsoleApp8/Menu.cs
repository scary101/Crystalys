using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp8
{
    static class Menu
    {
        public static int strela(int minposition, int maxposition, string path)
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
        public static void ShowPapka(string path)
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

                int poz = Menu.strela(0, type.Length - 1, path);
                if (poz == -1)
                {
                    string b = Path.GetDirectoryName(path);
                    ShowPapka(b);
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
    }

}

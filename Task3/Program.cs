using System;
using System.IO;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {

            string dirPath = @"C:\SkillFactory\WorkWithFiles";

            double dirSizeBefore = 0;
            double dirSizeAfter = 0;

            DirectoryVolume(dirPath, ref dirSizeBefore);

            Console.WriteLine("Текущий размер папки {0} байт", dirSizeBefore);

            DirectoryCleaner(dirPath);
            DirectoryVolume(dirPath, ref dirSizeAfter);

            Console.WriteLine("Удалено {0} байт", dirSizeBefore);
            Console.WriteLine("Текущий размер папки {0} байт", dirSizeAfter);
        }

        static void DirectoryVolume(string dirPath, ref double dirSize)
        {
            if (Directory.Exists(dirPath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirPath);

                foreach (var file in dirInfo.GetFiles())
                {
                    dirSize += file.Length;
                }

                foreach (var dir in dirInfo.GetDirectories())
                {
                    DirectoryVolume(dir.FullName, ref dirSize);
                }
            }
        }

        static void DirectoryCleaner(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirPath);

                foreach (var dir in dirInfo.GetDirectories())
                {
                    if (DateTime.Now - dir.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        dir.Delete(true);
                        Console.WriteLine("Папка удалена {0}", dir.Name);
                    }
                    else
                        Console.WriteLine("Условия не выполнены");
                }

                foreach (var file in dirInfo.GetFiles())
                {
                    if (DateTime.Now - file.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        file.Delete();
                        Console.WriteLine("Файл удален {0}", file.Name);
                    }
                    else
                        Console.WriteLine("Условия не выполнены");
                }
            }

            else
            {
                Console.WriteLine("Указан неверный путь");
            }
        }
    }
}

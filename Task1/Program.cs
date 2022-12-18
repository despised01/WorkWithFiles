using System;
using System.IO;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {

            string dirPath = @"C:\SkillFactory\WorkWithFiles";

            FolderCleaner(dirPath);
        }

        static void FolderCleaner(string dirPath)
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

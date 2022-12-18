using System;
using System.IO; 

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirPath = @"C:\SkillFactory\WorkWithFiles";

            double dirSize = 0;

            DirectoryVolume(dirPath, ref dirSize);

            Console.WriteLine("Размер папки {0} байт", dirSize);
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
    }
}

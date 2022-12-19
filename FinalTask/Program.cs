using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string programPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string dirPath = Path.Combine(programPath, "Students");
            string filePath = Path.Combine(programPath, "Students.dat");

            try
            {
                DirectoryCreate(dirPath);
                DirectorySort(ref dirPath, ref filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка {0}", ex.Message);
            }
        }

        static void DirectoryCreate(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
                dirInfo.Delete(true);
            }

            Directory.CreateDirectory(dirPath);

            Console.WriteLine("Создана папка 'Students'.");
        }

        static Student[] FileSerializer(string filePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using var fs = new FileStream(filePath, FileMode.Open);
            var students = (Student[])formatter.Deserialize(fs);

            Console.WriteLine("Файл 'Students.dat' десериализован.");

            return students;
        }

        static void DirectorySort(ref string dirPath, ref string filePath)
        {
            var students = FileSerializer(filePath);

            foreach (var student in students)
            {
                string programPath = Path.Combine(dirPath, student.Group + ".txt");

                if (!File.Exists(programPath))
                {
                    using (StreamWriter sw = File.CreateText(programPath))
                    {
                        foreach (var stud in students)
                        {
                            if (stud.Group == student.Group)
                            {
                                sw.WriteLine("{0} [{1}]", stud.Name, stud.DateOfBirth.ToShortDateString());
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Сортировка данных завершена");

            string[] files = Directory.GetFiles(dirPath);

            foreach (var data in files)
            {
                Console.WriteLine(data);
            }
        }

        [Serializable]
        class Student
        {
            public string Name { get; set; }
            public string Group { get; set; }
            public DateTime DateOfBirth { get; set; }

            public Student(string Name, string Group, DateTime DateOfBirth)
            {
                this.Name = Name;
                this.Group = Group;
                this.DateOfBirth = DateOfBirth;
            }
        }
    }
}

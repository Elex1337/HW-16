using System;
using System.IO;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Просмотр содержимого директории");
            Console.WriteLine("2. Создание файла/директории");
            Console.WriteLine("3. Удаление файла/директории");
            Console.WriteLine("4. Копирование файла/директории");
            Console.WriteLine("5. Перемещение файла/директории");
            Console.WriteLine("6. Чтение из файла");
            Console.WriteLine("7. Запись в файл");
            Console.WriteLine("0. Выход");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ListDirectoryContents();
                    break;
                case "2":
                    CreateFileOrDirectory();
                    break;
                case "3":
                    DeleteFileOrDirectory();
                    break;
                case "4":
                    CopyFileOrDirectory();
                    break;
                case "5":
                    MoveFileOrDirectory();
                    break;
                case "6":
                    ReadFromFile();
                    break;
                case "7":
                    WriteToFile();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный ввод. Пожалуйста, выберите действие из списка.");
                    break;
            }
        }
    }

    static void ListDirectoryContents()
    {
        Console.WriteLine("Введите путь к директории:");
        string path = Console.ReadLine();

        try
        {
            string[] files = Directory.GetFiles(path);
            string[] directories = Directory.GetDirectories(path);

            Console.WriteLine("Файлы:");
            foreach (string file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }

            Console.WriteLine("Директории:");
            foreach (string directory in directories)
            {
                Console.WriteLine(Path.GetFileName(directory));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void CreateFileOrDirectory()
    {
        Console.WriteLine("Введите путь и имя файла/директории:");
        string path = Console.ReadLine();

        try
        {
            Console.WriteLine("Выберите тип (F - файл, D - директория):");
            char type = Console.ReadLine().ToUpper()[0];

            if (type == 'F')
            {
                File.Create(path).Close();
                Console.WriteLine("Файл создан.");
            }
            else if (type == 'D')
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("Директория создана.");
            }
            else
            {
                Console.WriteLine("Неверный ввод. Выберите F или D.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void DeleteFileOrDirectory()
    {
        Console.WriteLine("Введите путь к файлу/директории для удаления:");
        string path = Console.ReadLine();

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                Console.WriteLine("Файл удален.");
            }
            else if (Directory.Exists(path))
            {
                Directory.Delete(path);
                Console.WriteLine("Директория удалена.");
            }
            else
            {
                Console.WriteLine("Файл или директория не существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void CopyFileOrDirectory()
    {
        Console.WriteLine("Введите путь к исходному файлу/директории:");
        string sourcePath = Console.ReadLine();

        Console.WriteLine("Введите путь к новому файлу/директории:");
        string destinationPath = Console.ReadLine();

        try
        {
            if (File.Exists(sourcePath))
            {
                File.Copy(sourcePath, destinationPath);
                Console.WriteLine("Файл скопирован.");
            }
            else if (Directory.Exists(sourcePath))
            {
                DirectoryCopy(sourcePath, destinationPath, true);
                Console.WriteLine("Директория скопирована.");
            }
            else
            {
                Console.WriteLine("Исходный файл или директория не существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void MoveFileOrDirectory()
    {
        Console.WriteLine("Введите путь к исходному файлу/директории:");
        string sourcePath = Console.ReadLine();

        Console.WriteLine("Введите путь к новому файлу/директории:");
        string destinationPath = Console.ReadLine();

        try
        {
            if (File.Exists(sourcePath))
            {
                File.Move(sourcePath, destinationPath);
                Console.WriteLine("Файл перемещен.");
            }
            else if (Directory.Exists(sourcePath))
            {
                Directory.Move(sourcePath, destinationPath);
                Console.WriteLine("Директория перемещена.");
            }
            else
            {
                Console.WriteLine("Исходный файл или директория не существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void ReadFromFile()
    {
        Console.WriteLine("Введите путь к файлу для чтения:");
        string filePath = Console.ReadLine();

        try
        {
            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                Console.WriteLine($"Содержимое файла:\n{content}");
            }
            else
            {
                Console.WriteLine("Файл не существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void WriteToFile()
    {
        Console.WriteLine("Введите путь к файлу для записи:");
        string filePath = Console.ReadLine();

        Console.WriteLine("Введите текст для записи в файл:");
        string content = Console.ReadLine();

        try
        {
            File.WriteAllText(filePath, content);
            Console.WriteLine("Текст записан в файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    {
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);
        DirectoryInfo[] dirs = dir.GetDirectories();

        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
        }

        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string tempPath = Path.Combine(destDirName, file.Name);
            file.CopyTo(tempPath, true);
        }

        if (copySubDirs)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
                string tempPath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
            }
        }
    }
}

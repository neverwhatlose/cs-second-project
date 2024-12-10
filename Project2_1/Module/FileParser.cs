using System.Security;
using System.Text;

namespace Project2_1.Module;

public static class FileParser
{
    public static readonly string DefaultInputPath = GetProjectDirectory() + $"{Path.DirectorySeparatorChar}File{Path.DirectorySeparatorChar}Input{Path.DirectorySeparatorChar}weatherAUS.csv";
    public static string customInputPath { get; private set; } = DefaultInputPath;

    //TODO: Check for correct file structure
    public static void SetNewFilePath(string path, ref string result, ref bool successfulExecution)
    {
        path = path.Replace("/", Path.DirectorySeparatorChar.ToString()).Replace("\\", Path.DirectorySeparatorChar.ToString());
        try
        {
            string[]? file = File.Exists(path) ? File.ReadAllLines(path) : null;

            if (path == DefaultInputPath)
            {
                customInputPath = DefaultInputPath;
                result = "Данные успешно прочитаны!";
                successfulExecution = true;
            }
            if (file is not null)
            {
                customInputPath = path;
                result = $"Путь успешно изменен на: {path}";
                successfulExecution = true;
            }
            else
            {
                result = "Файл не найден! Укажите новый путь к файлу";
                successfulExecution = false;
            }
        }
        catch (FileNotFoundException)
        {
            result = "Файл не найден! Укажите новый путь к файлу";
            successfulExecution = false;
        }
    }

    public static bool IsDefaultPathValid()
    {
        return File.Exists(DefaultInputPath);
    }
    

    public static string[]? ReadLines()
    {
        try
        {
            return File.ReadAllLines(customInputPath, Encoding.UTF8);
        }
        catch (Exception ex) when (ex is IOException or SecurityException or UnauthorizedAccessException)
        {
            Console.WriteLine("Возникла ошибка при чтении файла: " + ex.Message);
            return null;
        }
    }
    
    public static void WriteToFile(string path, List<string> content, ref bool successfulExecution)
    {
        try
        {
            File.WriteAllLines(path, content, Encoding.UTF8);
            successfulExecution = true;
        }
        catch (Exception ex) when (ex is IOException or SecurityException or UnauthorizedAccessException)
        {
            successfulExecution = false;
            throw new Exception("Возникла ошибка при записи в файл: " + ex.Message);
        }
    }
    
    public static string GetProjectDirectory()
    {
        return string.Join(" ", Directory.GetCurrentDirectory().Replace(" ",
                    "РАЗДЕЛИТЕЛЬ,ИСПОЛЬЗУЕМЫЙ_ДЛЯ_КОРРЕКТНОГО_ПОИСКА_ФАЙЛА_ДАЖЕ_С_УЧЕТОМ_ПРОБЕЛОВ_В_НАЗВАНИИ_ПАПКИ")
                .Split(Path.DirectorySeparatorChar)[..^3])
            .Replace(" ", Path.DirectorySeparatorChar.ToString()).Replace(
                "РАЗДЕЛИТЕЛЬ,ИСПОЛЬЗУЕМЫЙ_ДЛЯ_КОРРЕКТНОГО_ПОИСКА_ФАЙЛА_ДАЖЕ_С_УЧЕТОМ_ПРОБЕЛОВ_В_НАЗВАНИИ_ПАПКИ", " ");
    }
}
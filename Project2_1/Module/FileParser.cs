using System.Security;
using System.Text;

namespace Project2_1.Module;

/// <summary>
/// Класс для работы с файлами
/// </summary>
public static class FileParser
{
    // Путь к папке проекта
    public static readonly string ProjectDirectory = string.Join(" ", Directory.GetCurrentDirectory().Replace(" ",
                "РАЗДЕЛИТЕЛЬ,ИСПОЛЬЗУЕМЫЙ_ДЛЯ_КОРРЕКТНОГО_ПОИСКА_ФАЙЛА_ДАЖЕ_С_УЧЕТОМ_ПРОБЕЛОВ_В_НАЗВАНИИ_ПАПКИ")
                .Split(Path.DirectorySeparatorChar)[..^3])
                .Replace(" ", Path.DirectorySeparatorChar.ToString()).Replace(
                "РАЗДЕЛИТЕЛЬ,ИСПОЛЬЗУЕМЫЙ_ДЛЯ_КОРРЕКТНОГО_ПОИСКА_ФАЙЛА_ДАЖЕ_С_УЧЕТОМ_ПРОБЕЛОВ_В_НАЗВАНИИ_ПАПКИ", " ");
    // Путь к файлу по умолчанию
    public static readonly string DefaultInputPath = ProjectDirectory + $"{Path.DirectorySeparatorChar}File{Path.DirectorySeparatorChar}Input{Path.DirectorySeparatorChar}weatherAUS.csv";
    // Путь к файлу, который указал пользователь
    private static string CustomInputPath { get; set; } = DefaultInputPath;

    /// <summary>
    /// Устанавливает новый путь к файлу
    /// </summary>
    /// <param name="path">Полный путь к файлу</param>
    /// <param name="result">Результат установки нового пути</param>
    /// <param name="successfulExecution">True - если удалось изменить директорию, false - в остальных случаях</param>
    public static void SetNewFilePath(string path, ref string result, ref bool successfulExecution)
    {
        // Заменяем все слеши на текущую ОС
        path = path.Replace("/", Path.DirectorySeparatorChar.ToString()).Replace("\\", Path.DirectorySeparatorChar.ToString());
        try
        {
            // Считываем все строки из файла
            string[]? file = File.Exists(path) ? File.ReadAllLines(path) : null;

            if (path == DefaultInputPath)
            {
                CustomInputPath = DefaultInputPath;
                result = "Данные успешно прочитаны!";
                successfulExecution = true;
            }
            if (file is not null)
            {
                CustomInputPath = path;
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

    /// <summary>
    /// Читает все строки из файла
    /// </summary>
    /// <returns>Список считанных строк из файла</returns>
    public static string[]? ReadLines()
    {
        try
        {
            return File.ReadAllLines(CustomInputPath, Encoding.UTF8);
        }
        catch (Exception ex) when (ex is IOException or SecurityException or UnauthorizedAccessException)
        {
            Console.WriteLine("Возникла ошибка при чтении файла: " + ex.Message);
            return null;
        }
    }

    /// <summary>
    /// Записывает строки в файл
    /// </summary>
    /// <param name="path">Путь к файлу</param>
    /// <param name="content">Список строк для записи в файл</param>
    /// <param name="successfulExecution">True - если удалось записать данные в файл, false - в остальных случаях</param>
    /// <exception cref="Exception">При неудачной записи данных в файл</exception>
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
}

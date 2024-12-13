using Project2_1.Module;

namespace Project2_1.Tasks.Core;

/// <summary>
/// Класс задачи "Изменение директории входного файла"
/// </summary>
/// <param name="name">Техническое название задачи</param>
/// <param name="description">Название задачи понятное пользователю</param>
/// <param name="prompt">Описание задачи</param>
public class SetFilePath(string name, string description, string prompt) : Task(name, description, prompt)
{
    /// <summary>
    /// Основной конструктор задачи
    /// </summary>
    public SetFilePath() : this("SetFilePath", 
        "Изменить директорию входного файла", 
        $"Введите путь к входному файлу (относительно папки проекта) или же нажмите Enter, чтобы использовать директорию по умолчанию: {FileParser.DefaultInputPath}") { }

    /// <summary>
    /// Переопределенный метод выполнения задачи
    /// </summary>
    /// <param name="successfulExecution">Позволяет отслеживать успешность выполнения задачи</param>
    /// <param name="result">Возвращаемый задачей результат</param>
    public override void Execute(ref bool successfulExecution, ref string result)
    {
        string? input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Чтение входного файла...");
            
            FileParser.SetNewFilePath(FileParser.DefaultInputPath, ref result, ref successfulExecution);
        }
        else
        {
            Console.WriteLine("Чтение входного файла...");
            FileParser.SetNewFilePath(input, ref result, ref successfulExecution);
        }
    }
}
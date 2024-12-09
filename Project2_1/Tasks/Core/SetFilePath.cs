using Project2_1.Module;

namespace Project2_1.Tasks.Core;

public class SetFilePath(string name, string description, string prompt) : Task(name, description, prompt)
{
    public SetFilePath() : this("SetFilePath", 
        "Изменить директорию входного файла", 
        $"Введите путь к входному файлу (относительно папки проекта) или же нажмите Enter, чтобы использовать директорию по умолчанию: {FileParser.DefaultInputPath}") { }

    public override void Execute(ref bool successfulExecution, ref string result)
    {
        string? input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Чтение входного файла...");
            // парсим дефолтный путь
            if (FileParser.IsDefaultPathValid())
            {
                FileParser.SetNewFilePath(FileParser.DefaultInputPath, ref result, ref successfulExecution);
            }
            else
            {
                result = "Файл не найден!";
                successfulExecution = false;
            }
        }
        else
        {
            Console.WriteLine("Чтение входного файла...");
            FileParser.SetNewFilePath(input, ref result, ref successfulExecution);
        }
    }
}
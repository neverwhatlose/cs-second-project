namespace Project2_1.Tasks.Core;

/// <summary>
/// Класс задачи "Завершение программы"
/// </summary>
/// <param name="name">Техническое название задачи</param>
/// <param name="description">Название задачи понятное пользователю</param>
/// <param name="prompt">Описание задачи</param>
public class Terminate(string name, string description, string prompt) : Task(name, description, prompt)
{
    /// <summary>
    /// Основной конструктор задачи
    /// </summary>
    public Terminate() : this("Terminate", 
        "Завершить программу", 
        "Нажмите Enter для завершения программы") { }

    /// <summary>
    /// Переопределенный метод выполнения задачи
    /// </summary>
    /// <param name="successfulExecution">Позволяет отслеживать успешность выполнения задачи</param>
    /// <param name="result">Возвращаемый задачей результат</param>
    public override void Execute(ref bool successfulExecution, ref string result)
    {
        Console.ReadKey();
        Environment.Exit(0);
    }
}
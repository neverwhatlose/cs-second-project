namespace Project2_1.Tasks;

/// <summary>
/// Абстрактный класс, описывающий основные свойства и методы любой задачи
/// </summary>
/// <param name="name">Техническое название задачи</param>
/// <param name="description">Название задачи понятное пользователю</param>
/// <param name="prompt">Описание задачи</param>
public abstract class Task(string name, string description, string prompt)
{
    public string Name { get; } = name;
    public string Description { get; } = description;
    public string Prompt { get; } = prompt;

    /// <summary>
    /// Метод, выполняющий задачу
    /// </summary>
    /// <param name="successfulExecution">Позволяет отслеживать успешность выполнения задачи</param>
    /// <param name="result">Возвращаемый задачей результат</param>
    public abstract void Execute(ref bool successfulExecution, ref string result);
    
    /// <summary>
    /// Отображает информацию о задаче
    /// </summary>
    public void ShowInfo()
    {
        Console.WriteLine($"Название: {Description}");
        Console.WriteLine($"Описание: {Prompt}");
    }
}
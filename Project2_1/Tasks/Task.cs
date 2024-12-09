namespace Project2_1.Tasks;

/// <summary>
/// Represents an abstract task with a name, description, and prompt.
/// </summary>
/// <param name="name">Param used by the system to determine the task ID</param>
/// <param name="description">Param with the name of the task</param>
/// <param name="prompt">Param with the description of the task</param>
public abstract class Task(string name, string description, string prompt)
{
    public string Name { get; } = name;
    public string Description { get; } = description;
    public string Prompt { get; } = prompt;

    public abstract void Execute(ref bool successfulExecution, ref string result);
    
    public void ShowInfo()
    {
        Console.WriteLine($"Название: {Description}");
        Console.WriteLine($"Описание: {Prompt}");
    }
}
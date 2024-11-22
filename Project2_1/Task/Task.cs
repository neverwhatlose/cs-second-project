namespace Project2_1.Task;

public abstract class Task(string name, string description)
{
    private string Name { get; } = name;
    private string Description { get; } = description;
    
    public abstract void Execute(string input);
    
    public void ShowInfo()
    {
        Console.WriteLine($"Название: {Name}");
        Console.WriteLine($"Описание: {Description}");
    }
}
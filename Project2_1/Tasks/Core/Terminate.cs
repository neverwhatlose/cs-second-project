namespace Project2_1.Tasks.Core;

public class Terminate(string name, string description, string prompt) : Task(name, description, prompt)
{
    public Terminate() : this("Terminate", 
        "Завершить программу", 
        "Нажмите Enter для завершения программы") { }

    public override void Execute(ref bool successfulExecution, ref string result)
    {
        Console.ReadKey();
        Environment.Exit(0);
    }
}
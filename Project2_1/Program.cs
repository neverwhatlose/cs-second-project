using System.Globalization;
using Project2_1.Module;
using Project2_1.Tasks;

namespace Project2_1;

public class MainClass
{
    public static void Main()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
        
        Terminal terminal = new Terminal();
        bool successfulExecution = false;
        while (!successfulExecution)
        {
            terminal.SetTask(TaskListName.SetFilePath).Execute(ref successfulExecution);
        }
        Console.WriteLine("Нажмите Enter, чтобы продолжить");
        Console.ReadKey();

        terminal.ShowTaskList();
        while (true)
        {
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int number))
            {
                Console.Clear();
                
                terminal.SetTask((TaskListName)number).Execute();
                
                Console.WriteLine("Нажмите Enter, чтобы продолжить");
                Console.ReadKey();
                terminal.ShowTaskList();
            }
        }
    }
}
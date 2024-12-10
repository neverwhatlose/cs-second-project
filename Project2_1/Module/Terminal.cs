using Project2_1.Tasks;
using Project2_1.Tasks.Additional;
using Project2_1.Tasks.Core;

namespace Project2_1.Module;

/// <summary>
/// Класс, представляющий терминал для выполнения задач.
/// </summary>
public class Terminal
{
    // Неизменяемый список задач. Порядок задач соответствует порядку в перечислении TaskListName
    private static readonly Tasks.Task[] TaskList = [new SetFilePath(), new WeatherInSydney(), new ShowStatistics(), new ProlongedSunshine(), new SortedByRainfall(), new Terminate()];
    private int _currentTask = -1;
    
    /// <summary>
    /// Устанавливает текущую задачу.
    /// </summary>
    /// <param name="taskName">Название задачи</param>
    /// <returns>Терминал. Возвращает именно такое значение для компактного и последовательного выполнения задач</returns>
    public Terminal SetTask(TaskListName taskName)
    {
        for (int i = 0; i < TaskList.Length; i++)
        {
            if (TaskList[i].Name.Equals(taskName.ToString()))
            {
                _currentTask = i;
                return this;
            }
        }
        Console.WriteLine("Такой задачи не существует!");

        return this;
    }
    
    /// <summary>
    /// Выполняет текущую задачу.
    /// </summary>
    /// <returns>Терминал. Возвращает именно такое значение для компактного и последовательного выполнения задач</returns>
    public Terminal Execute()
    {
        bool successfulExecution = false;
        string result = "";

        if (_currentTask == -1)
        {
            Console.WriteLine("Выберите задачу, прежде чем исполнять ее!");
            return this;
        }
    
        TaskList[_currentTask].ShowInfo();

        TaskList[_currentTask].Execute(ref successfulExecution, ref result);
        Console.WriteLine(successfulExecution ? result : $"В ходе выполнения задачи возникла ошибка: {result}");
        _currentTask = -1;

        return this;
    }
    
    /// <summary>
    /// Выполняет текущую задачу. Перегруженный метод, позволяющий получить результат выполнения задачи.
    /// </summary>
    /// <param name="successfulExecution">Результат выполнения задачи</param>
    /// <returns>Терминал. Возвращает именно такое значение для компактного и последовательного выполнения задач</returns>
    public Terminal Execute(ref bool successfulExecution)
    {
        string result = "";

        if (_currentTask == -1)
        {
            Console.WriteLine("Выберите задачу, прежде чем исполнять ее!");
            return this;
        }

        TaskList[_currentTask].ShowInfo();

        TaskList[_currentTask].Execute(ref successfulExecution, ref result);
        Console.WriteLine(successfulExecution ? result : $"В ходе выполнения задачи возникла ошибка: {result}\n");
        _currentTask = -1;

        return this;
    }

    /// <summary>
    /// Выводит список доступных задач.
    /// </summary>
    /// <returns>Терминал. Возвращает именно такое значение для компактного и последовательного выполнения задач</returns>
    public Terminal ShowTaskList()
    {
        Console.Clear();

        for (int i = 0; i < TaskList.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {TaskList[i].Description}");
        }

        return this;
    }
}
/*
 * Проект 2 - пересдача
 * Группа: БПИ249-1
 * Студент: Селчук Эмин Абдулкеримович
 * Дата: 10.12.24
 */

using System.Globalization;
using Project2_1.Module;
using Project2_1.Tasks;

namespace Project2_1;

/// <summary>
/// Главный класс программы
/// </summary>
public class MainClass
{
    /// <summary>
    /// Точка входа в программу
    /// </summary>
    public static void Main()
    {
        // Установка культуры по умолчанию, чтобы избежать проблем с разделителями
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
        
        Terminal terminal = new Terminal();
        bool successfulExecution = false;
        
        // Установка пути к файлу
        while (!successfulExecution)
        {
            terminal.SetTask(TaskListName.SetFilePath).Execute(ref successfulExecution);
        }
        Console.WriteLine("Нажмите Enter, чтобы продолжить");
        Console.ReadKey();

        terminal.ShowTaskList();
        
        // Основной цикл программы
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
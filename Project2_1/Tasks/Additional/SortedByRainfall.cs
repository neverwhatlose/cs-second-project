using Project2_1.Module;

namespace Project2_1.Tasks.Additional;

/// <summary>
/// Класс задачи "Сортировка по количеству осадков"
/// </summary>
/// <param name="name">Техническое название задачи</param>
/// <param name="description">Название задачи понятное пользователю</param>
/// <param name="prompt">Описание задачи</param>
public class SortedByRainfall(string name, string description, string prompt) : Task(name, description, prompt)
{
    /// <summary>
    /// Основной конструктор задачи
    /// </summary>
    public SortedByRainfall() : this("SortedByRainfall",
        "Сортировка по количеству осадков",
        "Сохраняет данные о количестве осадков за каждый день в убывающем порядке по группам") { }

    /// <summary>
    /// Переопределенный метод выполнения задачи
    /// </summary>
    /// <param name="successfulExecution">Позволяет отслеживать успешность выполнения задачи</param>
    /// <param name="result">Возвращаемый задачей результат</param>
    public override void Execute(ref bool successfulExecution, ref string result)
    {
        string[]? data = FileParser.ReadLines();
        if (data is not null)
        {
            Dictionary<string, List<WeatherRec>> groups = new();

            try
            {
                // Парсинг данных
                var weatherRecs = DataParser.ParseData(data);

                Console.WriteLine("Формирую результат запроса...\n");

                // Группировка данных по локациям
                foreach (var weatherRec in weatherRecs)
                {
                    if (!groups.TryAdd(weatherRec.Location, new List<WeatherRec> { weatherRec }))
                    {
                        groups[weatherRec.Location].Add(weatherRec);
                    }
                }

                // Формирование результата
                foreach (var group in groups)
                {
                    result += $"Локация: {group.Key}\n"
                              + $"Среднее количество осадков: {GetAverageRainfall(group.Value)}\n";
                }

                string outputDir = $"{FileParser.ProjectDirectory}{Path.DirectorySeparatorChar}File{Path.DirectorySeparatorChar}Output{Path.DirectorySeparatorChar}average_rain_weatherAUS.csv";

                SortByRainfall(ref groups);

                Console.WriteLine($"Отсортированные данные загружены в {outputDir}");
                FileParser.WriteToFile(outputDir, ListsToList(groups), ref successfulExecution);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                successfulExecution = false;
            }
        }
        else
        {
            result = "Ошибка при чтении файла!";
        }

    }

    /// <summary>
    /// Метод для подсчета среднего количества осадков
    /// </summary>
    /// <param name="weatherRecs">Список данных для подсчета среднего</param>
    /// <returns>Среднее арифметическое из полученных данных</returns>
    private double GetAverageRainfall(List<WeatherRec> weatherRecs)
    {
        double sum = 0;
        foreach (var weatherRec in weatherRecs)
        {
            sum += weatherRec.Rainfall;
        }

        return sum / weatherRecs.Count;
    }

    /// <summary>
    /// Метод для сортировки данных по количеству осадков
    /// </summary>
    /// <param name="groups">Список данных для сортировки по убыванию</param>
    private void SortByRainfall(ref Dictionary<string, List<WeatherRec>> groups)
    {
        foreach (var group in groups)
        {
            group.Value.Sort((a, b) => b.Rainfall.CompareTo(a.Rainfall));
        }
    }

    /// <summary>
    /// Преобразование списка списков в список строк
    /// </summary>
    /// <param name="groups">Данные для преобразования</param>
    /// <returns>Список строк</returns>
    private List<string> ListsToList(Dictionary<string, List<WeatherRec>> groups)
    {
        List<string> result = new();
        foreach (var group in groups)
        {
            foreach (var weatherRec in group.Value)
            {
                result.Add(weatherRec.ToString());
            }
        }

        return result;
    }
}

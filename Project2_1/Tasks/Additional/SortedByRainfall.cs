using Project2_1.Module;

namespace Project2_1.Tasks.Additional;

public class SortedByRainfall(string name, string description, string prompt) : Task(name, description, prompt)
{
    public SortedByRainfall() : this("SortedByRainfall",
        "Сортировка по количеству осадков",
        "Сохраняет данные о количестве осадков за каждый день в убывающем порядке по группам") { }

    public override void Execute(ref bool successfulExecution, ref string result)
    {
        string[]? data = FileParser.ReadLines();
        if (data is not null)
        {
            Dictionary<string, List<WeatherRec>> groups = new();
            
            try
            {
                List<WeatherRec> weatherRecs = DataParser.ParseData(data);
                
                Console.WriteLine("Формирую результат запроса...\n");
                
                foreach (WeatherRec weatherRec in weatherRecs)
                {
                    if (!groups.TryAdd(weatherRec.Location, new List<WeatherRec> { weatherRec }))
                    {
                        groups[weatherRec.Location].Add(weatherRec);
                    }
                }

                foreach (KeyValuePair<string, List<WeatherRec>> group in groups)
                {
                    result += $"Локация: {group.Key}\n"
                              + $"Среднее количество осадков: {GetAverageRainfall(group.Value)}\n";
                }
                
                string outputDir = $"{FileParser.GetProjectDirectory()}{Path.DirectorySeparatorChar}File{Path.DirectorySeparatorChar}Output{Path.DirectorySeparatorChar}average_rain_weatherAUS.csv";
            
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

    private double GetAverageRainfall(List<WeatherRec> weatherRecs)
    {
        double sum = 0;
        foreach (WeatherRec weatherRec in weatherRecs)
        {
            sum += weatherRec.Rainfall;
        }

        return sum / weatherRecs.Count;
    }

    private void SortByRainfall(ref Dictionary<string, List<WeatherRec>> groups)
    {
        foreach (KeyValuePair<string, List<WeatherRec>> group in groups)
        {
            group.Value.Sort((a, b) => b.Rainfall.CompareTo(a.Rainfall));
        }
    }
    
    private List<string> ListsToList(Dictionary<string, List<WeatherRec>> groups)
    {
        List<string> result = new();
        foreach (KeyValuePair<string, List<WeatherRec>> group in groups)
        {
            foreach (WeatherRec weatherRec in group.Value)
            {
                result.Add(weatherRec.ToString());
            }
        }

        return result;
    }
}

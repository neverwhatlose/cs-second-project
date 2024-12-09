using Project2_1.Module;

namespace Project2_1.Tasks.Core;

public class WeatherInSydney(string name, string description, string prompt) : Task(name, description, prompt)
{
    public WeatherInSydney() : this("WeatherInSydney", 
        "Показать погоду в Сиднее", 
        "Информация о погоде, собранной в Сиднее (Location = Sydney) за 2009 и 2010 год.") { }

    public override void Execute(ref bool successfulExecution, ref string result)
    {
        Console.WriteLine("Получение данных о погоде в Сиднее...");
        
        string[]? data = FileParser.ReadLines();
        if (data is not null)
        {
            try
            {
                List<WeatherRec> weatherRecs = DataParser.ParseData(data);
                result = weatherRecs.Count.ToString();
                successfulExecution = true;
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
}
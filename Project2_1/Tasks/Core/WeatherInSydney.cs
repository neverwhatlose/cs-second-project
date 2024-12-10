using Project2_1.Module;

namespace Project2_1.Tasks.Core;

public class WeatherInSydney(string name, string description, string prompt) : Task(name, description, prompt)
{
    public WeatherInSydney() : this("WeatherInSydney", 
        "Показать погоду в Сиднее", 
        "Информация о погоде, собранной в Сиднее (Location = Sydney) за 2009 и 2010 год.") { }

    public override void Execute(ref bool successfulExecution, ref string result)
    {
        string[]? data = FileParser.ReadLines();
        if (data is not null)
        {
            try
            {
                List<WeatherRec> weatherRecs = DataParser.ParseData(data);
                
                Console.WriteLine("Получение данных о погоде в Сиднее...\n");
                
                List<string> weatherRecsStr = new();
                weatherRecsStr.Add("Date,Location,MinTemp,MaxTemp,Rainfall,Evaporation,Sunshine,WindGustDir,WindGustSpeed,WindDir9am,WindDir3pm,WindSpeed9am,WindSpeed3pm,Humidity9am,Humidity3pm,Pressure9am,Pressure3pm,Cloud9am,Cloud3pm,Temp9am,Temp3pm,RainToday,RainTomorrow");
                foreach (WeatherRec weatherRec in weatherRecs)
                {
                    if (weatherRec.Location == "Sydney" &&
                        (weatherRec.Date.Year == 2009 || weatherRec.Date.Year == 2010))
                    {
                        weatherRecsStr.Add(weatherRec.ToString());
                    }
                }
                
                string outputDir = $"{FileParser.GetProjectDirectory()}{Path.DirectorySeparatorChar}File{Path.DirectorySeparatorChar}Output{Path.DirectorySeparatorChar}Sydney_2009_2010_weatherAUS.csv";
                FileParser.WriteToFile(outputDir, weatherRecsStr, ref successfulExecution);
                
                result = $"Найдено {weatherRecsStr.Count - 1} записей о погоде в Сиднее за 2009 и 2010 годы. " +
                         $"Результат записан в файл {outputDir}";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
        }
        else
        {
            result = "Ошибка при чтении файла!";
        }
    }
}
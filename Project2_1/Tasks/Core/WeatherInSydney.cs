using Project2_1.Module;

namespace Project2_1.Tasks.Core;

/// <summary>
/// Класс задачи "Показать погоду в Сиднее"
/// </summary>
/// <param name="name">Техническое название задачи</param>
/// <param name="description">Название задачи понятное пользователю</param>
/// <param name="prompt">Описание задачи</param>
public class WeatherInSydney(string name, string description, string prompt) : Task(name, description, prompt)
{
    /// <summary>
    /// Основной конструктор задачи
    /// </summary>
    public WeatherInSydney() : this("WeatherInSydney",
        "Показать погоду в Сиднее",
        "Информация о погоде, собранной в Сиднее (Location = Sydney) за 2009 и 2010 год.") { }

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
            try
            {
                var weatherRecs = DataParser.ParseData(data);

                Console.WriteLine("Получение данных о погоде в Сиднее...\n");

                List<string> weatherRecsStr = new();
                weatherRecsStr.Add("Date,Location,MinTemp,MaxTemp,Rainfall,Evaporation,Sunshine,WindGustDir,WindGustSpeed,WindDir9am,WindDir3pm,WindSpeed9am,WindSpeed3pm,Humidity9am,Humidity3pm,Pressure9am,Pressure3pm,Cloud9am,Cloud3pm,Temp9am,Temp3pm,RainToday,RainTomorrow");
                foreach (var weatherRec in weatherRecs)
                {
                    if (weatherRec.Location == "Sydney" &&
                        (weatherRec.Date.Year == 2009 || weatherRec.Date.Year == 2010))
                    {
                        weatherRecsStr.Add(weatherRec.ToString());
                    }
                }

                string outputDir = $"{FileParser.ProjectDirectory}{Path.DirectorySeparatorChar}File{Path.DirectorySeparatorChar}Output{Path.DirectorySeparatorChar}Sydney_2009_2010_weatherAUS.csv";
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

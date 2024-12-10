using Project2_1.Module;

namespace Project2_1.Tasks.Additional;

public class ProlongedSunshine(string name, string description, string prompt) : Task(name, description, prompt)
{
    public ProlongedSunshine() : this("ProlongedSunshine", 
        "Показать дни с продолжительным солнечным светом", 
        "Нажмите Enter для продолжения") { }
    
    public override void Execute(ref bool successfulExecution, ref string result)
    {
        string[]? data = FileParser.ReadLines();
        
        if (data is not null)
        {
            try
            {
                List<WeatherRec> weatherRecs = DataParser.ParseData(data);
                
                Console.WriteLine("Подсчет дней с продолжительным солнечным светом...\n" +
                                  "Куда сохранить результат? (введите полный путь к файлу или нажмите Enter для сохранения в " +
                                  $"{FileParser.GetProjectDirectory()}{Path.DirectorySeparatorChar}File{Path.DirectorySeparatorChar}Output{Path.DirectorySeparatorChar}sunshine_days.csv)");
                
                WeatherRec longestSunshine = weatherRecs[0];
                List<string> sunshineDays = new();
                
                sunshineDays.Add("Date,Location,MinTemp,MaxTemp,Rainfall,Evaporation,Sunshine,WindGustDir,WindGustSpeed,WindDir9am,WindDir3pm,WindSpeed9am,WindSpeed3pm,Humidity9am,Humidity3pm,Pressure9am,Pressure3pm,Cloud9am,Cloud3pm,Temp9am,Temp3pm,RainToday,RainTomorrow");
                foreach (WeatherRec weatherRec in weatherRecs)
                {
                    if (weatherRec.Sunshine >= 4)
                    {
                        sunshineDays.Add(weatherRec.ToString());
                        if (weatherRec.Sunshine > longestSunshine.Sunshine)
                        {
                            longestSunshine = weatherRec;
                        }
                    }
                }

                result =
                    $"Самый длинный период солнечного света: {longestSunshine.MaxTemp} был {longestSunshine.Date.ToShortDateString()}.\n" +
                    $"Всего найдено {sunshineDays.Count} дней с продолжительным солнечным светом. ";
                
                string? outputDir = Console.ReadLine();
                try
                {
                    if (outputDir is null || outputDir == "")
                    {
                        outputDir = $"{FileParser.GetProjectDirectory()}{Path.DirectorySeparatorChar}File{Path.DirectorySeparatorChar}Output{Path.DirectorySeparatorChar}sunshine_days.csv";
                    }
                    result +=  $"Результат записан в файл {outputDir}";
                    
                    FileParser.WriteToFile(outputDir, sunshineDays, ref successfulExecution );
                }
                catch (Exception ex)
                {
                   result = ex.Message;
                } 

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
using Project2_1.Module;

namespace Project2_1.Tasks.Core;

/// <summary>
/// Класс задачи "Показать статистику по дням"
/// </summary>
/// <param name="name">Техническое название задачи</param>
/// <param name="description">Название задачи понятное пользователю</param>
/// <param name="prompt">Описание задачи</param>
public class ShowStatistics(string name, string description, string prompt) : Task(name, description, prompt)
{
    /// <summary>
    /// Основной конструктор задачи
    /// </summary>
    public ShowStatistics() : this("ShowStatistics",
        "Показать статистику по дням",
        "Сводная статистика по данным загруженного файла") { }

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

                Console.WriteLine("Формирование сводной статистики...");

                HashSet<DateTime> fishingDays = new(), rainyWarmDays = new(), normalAtmospherePressure = new(), winDirCount = new();
                var groups = new Dictionary<string, int>();

                // Собираем статистику по дням
                foreach (var weatherRec in weatherRecs)
                {
                    if (weatherRec.WindSpeed3Pm < 13)
                    {
                        fishingDays.Add(weatherRec.Date);
                    }

                    if (weatherRec.MaxTemp >= 20 && weatherRec.RainToday)
                    {
                        rainyWarmDays.Add(weatherRec.Date);
                    }

                    if (1000 <= weatherRec.Pressure9Am && weatherRec.Pressure9Am <= 1007)
                    {
                        normalAtmospherePressure.Add(weatherRec.Date);
                    }
                    if (weatherRec.WindDir9Am == WorldSides.W
                        || weatherRec.WindDir3Pm == WorldSides.WSW
                        || weatherRec.WindDir3Pm == WorldSides.SW
                        || weatherRec.WindDir3Pm == WorldSides.S
                        || weatherRec.WindDir3Pm == WorldSides.SSW)
                    {
                        winDirCount.Add(weatherRec.Date);
                    }
                    if (!groups.TryAdd(weatherRec.Location, 1))
                    {
                        groups[weatherRec.Location]++;
                    }
                }

                result = $"\nКоличество дней, когда скорость ветра в 3PM меньше 13: {fishingDays.Count}\n" +
                         $"Количество дней, когда максимальная температура больше 20 и шел дождь: {rainyWarmDays.Count}\n" +
                         $"Количество дней, когда атмосферное давление в 9AM было в пределах 1000-1007: {normalAtmospherePressure.Count}\n" +
                         $"Количество дней, когда ветер дул только на W, WSW, W, SSW, S: {winDirCount.Count}\n" +
                         "Локации и количество записей в каждой из них:";

                foreach (var group in groups)
                {
                    result += $"\n{group.Key}: {group.Value}";
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

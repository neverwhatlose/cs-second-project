namespace Project2_1.Module;

/// <summary>
/// Класс для парсинга данных
/// </summary>
public static class DataParser
{
    private const int ColumnCount = 23;

    private const string ColumnNames =
        "Date,Location,MinTemp,MaxTemp,Rainfall,Evaporation,Sunshine,WindGustDir,WindGustSpeed,WindDir9am,WindDir3pm,WindSpeed9am,WindSpeed3pm,Humidity9am,Humidity3pm,Pressure9am,Pressure3pm,Cloud9am,Cloud3pm,Temp9am,Temp3pm,RainToday,RainTomorrow";
    
    /// <summary>
    /// Парсит данные из массива строк
    /// </summary>
    /// <param name="lines">Входные данные из файла</param>
    /// <returns>Список объектов после парсинга</returns>
    /// <exception cref="Exception">Выбрасывается при неверном формате входных данных</exception>
    public static List<WeatherRec> ParseData(string[] lines)
    {
        if (lines.Length <= 1 || lines[0] != ColumnNames)
        {
            throw new Exception("Неверный формат данных");
        }
        
        // Удаляем первую строку с названиями столбцов
        lines = lines[1..];

        List<WeatherRec> weatherRecs = new();
        
        // Парсим каждую строку
        foreach (string line in lines)
        {
            if (IsLineValid(line, out WeatherRec weatherRec))
            {
                weatherRecs.Add(weatherRec);
            }
        }

        return weatherRecs;
    }
    
    /// <summary>
    /// Парсит строку к объекту WeatherRec
    /// </summary>
    /// <param name="line">Передаваемая строка</param>
    /// <param name="weatherRec">Полученный объект после парсинга. Может быть null (при неудачном парсинге) или иметь свойства</param>
    /// <returns>True - если удалось отпарсить строку, false в остальных случаях</returns>
    private static bool IsLineValid(string line, out WeatherRec weatherRec)
    {
        string[] values = line.Replace("No", "false").Replace("Yes", "true").Split(',');
        if (values.Length == ColumnCount)
        {
            if (DateTime.TryParse(values[0], out DateTime time)
                && values[2].TryParseWithExtension(out double minTemp)
                && values[3].TryParseWithExtension(out double maxTemp)
                && values[4].TryParseWithExtension(out double rainfall)
                && values[5].TryParseWithExtension(out double evaporation)
                && values[6].TryParseWithExtension(out double sunshine)
                && Enum.TryParse(values[7], out WorldSides windGustDir)
                && values[8].TryParseWithExtension(out int windGustSpeed)
                && Enum.TryParse(values[9], out WorldSides windDir9Am)
                && Enum.TryParse(values[10], out WorldSides windDir3Pm)
                && values[11].TryParseWithExtension(out int windSpeed9Am)
                && values[12].TryParseWithExtension(out int windSpeed3Pm)
                && values[13].TryParseWithExtension(out int humidity9Am)
                && values[14].TryParseWithExtension(out int humidity3Pm)
                && values[15].TryParseWithExtension(out double pressure9Am)
                && values[16].TryParseWithExtension(out double pressure3Pm)
                && values[17].TryParseWithExtension(out int cloud9Am)
                && values[18].TryParseWithExtension(out int cloud3Pm)
                && values[19].TryParseWithExtension(out double temp9Am)
                && values[20].TryParseWithExtension(out double temp3Pm)
                && values[21].TryParseWithExtension(out bool rainToday)
                && values[22].TryParseWithExtension(out bool rainTomorrow))
            {
                weatherRec = new WeatherRec(time, values[1], minTemp, maxTemp, rainfall, evaporation, sunshine,
                    windGustDir, windGustSpeed, windDir9Am, windDir3Pm, windSpeed9Am, windSpeed3Pm, humidity9Am,
                    humidity3Pm, pressure9Am, pressure3Pm, cloud9Am, cloud3Pm, temp9Am, temp3Pm, rainToday,
                    rainTomorrow);
                return true;
            }
        }

        weatherRec = new();
        return false;
    }
    
    /// <summary>
    /// Расширение для TryParse
    /// </summary>
    /// <param name="value">Значение для парсинга</param>
    /// <param name="result">Возвращаемое значение после парсинга</param>
    /// <returns>True - при удачном парсинге строки, false в остальных случаях</returns>
    private static bool TryParseWithExtension(this string value, out bool result)
    {
        if (!value.Equals("NA"))
        {
            return bool.TryParse(value, out result);
        }

        result = false;
        return true;
    }
    
    /// <summary>
    /// Расширение для TryParse
    /// </summary>
    /// <param name="value">Значение для парсинга</param>
    /// <param name="result">Возвращаемое значение после парсинга</param>
    /// <returns>True - при удачном парсинге строки, false в остальных случаях</returns>
    private static bool TryParseWithExtension(this string value, out double result)
    {
        if (!value.Equals("NA"))
        {
            return double.TryParse(value, out result);
        }

        result = 0;
        return true;
    }
    
    /// <summary>
    /// Расширение для TryParse
    /// </summary>
    /// <param name="value">Значение для парсинга</param>
    /// <param name="result">Возвращаемое значение после парсинга</param>
    /// <returns>True - при удачном парсинге строки, false в остальных случаях</returns>
    private static bool TryParseWithExtension(this string value, out int result)
    {
        if (!value.Equals("NA"))
        {
            return int.TryParse(value, out result);
        }

        result = 0;
        return true;
    }
}
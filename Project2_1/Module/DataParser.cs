using System.Runtime.Serialization;

namespace Project2_1.Module;

public static class DataParser
{
    private const int ColumnCount = 23;

    private const string ColumnNames =
        "Date,Location,MinTemp,MaxTemp,Rainfall,Evaporation,Sunshine,WindGustDir,WindGustSpeed,WindDir9am,WindDir3pm,WindSpeed9am,WindSpeed3pm,Humidity9am,Humidity3pm,Pressure9am,Pressure3pm,Cloud9am,Cloud3pm,Temp9am,Temp3pm,RainToday,RainTomorrow";

    public static List<WeatherRec> ParseData(string[] lines)
    {
        if (lines.Length <= 1 || lines[0] != ColumnNames)
        {
            throw new Exception("Неверный формат данных");
        }

        lines = lines[1..];

        List<WeatherRec> weatherRecs = new();

        foreach (string line in lines)
        {
            if (IsLineValid(line, out WeatherRec weatherRec))
            {
                weatherRecs.Add(weatherRec);
            }
        }

        return weatherRecs;
    }

    private static bool IsLineValid(string line, out WeatherRec weatherRec)
    {
        string[] values = line.Replace("No", "false").Replace("Yes", "true").Split(',');
        if (values.Length == ColumnCount)
        {
            if (!values.Contains("NA")
                && !values.Contains("\t")
                && DateTime.TryParse(values[0], out DateTime time)
                && double.TryParse(values[2], out double minTemp)
                && double.TryParse(values[3], out double maxTemp)
                && double.TryParse(values[4], out double rainfall)
                && double.TryParse(values[5], out double evaporation)
                && double.TryParse(values[6], out double sunshine)
                && Enum.TryParse(values[7], out WorldSides windGustDir)
                && int.TryParse(values[8], out int windGustSpeed)
                && Enum.TryParse(values[9], out WorldSides windDir9Am)
                && Enum.TryParse(values[10], out WorldSides windDir3Pm)
                && int.TryParse(values[11], out int windSpeed9Am)
                && int.TryParse(values[12], out int windSpeed3Pm)
                && int.TryParse(values[13], out int humidity9Am)
                && int.TryParse(values[14], out int humidity3Pm)
                && double.TryParse(values[15], out double pressure9Am)
                && double.TryParse(values[16], out double pressure3Pm)
                && int.TryParse(values[17], out int cloud9Am)
                && int.TryParse(values[18], out int cloud3Pm)
                && double.TryParse(values[19], out double temp9Am)
                && double.TryParse(values[20], out double temp3Pm)
                && bool.TryParse(values[21], out bool rainToday)
                && bool.TryParse(values[22], out bool rainTomorrow))
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
}
namespace Project2_1.Module;

/// <summary>
/// Класс, представляющий запись о погоде
/// </summary>
public class WeatherRec
{
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public double MinTemp { get; set; }
    public double MaxTemp { get; set; }
    public double Rainfall { get; set; }
    public double Evaporation { get; set; }
    public double Sunshine { get; set; }
    public double Pressure9Am { get; set; }
    public double Pressure3Pm { get; set; }
    public double Temp9Am { get; set; }
    public double Temp3Pm { get; set; }
    public int WindGustSpeed { get; set; }
    public int WindSpeed9Am { get; set; }
    public int WindSpeed3Pm { get; set; }
    public int Humidity9Am { get; set; }
    public int Humidity3Pm { get; set; }
    public int Cloud9Am { get; set; }
    public int Cloud3Pm { get; set; }
    public WorldSides WindGustDir { get; set; }
    public WorldSides WindDir9Am { get; set; }
    public WorldSides WindDir3Pm { get; set; }
    public bool RainToday { get; set; }
    public bool RainTomorrow { get; set; }

    /// <summary>
    /// Пустой конструктор для неудачного парсинга строки
    /// </summary>
    public WeatherRec() { }

    /// <summary>
    /// Основной конструктор
    /// </summary>
    /// <param name="date"></param>
    /// <param name="location"></param>
    /// <param name="minTemp"></param>
    /// <param name="maxTemp"></param>
    /// <param name="rainfall"></param>
    /// <param name="evaporation"></param>
    /// <param name="sunshine"></param>
    /// <param name="windGustDir"></param>
    /// <param name="windGustSpeed"></param>
    /// <param name="windDir9Am"></param>
    /// <param name="windDir3Pm"></param>
    /// <param name="windSpeed9Am"></param>
    /// <param name="windSpeed3Pm"></param>
    /// <param name="humidity9Am"></param>
    /// <param name="humidity3Pm"></param>
    /// <param name="pressure9Am"></param>
    /// <param name="pressure3Pm"></param>
    /// <param name="cloud9Am"></param>
    /// <param name="cloud3Pm"></param>
    /// <param name="temp9Am"></param>
    /// <param name="temp3Pm"></param>
    /// <param name="rainToday"></param>
    /// <param name="rainTomorrow"></param>
    public WeatherRec(DateTime date, string location, double minTemp, double maxTemp, double rainfall, double evaporation, double sunshine, WorldSides windGustDir, int windGustSpeed, WorldSides windDir9Am, WorldSides windDir3Pm, int windSpeed9Am, int windSpeed3Pm, int humidity9Am, int humidity3Pm, double pressure9Am, double pressure3Pm, int cloud9Am, int cloud3Pm, double temp9Am, double temp3Pm, bool rainToday, bool rainTomorrow)
    {
        Date = date;
        Location = location;
        MinTemp = minTemp;
        MaxTemp = maxTemp;
        Rainfall = rainfall;
        Evaporation = evaporation;
        Sunshine = sunshine;
        WindGustDir = windGustDir;
        WindGustSpeed = windGustSpeed;
        WindDir9Am = windDir9Am;
        WindDir3Pm = windDir3Pm;
        WindSpeed9Am = windSpeed9Am;
        WindSpeed3Pm = windSpeed3Pm;
        Humidity9Am = humidity9Am;
        Humidity3Pm = humidity3Pm;
        Pressure9Am = pressure9Am;
        Pressure3Pm = pressure3Pm;
        Cloud9Am = cloud9Am;
        Cloud3Pm = cloud3Pm;
        Temp9Am = temp9Am;
        Temp3Pm = temp3Pm;
        RainToday = rainToday;
        RainTomorrow = rainTomorrow;
    }

    // ))))))))))))))))))))))
    // use expression body)) toString))))
    /// <summary>
    /// Переопределение метода ToString
    /// </summary>
    /// <returns>Строковое представление объекта WeatherRec</returns>
    public override string ToString()
    {
        string result = $"{Date.ToShortDateString()},{Location},{MinTemp},{MaxTemp},{Rainfall},{Evaporation},{Sunshine},{WindGustDir},{WindGustSpeed},{WindDir9Am},{WindDir3Pm},{WindSpeed9Am},{WindSpeed3Pm},{Humidity9Am},{Humidity3Pm},{Pressure9Am},{Pressure3Pm},{Cloud9Am},{Cloud3Pm},{Temp9Am},{Temp3Pm},{RainToday},{RainTomorrow}".Replace("True", "Yes").Replace("False", "No");
        return result;
    }
}

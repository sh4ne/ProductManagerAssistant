using System.Linq;
using GroceryConsole.Weather.Weather;

namespace GroceryConsole
{
    public static class Algorithms
    {
        public static Weather.Weather.Current WeatherFromScore(int weatherScore)
        {
            Current toWeather = new Current();

            //var score = 0;
            var weath = weatherScore.ToString();
            if (weath.Length == 5)
            {
                toWeather.Weather = new Weather.Weather.Weather { Icon = "" };
                switch (weath.ElementAt(0))
                {
                    case '0':
                        toWeather.Weather.Icon = "13d";
                        break;
                    case '1':
                        toWeather.Weather.Icon = "09d";
                        break;
                    case '2':
                        toWeather.Weather.Icon = "03d";
                        break;
                    case '3':
                        toWeather.Weather.Icon = "00d";
                        break;
                    default:
                        break;
                }

                toWeather.Temperature = new Temperature { Value = "0" };
                switch (weath.ElementAt(1))
                {
                    case '0':
                        toWeather.Temperature.Value = "-1";
                        break;
                    case '1':
                        toWeather.Temperature.Value = "25";
                        break;
                    case '2':
                        toWeather.Temperature.Value = "55";
                        break;
                    case '3':
                        toWeather.Temperature.Value = "75";
                        break;
                    case '4':
                        toWeather.Temperature.Value = "95";
                        break;
                    case '5':
                        toWeather.Temperature.Value = "105";
                        break;
                    default:
                        break;
                }

                toWeather.Humidity = new Humidity { Value = "0" };
                switch (weath.ElementAt(2))
                {
                    case '0':
                        toWeather.Humidity.Value = "10";
                        break;
                    case '1':
                        toWeather.Humidity.Value = "30";
                        break;
                    case '2':
                        toWeather.Humidity.Value = "55";
                        break;
                    case '3':
                        toWeather.Humidity.Value = "80";
                        break;
                    case '4':
                        toWeather.Humidity.Value = "95";
                        break;
                    default:
                        break;
                }

                toWeather.Wind = new Wind { Speed = new Speed { Value = "0" } };
                switch (weath.ElementAt(3))
                {
                    case '0':
                        toWeather.Wind.Speed.Value = "3";
                        break;
                    case '1':
                        toWeather.Wind.Speed.Value = "10";
                        break;
                    case '2':
                        toWeather.Wind.Speed.Value = "20";
                        break;
                    default:
                        break;
                }

                toWeather.Pressure = new Pressure { Value = "0" };
                switch (weath.ElementAt(4))
                {
                    case '0':
                        toWeather.Pressure.Value = "100";
                        break;
                    case '1':
                        toWeather.Pressure.Value = "115";
                        break;
                    default:
                        break;
                }
            }
            return toWeather;
        }
    }
}
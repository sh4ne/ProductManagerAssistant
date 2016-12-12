using GroceryConsole.Weather.Weather;

namespace GroceryConsole.Weather
{
    public class Item_Weather
    {
        public string Product { get; set; }
        public Current Weather { get; set; }
        public string Date { get; set; }
        public string Projection { get; set; }
        public int WeatherScore { get; set; }

        public Item_Weather(string product, Current weather)
        {
            Product = product;
            Weather = weather;
            Projection = "false";
            Date = weather.Lastupdate.Value;
            WeatherScore = weather.GenerateScore();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Accord.Math.Comparers;
using GroceryConsole.Weather;
using GroceryConsole.Weather.Weather;
using GroceryConsole.WeatherCoder;
using GroceryConsole.WeatherScorer;

namespace GroceryConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool breaker = true;
            WeatherRetreiver retreive = new WeatherRetreiver();
            while (breaker)
            {
                Console.Write("Please enter a zip code for store location: ");
                var zip = Console.ReadLine();
                if (zip == "exit")
                {
                    breaker = false;
                    return;
                }
                var code = Convert.ToInt32(zip);

                var weather = retreive.GetCurrentWeatherFromZip(code);
                Console.WriteLine("Would you like view the xml? y or n");
                var view = Console.ReadLine();

                if(view == "y" || view == "Y")
                {
                    Console.WriteLine(weather);
                }

                var curr = weather.ParseWeatherXml<Current>();
                Console.WriteLine("\nWeather Code: " + curr.GenerateScore() + "\n");

                Console.WriteLine("Item to Score:");
                var item = "Washington Berry Juice";
                Console.WriteLine(item);

                var itemWeather = new Item_Weather(item, curr);

                var decTree = new Decision_Tree();

                var reader= new CSVReader();
                var training = reader.GetItemInfo(@"C: \Users\Shane\Desktop\training_data.csv", item);

                decTree.TrainAlgorithm(training);
             
                var answer = decTree.CheckItemProjection(itemWeather);

                Console.WriteLine("Will Sell?: " + answer + "\n");

            }
        }
    }
}

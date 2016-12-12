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
using GroceryConsole.Weathercoder;
using GroceryConsole.WeatherScorer;

namespace GroceryConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var manBool = false;
            while (!manBool)
            {
                Console.WriteLine("Generate Weather Code:\n");
                var code = coder.ManuallyGenerateWeatherCode();
                Console.WriteLine("Code: " + code);
                Console.WriteLine("Do you want to continue? y or n");
                var respon = Console.ReadLine();
                switch (respon)
                {
                    case "Y":
                    case "y":
                        continue;
                    case "N":
                    case "n":
                        manBool = true;
                        break;
                    default:
                        Console.WriteLine("Not a valid input. Going to beginning.");
                        continue;
                }
            }

            bool breaker = true;
            WeatherRetreiver retreive = new WeatherRetreiver();
            while (breaker)
            {
                Console.Write("Please enter a zip code for weather search: ");
                var zip = Console.ReadLine();
                if (zip == "exit")
                {
                    breaker = false;
                    return;
                }
                int code = Convert.ToInt32(zip);

                var weather = retreive.GetCurrentWeatherFromZip(code);
                Console.WriteLine(weather);

                var curr = weather.ParseWeatherXml<Current>();
                Console.WriteLine("Score: " + curr.GenerateScore());

                Console.WriteLine("Item to Score:");
                var item = "Washington Berry Juice";
                Console.WriteLine(item);

                var item_weather = new Item_Weather(item, curr);

                var dec_tree = new Decision_Tree();

                List<Item_Weather> training = new List<Item_Weather>();
                CSVReader reader= new CSVReader();
                training = reader.GetItemInfo(@"C: \Users\Shane\Desktop\training_data.csv", item);

                SVM_Algorithm svm = new SVM_Algorithm();

                dec_tree.TrainAlgorithm(training);

                //svm.TrainAlgorithm(training);
                
                var answer = dec_tree.CheckItemProjection(item_weather);

                //var svmAnswer = svm.CheckItemProjection(item_weather);

                //Console.WriteLine("SVM: " + svmAnswer);
                Console.WriteLine("Desicion Tree: " + answer);

            }
        }
    }
}

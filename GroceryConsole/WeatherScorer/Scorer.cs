using System;
using System.Data;
using Accord.MachineLearning.DecisionTrees;
using GroceryConsole.Weather.Weather;

namespace GroceryConsole.Weathercoder
{
    public static class Coder
    {
        public static int ManuallyGenerateWeatherCode()
        {
            var code = 0;
            var weatherBool = false;
            while (!weatherBool)
            {
                Console.WriteLine("Choose the most accurate: Sun, Overcast, Rain, Snow ");
                var response = Console.ReadLine();
                switch (response)
                {
                    case "Sun":
                    case "sun":
                        code += 10000;
                        weatherBool = true;
                        break;
                    case "Overcast":
                    case "overcast":
                        code += 00000;
                        weatherBool = true;
                        break;
                    case "rain":
                    case "Rain":
                        code += 20000;
                        weatherBool = true;
                        break;
                    case "snow":
                    case "Snow":
                        code += 30000;
                        weatherBool = true;
                        break;
                    default:
                        Console.WriteLine("Not a valid option, please try again.");
                        break;
                }

            }

            Console.WriteLine("What is the tempeture?");
            var temp = Convert.ToInt32(Console.ReadLine());

            if (temp > 100)
            {
                code += 5000;
            }
            else if (temp < 100 && temp >= 90)
            {
                code += 4000;
            }
            else if (temp < 90 && temp >= 70)
            {
                code += 3000;
            }
            else if (temp < 70 && temp >= 40)
            {
                code += 2000;
            }
            else if (temp < 40 && temp >= 0)
            {
                code += 1000;
            }
            else
            {
                code += 0000;
            }

            Console.WriteLine("What is the humidity?");

            var humid = Convert.ToInt64(Console.ReadLine());

            if (humid < 100 && humid >= 90)
            {
                code += 400;
            }
            else if (humid < 90 && humid >= 70)
            {
                code += 300;
            }
            else if (humid < 70 && humid >= 40)
            {
                code += 200;
            }
            else if (humid < 40 && humid >= 20)
            {
                code += 100;
            }
            else
            {
                code += 000;
            }

            Console.WriteLine("How fast is the wind?");
            var wind = Convert.ToDouble(Console.ReadLine());

            if (wind >= 15)
            {
                code += 20;
            }
            else if (wind < 15 && wind >= 5)
            {
                code += 10;
            }
            else
            {
                code += 00;
            }

            Console.WriteLine("Aptmospheric pressure?");
            var press = Convert.ToDouble(Console.ReadLine());

            if (press >= 110)
            {
                code += 1;
            }
            else
            {
                code += 0;
            }

            return code;
       
        }
    }
}
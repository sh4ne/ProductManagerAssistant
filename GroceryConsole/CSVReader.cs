using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GroceryConsole.Weather;
using GroceryConsole.Weather.Weather;

namespace GroceryConsole
{
    public class CSVReader
    {
        public List<Item_Weather> GetItemInfo(string inputFile, string itemName)
        {
            string[] allLines = File.ReadAllLines(inputFile);

            var query = from line in allLines
                        let data = line.Split(',')
                        select new
                        {
                            Product = data[0],
                            Date = data[1],
                            Sales = Convert.ToDouble(data[2]),
                            WeatherScore = Convert.ToInt32(data[3]),
                            Projection = data[4]
                        };
            List<Item_Weather> forReturn = new List<Item_Weather>();
            foreach (var item in query)
            {
                if (item.Product == itemName)
                {
                    var weatherFromScore = Algorithms.WeatherFromScore(item.WeatherScore);
                    weatherFromScore.Lastupdate = new Lastupdate {Value = item.Date};
                    Item_Weather toAdd = new Item_Weather(itemName, weatherFromScore);
                    toAdd.Projection = item.Projection;
                    forReturn.Add(toAdd);
                }
            }
            return forReturn;
        }
    }
}
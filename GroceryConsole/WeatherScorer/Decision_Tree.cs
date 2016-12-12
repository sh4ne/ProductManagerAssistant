using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Math;
using Accord.Statistics.Filters;
using GroceryConsole.Weather;
using GroceryConsole.Weather.Weather;

namespace GroceryConsole.WeatherScorer
{
    public class Decision_Tree
    {
        public DecisionTree Tree { get; set; }
        public List<DecisionVariable> Variables { get; set; }
        public DataTable Data { get; }
        public Codification Codebook { get; set; }

        public Decision_Tree()
        {
            Data = new DataTable("Item Projections");
            Data.Columns.Add("Weather", typeof(string));
            Data.Columns.Add("Temperature", typeof(string));
            Data.Columns.Add("Humidity", typeof(string));
            Data.Columns.Add("Wind", typeof(string));
            Data.Columns.Add("Pressure", typeof(string));
            Data.Columns.Add("Projection", typeof(string));

            Variables = new List<DecisionVariable>
            {
                new DecisionVariable("Weather", 3), // 3 possible values (Sunny, overcast, rain, snow, )
                new DecisionVariable("Temperature", 5),//possible values (Extreme Hot, Hot, cool, freezing, Below 0)  
                new DecisionVariable("Humidity", 4), // 2 possible values (90-100, 60-80, 30-50, <30)    
                new DecisionVariable("Wind", 3), // 3 possible values (Weak, strong, none) 
                new DecisionVariable("Pressure", 2) //2 possible valuse (High, normal)
            };

            Tree = new DecisionTree(Variables, 2);
        }

        public void TrainAlgorithm(List<Item_Weather> itemWeather)
        {
            foreach (var item in itemWeather)
            {
                object[] info =
                {
                    item.Weather.Weather.Icon,
                    item.Weather.Temperature.Value,
                    item.Weather.Humidity.Value,
                    item.Weather.Wind.Speed.Value,
                    item.Weather.Pressure.Value,
                    item.Projection
                };
                Data.Rows.Add(info);
            }

            Codebook = new Codification(Data);
            
            DataTable symbols = Codebook.Apply(Data, "Weather", "Temperature", "Humidity", "Wind", "Pressure","Projection");
            int[][] inputs = symbols.ToArray<int>("Weather", "Temperature", "Humidity", "Wind", "Pressure");
            int[] outputs = symbols.ToArray<int>("Projection");

            ID3Learning learning = new ID3Learning(Tree);
            learning.Learn(inputs, outputs);
        }

        public string CheckItemProjection(Item_Weather itemToCheck)
        {
            var score = itemToCheck.WeatherScore;
            var newWeather = Algorithms.WeatherFromScore(score);
            string answer = Codebook.Translate("Projection",
                            Tree.Decide(Codebook.Translate(newWeather.Weather.Icon,
                            newWeather.Temperature.Value, newWeather.Humidity.Value,
                            newWeather.Wind.Speed.Value, newWeather.Pressure.Value)));

            return answer;
        }
    }
}
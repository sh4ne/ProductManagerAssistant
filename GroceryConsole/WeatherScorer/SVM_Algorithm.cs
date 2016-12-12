using System;
using System.Collections.Generic;
using System.Data;
using Accord;
using Accord.Collections;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Filters;
using Accord.Statistics.Kernels;
using GroceryConsole.Weather;

namespace GroceryConsole.WeatherScorer
{
    public class SVM_Algorithm
    {
        public void TrainAlgorithm(List<Item_Weather> itemWeather)
        {
            string[] inputColumns =
            {
                "Weather", "Temperature", "Humidity",
                "Wind", "Presure"
            };

            string outputColumn = "Projection";

            DataTable table = new DataTable("Item Projection");
            table.Columns.Add(inputColumns);
            table.Columns.Add(outputColumn);

            foreach (var item in itemWeather)
                table.Rows.Add(item.Weather.Weather.Icon, item.Weather.Temperature.Value, item.Weather.Humidity.Value, 
                                item.Weather.Wind.Speed.Value, item.Weather.Pressure.Value);


            Codification codebook = new Codification(table, inputColumns);

            DataTable symbols = codebook.Apply(table);

            double[][] inputs = symbols.ToArray(inputColumns);
            int[] outputs = symbols.ToArray<int>(outputColumn);


            var teacher = new MulticlassSupportVectorLearning<Gaussian>()
            {
                Learner = (param) => new SequentialMinimalOptimization<Gaussian>()
                {
                    UseKernelEstimation = true
                }
            };
            
            var machine = teacher.Learn(inputs, outputs);

            var calibration = new MulticlassSupportVectorLearning<Gaussian>
            {
                Model = machine,
                Learner = (p) => new ProbabilisticOutputCalibration<Gaussian>()
                {
                    Model = p.Model
                }
            };

            // Configure parallel execution options
            calibration.ParallelOptions.MaxDegreeOfParallelism = 1;

            // Learn a machine
            calibration.Learn(inputs, outputs);

            // Obtain class predictions for each sample
            int[] predicted = machine.Decide(inputs);

            double[] scores = machine.Score(inputs);

            // Get log-likelihoods (should be same as scores)
            double[][] logl = machine.LogLikelihoods(inputs);

            // Get probability for each sample
            double[][] prob = machine.Probabilities(inputs);

            // Compute classification error
            double error = new ZeroOneLoss(outputs).Loss(predicted);
            double loss = new CategoryCrossEntropyLoss(outputs).Loss(prob);

        }

        public string CheckItemProjection(Item_Weather itemToCheck)
        {
            // weather to tree format
            var weather = itemToCheck.WeatherScore.ToString();

            //string answer = Codebook.Translate("Projection",
            //                Tree<>.Decide(Codebook.Translate(itemToCheck.Weather.Weather.Icon,
            //                itemToCheck.Weather.Temperature.Value, itemToCheck.Weather.Humidity.Value,
            //                itemToCheck.Weather.Wind.Speed.Value, itemToCheck.Weather.Pressure.Value)));

            return "";
        }
    }
}
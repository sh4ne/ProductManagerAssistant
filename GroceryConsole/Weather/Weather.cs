using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace GroceryConsole.Weather
{
    namespace Weather
    {
        [XmlRoot(ElementName = "coord")]
        public class Coord
        {
            [XmlAttribute(AttributeName = "lon")]
            public string Lon { get; set; }
            [XmlAttribute(AttributeName = "lat")]
            public string Lat { get; set; }
        }

        [XmlRoot(ElementName = "sun")]
        public class Sun
        {
            [XmlAttribute(AttributeName = "rise")]
            public string Rise { get; set; }
            [XmlAttribute(AttributeName = "set")]
            public string Set { get; set; }
        }

        [XmlRoot(ElementName = "city")]
        public class City
        {
            [XmlElement(ElementName = "coord")]
            public Coord Coord { get; set; }
            [XmlElement(ElementName = "country")]
            public string Country { get; set; }
            [XmlElement(ElementName = "sun")]
            public Sun Sun { get; set; }
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
        }

        [XmlRoot(ElementName = "temperature")]
        public class Temperature
        {
            [XmlAttribute(AttributeName = "value")]
            public string Value { get; set; }
            [XmlAttribute(AttributeName = "min")]
            public string Min { get; set; }
            [XmlAttribute(AttributeName = "max")]
            public string Max { get; set; }
            [XmlAttribute(AttributeName = "unit")]
            public string Unit { get; set; }
        }

        [XmlRoot(ElementName = "humidity")]
        public class Humidity
        {
            [XmlAttribute(AttributeName = "value")]
            public string Value { get; set; }
            [XmlAttribute(AttributeName = "unit")]
            public string Unit { get; set; }
        }

        [XmlRoot(ElementName = "pressure")]
        public class Pressure
        {
            [XmlAttribute(AttributeName = "value")]
            public string Value { get; set; }
            [XmlAttribute(AttributeName = "unit")]
            public string Unit { get; set; }
        }

        [XmlRoot(ElementName = "speed")]
        public class Speed
        {
            [XmlAttribute(AttributeName = "value")]
            public string Value { get; set; }
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
        }

        [XmlRoot(ElementName = "direction")]
        public class Direction
        {
            [XmlAttribute(AttributeName = "value")]
            public string Value { get; set; }
            [XmlAttribute(AttributeName = "code")]
            public string Code { get; set; }
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
        }

        [XmlRoot(ElementName = "wind")]
        public class Wind
        {
            [XmlElement(ElementName = "speed")]
            public Speed Speed { get; set; }
            [XmlElement(ElementName = "gusts")]
            public string Gusts { get; set; }
            [XmlElement(ElementName = "direction")]
            public Direction Direction { get; set; }
        }

        [XmlRoot(ElementName = "clouds")]
        public class Clouds
        {
            [XmlAttribute(AttributeName = "value")]
            public string Value { get; set; }
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
        }

        [XmlRoot(ElementName = "visibility")]
        public class Visibility
        {
            [XmlAttribute(AttributeName = "value")]
            public string Value { get; set; }
        }

        [XmlRoot(ElementName = "precipitation")]
        public class Precipitation
        {
            [XmlAttribute(AttributeName = "mode")]
            public string Mode { get; set; }
        }

        [XmlRoot(ElementName = "weather")]
        public class Weather
        {
            [XmlAttribute(AttributeName = "number")]
            public string Number { get; set; }
            [XmlAttribute(AttributeName = "value")]
            public string Value { get; set; }
            [XmlAttribute(AttributeName = "icon")]
            public string Icon { get; set; }
        }

        [XmlRoot(ElementName = "lastupdate")]
        public class Lastupdate
        {
            [XmlAttribute(AttributeName = "value")]
            public string Value { get; set; }
        }

        [XmlRoot(ElementName = "current")]
        public class Current
        {
            [XmlElement(ElementName = "city")]
            public City City { get; set; }
            [XmlElement(ElementName = "temperature")]
            public Temperature Temperature { get; set; }
            [XmlElement(ElementName = "humidity")]
            public Humidity Humidity { get; set; }
            [XmlElement(ElementName = "pressure")]
            public Pressure Pressure { get; set; }
            [XmlElement(ElementName = "wind")]
            public Wind Wind { get; set; }
            [XmlElement(ElementName = "clouds")]
            public Clouds Clouds { get; set; }
            [XmlElement(ElementName = "visibility")]
            public Visibility Visibility { get; set; }
            [XmlElement(ElementName = "precipitation")]
            public Precipitation Precipitation { get; set; }
            [XmlElement(ElementName = "weather")]
            public Weather Weather { get; set; }
            [XmlElement(ElementName = "lastupdate")]
            public Lastupdate Lastupdate { get; set; }
            public int Score { get; set; }
            public int GenerateScore()
            {
                //5 digit number 4,5,4,3,2
                //new DecisionVariable("Weather", 4), // 4 possible values (Sunny, overcast, rain, snow, )
                //new DecisionVariable("Temperature", 5),// 5 possible values (Extreme Hot, Hot, cool, freezing, Below 0)  
                //new DecisionVariable("Humidity", 4), // 4 possible values (90-100, 60-80, 30-50, <30)    
                //new DecisionVariable("Wind", 3), // 3 possible values (Weak, strong, none) 
                //new DecisionVariable("Pressure", 2) //2 possible valuse (High, normal)

                var score = 0;

                switch (Weather.Icon)
                {
                    case "10d":
                    case "09d":
                        score += 10000;
                        break;
                    case "13d":
                        score += 00000;
                        break;
                    case "04d":
                    case "03d":
                        score += 20000;
                        break;
                    default:
                        score += 30000;
                        break;
                }

                var temp = Convert.ToDouble(Temperature.Value);

                if (temp > 100)
                {
                    score += 5000;
                }
                else if (temp < 100 && temp >= 90)
                {
                    score += 4000;
                }
                else if (temp < 90 && temp >= 70)
                {
                    score += 3000;
                }
                else if (temp < 70 && temp >= 40)
                {
                    score += 2000;
                }
                else if (temp < 40 && temp >= 0)
                {
                    score += 1000;
                }
                else
                {
                    score += 0000;
                }

                var humid = Convert.ToInt64(Humidity.Value);

                if (humid < 100 && humid >= 90)
                {
                    score += 400;
                }
                else if (humid < 90 && humid >= 70)
                {
                    score += 300;
                }
                else if (humid < 70 && humid >= 40)
                {
                    score += 200;
                }
                else if (humid < 40 && humid >= 20)
                {
                    score += 100;
                }
                else
                {
                    score += 000;
                }

                var wind = Convert.ToDouble(Wind.Speed.Value);

                if (wind >= 15)
                {
                    score += 20;
                }
                else if (wind < 15 && wind >= 5)
                {
                    score += 10;
                }
                else
                {
                    score += 00;
                }

                var press = Convert.ToDouble(Pressure.Value);

                if (press >= 112)
                {
                    score += 1;
                }
                else
                {
                    score += 0;
                }
                Score = score;
                return score;
            }
        }
    }

}
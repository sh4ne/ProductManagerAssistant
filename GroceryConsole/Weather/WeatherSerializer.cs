using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GroceryConsole.Weather
{
    public static class WeatherSerializer
    {
        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public static T ParseWeatherXml<T>(this string @this) where T : class
        {
            using (Stream s = GenerateStreamFromString(@this.Trim()))
            {
                var reader = XmlReader.Create(s, new XmlReaderSettings {ConformanceLevel = ConformanceLevel.Document});
                return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
            }
        }
    }
}
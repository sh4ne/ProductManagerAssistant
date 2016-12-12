using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Xml;

namespace GroceryConsole.Weather {
public
  class WeatherRetreiver {
  private const string _apiKey =     "f93ed3db2f7416df8d0ff5aa9ca8f1b9";
  private const string CurrentUrl =  "http://api.openweathermap.org/data/2.5/weather?" +
                                     "q=@LOC@&mode=xml&units=imperial&APPID=" + _apiKey;
  private const string ForecastUrl = "http://api.openweathermap.org/data/2.5/forecast?" +
                                     "q=@LOC@&mode=xml&units=imperial&APPID=" + _apiKey;
  private int ZipCode {get; set;}

    public string GetCurrentWeatherFromZip(int zipCode) {
      ZipCode = zipCode;
        var url = CurrentUrl.Replace("@LOC@", zipCode.ToString());
            var currWeatherXml = GetFormattedXml(url);
      return currWeatherXml;
    }
        private string GetFormattedXml(string url)
        {
            // Create a web client.
            using (var client = new WebClient())
            {
                // Get the response string from the URL.
                var xml = client.DownloadString(url);

                // Load the response into an XML document.
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xml);

                // Format the XML.
                using (var stringWriter = new StringWriter())
                {
                    var xmlTextWriter = new XmlTextWriter(stringWriter) {Formatting = Formatting.Indented};
                    xmlDocument.WriteTo(xmlTextWriter);
                    // Return the result.
                    return stringWriter.ToString();
                }
            }
        }
    }
}
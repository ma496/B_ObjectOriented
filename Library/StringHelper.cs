using Newtonsoft.Json;
using System.Xml;

namespace Library;

public static class StringHelper
{
    public static bool IsJson(string input)
    {
        try
        {
            JsonConvert.DeserializeObject(input);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    public static bool IsXml(string input)
    {
        try
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(input);
            return true;
        }
        catch (XmlException)
        {
            return false;
        }
    }
}

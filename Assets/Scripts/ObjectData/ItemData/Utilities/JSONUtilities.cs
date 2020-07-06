using Newtonsoft.Json.Linq;
using System.IO;

namespace ObjectData.ItemData.Utilities
{
    public static class JSONUtilities
    {
        public static JObject LoadJSON(string filePath)
        {
            return JObject.Parse(File.ReadAllText(filePath));
        }
    }
}
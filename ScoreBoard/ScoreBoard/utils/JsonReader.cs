using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScoreBoard.utils
{
    public static class JsonReader
    {
        public static Dictionary<string, string> ReadJsonStringValue(string jsonPath)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            try
            {
                using (StreamReader reader = new StreamReader(jsonPath))
                {
                    string json = reader.ReadToEnd();
                    var deserializedMap = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                    if (deserializedMap != null)
                    {
                        map = deserializedMap;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
            }
            return map;
        }
    }
}

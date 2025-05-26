using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ScoreBoard.utils
{
    public static class DataReader
    {
        /*
         * JsonReader.ReadCorpsData(jsonPath)
         * - jsonPath: JSON 파일 경로
         * - return: JSON 파일의 Key-Value 쌍을 Dictionary<string, string> 형태로 반환
         */
        public static Dictionary<string, string> ReadCorpsData(string jsonPath)
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

        /*
         * JsonReader.ReadCorpsMembersData(corpsId)
         * - corpsId: 군단 ID
         * - return: 해당 군단의 병사 id, name을 Dictionary<string, string> 형태로 반환
         */
        public static Dictionary<string, string> ReadCorpsMembersData(string corpsId)
        {
            var membersMap = new Dictionary<string, string>();
            try
            {
                string[] files = GetCharacterFilesByCorpsId(corpsId);

                if (files.Length == 0)
                {
                    MessageBox.Show("해당 군단의 병사 정보가 없습니다.");
                    return membersMap;
                }

                foreach (var file in files)
                {
                    var entry = ExtractIdAndName(file);
                    if (entry != null)
                    {
                        membersMap[entry.Value.id] = entry.Value.name;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"병사 정보를 읽는 중 오류 발생: {ex.Message}");
            }

            return membersMap;
        }

        /*
         * JsonReader.GetCharacterFilesByCorpsId(corpsId)
         * - corpsId: 군단 ID
         * - return: 해당 군단의 병사 JSON 파일 경로 배열
         */
        private static string[] GetCharacterFilesByCorpsId(string corpsId)
        {
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "meta_data", "character");
            string pattern = $"{corpsId}_*.json";
            return Directory.GetFiles(directoryPath, pattern);
        }

        /*
         * JsonReader.ExtractIdAndName(filePath)
         * - filePath: JSON 파일 경로
         * - return: JSON 파일에서 id와 name을 추출하여 (id, name) 형태로 반환
         */
        private static (string id, string name)? ExtractIdAndName(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                var data = JsonSerializer.Deserialize<JsonObject>(json);

                if (data != null && data.ContainsKey("id") && data.ContainsKey("name"))
                {
                    string? id = data["id"]?.ToString();
                    string? name = data["name"]?.ToString();

                    if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(name))
                    {
                        return (id, name);
                    }
                }

                MessageBox.Show($"파일 형식이 잘못되었습니다: {Path.GetFileName(filePath)}. 'id'와 'name' 키가 필요합니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"파일을 읽는 중 오류 발생: {ex.Message}");
            }

            return null;
        }
    }
}

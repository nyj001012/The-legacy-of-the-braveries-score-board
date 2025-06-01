using ScoreBoard.data;
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
                MessageBox.Show(jsonPath + " 파일을 읽는 중 오류 발생: " + ex.Message);
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

                if (files.Length > 0)
                {
                    foreach (var file in files)
                    {
                        var entry = ExtractIdAndName(file);
                        if (entry != null)
                        {
                            membersMap[entry.Value.id] = entry.Value.name;
                        }
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
        public static string[] GetCharacterFilesByCorpsId(string corpsId)
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
        public static (string id, string name)? ExtractIdAndName(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                JsonNode? node = JsonNode.Parse(json);

                if (node is JsonObject obj &&
                    obj.TryGetPropertyValue("id", out var idNode) &&
                    obj.TryGetPropertyValue("name", out var nameNode))
                {
                    string? id = idNode?.ToString();
                    string? name = nameNode?.ToString();

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

        /*
         * JsonReader.ReadMemberData(id)
         * - id: 병사 ID
         * - return: 해당 병사의 JSON 데이터를 반환
         */
        internal static CorpsMember? ReadMemberData (string id)
        {
            string jsonPath = @$"Resources/meta_data/character/{id}.json";
            if (!File.Exists(jsonPath))
            {
                return null;
            }
            string json = File.ReadAllText(jsonPath);
            return JsonSerializer.Deserialize<CorpsMember>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // 대소문자 구분 없이 속성 이름을 매칭
            });
        }
    }
}

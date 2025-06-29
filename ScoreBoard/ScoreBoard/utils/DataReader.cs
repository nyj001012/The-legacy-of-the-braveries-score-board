using ScoreBoard.data.artifact;
using ScoreBoard.data.character;
using ScoreBoard.data.monster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScoreBoard.utils
{
    public static class DataReader
    {
        // 캐싱된 JsonSerializerOptions 인스턴스
        private static readonly JsonSerializerOptions CachedJsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // 대소문자 구분 없이 속성 이름을 매칭
        };

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
                string[] files = GetDataFilesById(corpsId, "character");

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
         * JsonReader.GetDataFilesById(id)
         * - id: ID
         * - dataDirectory: 메타 데이터가 저장된 디렉토리 이름
         * - return: JSON 파일 경로 배열
         */
        public static string[] GetDataFilesById(string id, string dataDirectory)
        {
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "meta_data", dataDirectory);
            string pattern = $"{id}_*.json";
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
        internal static CorpsMember? ReadMemberData(string id)
        {
            string jsonPath = @$"Resources/meta_data/character/{id}.json";
            if (!File.Exists(jsonPath))
            {
                return null;
            }
            string json = File.ReadAllText(jsonPath);
            return JsonSerializer.Deserialize<CorpsMember>(json, CachedJsonSerializerOptions);
        }

        /*
         * ReadMonsterGrade()
         * - return: 몬스터 등급 정보 반환
         */
        public static Dictionary<string, MonsterGrade>? ReadMonsterGrade()
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "meta_data", "monster_grade.json");
            if (!File.Exists(jsonPath))
            {
                return null;
            }
            string json = File.ReadAllText(jsonPath);
            return JsonSerializer.Deserialize<Dictionary<string, MonsterGrade>>(json, CachedJsonSerializerOptions);
        }

        /*
         * ReadMonsterDataByGradeId(ushort id)
         * - id: 등급별 Id
         * - return: 등급별 몬스터 JSON 파일의 경로 배열
         */
        public static Dictionary<string, string> ReadMonsterDataByGradeId(ushort id)
        {
            var membersMap = new Dictionary<string, string>();
            try
            {
                string[] files = GetDataFilesById(id.ToString(), "monster");

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
                MessageBox.Show($"몬스터 정보를 읽는 중 오류 발생: {ex.Message}");
            }

            return membersMap;
        }

        /*
         * JsonReader.ReadMonsterData(id)
         * - id: 몬스터 ID
         * - return: 해당 몬스터의 JSON 데이터를 반환
         */
        internal static Monster? ReadMonsterData(string id)
        {
            string jsonPath = $@"Resources/meta_data/monster/{id}.json";
            if (!File.Exists(jsonPath))
            {
                return null;
            }
            string json = File.ReadAllText(jsonPath);
            return JsonSerializer.Deserialize<Monster>(json, CachedJsonSerializerOptions);
        }

        /*
         * JsonReader.ReadArtifactData(id)
         * - id: 유물 ID
         * - return: 해당 유물의 JSON 데이터를 반환
         */
        public static Artifact? ReadArtifactData(string id)
        {
            string jsonPath = $@"Resources/meta_data/artifact/{id}.json";
            if (!File.Exists(jsonPath))
            {
                return null;
            }
            string json = File.ReadAllText(jsonPath);
            CachedJsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            return JsonSerializer.Deserialize<Artifact>(json, CachedJsonSerializerOptions);
        }
    }
}

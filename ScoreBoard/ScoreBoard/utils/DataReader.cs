using ScoreBoard.data.artifact;
using ScoreBoard.data.character;
using ScoreBoard.data.monster;
using ScoreBoard.data.statusEffect;
using ScoreBoard.data.weather;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            PropertyNameCaseInsensitive = true, // 대소문자 구분 없이 속성 이름을 매칭
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        /*
         * ReadCorpsData(jsonPath)
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
         * ReadCorpsMembersData(corpsId)
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
         * GetDataFilesById(id)
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
         * ExtractIdAndName(filePath)
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
         * ReadMemberData(id)
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
         * ReadMonsterData(id)
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
         * ReadArtifactData(id)
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
            // JSON 파일을 읽어오기
            string json = File.ReadAllText(jsonPath);
            try
            {
                // id 형식: type_index_className (예: 0_01_Bandage)
                string[] parts = id.Split('_');
                if (parts.Length < 3)
                    throw new FormatException($"유효하지 않은 ID 형식: {id}");

                string className = parts[2];

                return className switch
                {
                    // 방어구
                    "VinylClothes" => JsonSerializer.Deserialize<VinylClothes>(json, CachedJsonSerializerOptions),
                    "PetitShieldClothes" => JsonSerializer.Deserialize<PetitShieldClothes>(json, CachedJsonSerializerOptions),
                    "Shirt" => JsonSerializer.Deserialize<Shirt>(json, CachedJsonSerializerOptions),
                    "Underwear" => JsonSerializer.Deserialize<Underwear>(json, CachedJsonSerializerOptions),
                    "BulletproofVest" => JsonSerializer.Deserialize<BulletproofVest>(json, CachedJsonSerializerOptions),
                    "Robe" => JsonSerializer.Deserialize<Robe>(json, CachedJsonSerializerOptions),
                    "WizardCloak" => JsonSerializer.Deserialize<WizardCloak>(json, CachedJsonSerializerOptions),
                    "Training" => JsonSerializer.Deserialize<Training>(json, CachedJsonSerializerOptions),
                    "Swimsuit" => JsonSerializer.Deserialize<Swimsuit>(json, CachedJsonSerializerOptions),
                    "Suit" => JsonSerializer.Deserialize<Suit>(json, CachedJsonSerializerOptions),
                    "BbangBbangSuit" => JsonSerializer.Deserialize<BbangBbangSuit>(json, CachedJsonSerializerOptions),
                    "HighPriest" => JsonSerializer.Deserialize<HighPriest>(json, CachedJsonSerializerOptions),
                    "Hood" => JsonSerializer.Deserialize<Hood>(json, CachedJsonSerializerOptions),
                    "General" => JsonSerializer.Deserialize<General>(json, CachedJsonSerializerOptions),
                    "Titan" => JsonSerializer.Deserialize<Titan>(json, CachedJsonSerializerOptions),
                    "Sage" => JsonSerializer.Deserialize<Sage>(json, CachedJsonSerializerOptions),
                    "LuxuryJacket" => JsonSerializer.Deserialize<LuxuryJacket>(json, CachedJsonSerializerOptions),
                    "Highend" => JsonSerializer.Deserialize<Highend>(json, CachedJsonSerializerOptions),

                    // 투구
                    "Cap" => JsonSerializer.Deserialize<Cap>(json, CachedJsonSerializerOptions),
                    "ForeheadGuard" => JsonSerializer.Deserialize<ForeheadGuard>(json, CachedJsonSerializerOptions),
                    "Headband" => JsonSerializer.Deserialize<Headband>(json, CachedJsonSerializerOptions),
                    "BulletproofHelmet" => JsonSerializer.Deserialize<BulletproofHelmet>(json, CachedJsonSerializerOptions),
                    "Tiara" => JsonSerializer.Deserialize<Tiara>(json, CachedJsonSerializerOptions),
                    "LaurelWreath" => JsonSerializer.Deserialize<LaurelWreath>(json, CachedJsonSerializerOptions),
                    "Biker" => JsonSerializer.Deserialize<Biker>(json, CachedJsonSerializerOptions),
                    "ConeHat" => JsonSerializer.Deserialize<ConeHat>(json, CachedJsonSerializerOptions),
                    "Helmet" => JsonSerializer.Deserialize<Helmet>(json, CachedJsonSerializerOptions),
                    "Rider" => JsonSerializer.Deserialize<Rider>(json, CachedJsonSerializerOptions),
                    "SpaceHelmet" => JsonSerializer.Deserialize<SpaceHelmet>(json, CachedJsonSerializerOptions),
                    "Magitech" => JsonSerializer.Deserialize<MagitechHelmet>(json, CachedJsonSerializerOptions),
                    "Dandy" => JsonSerializer.Deserialize<Dandy>(json, CachedJsonSerializerOptions),
                    "MonsterHelmet" => JsonSerializer.Deserialize<MonsterHelmet>(json, CachedJsonSerializerOptions),
                    "Assassin" => JsonSerializer.Deserialize<Assassin>(json, CachedJsonSerializerOptions),
                    "HornHelmet" => JsonSerializer.Deserialize<HornHelmet>(json, CachedJsonSerializerOptions),
                    "RiderMask" => JsonSerializer.Deserialize<RiderMask>(json, CachedJsonSerializerOptions),
                    "DischargeHat" => JsonSerializer.Deserialize<DischargeHat>(json, CachedJsonSerializerOptions),

                    // 악세사리
                    "Bandage" => JsonSerializer.Deserialize<Bandage>(json, CachedJsonSerializerOptions),
                    "OilingWeapon" => JsonSerializer.Deserialize<OilingWeapon>(json, CachedJsonSerializerOptions),
                    "Absorber" => JsonSerializer.Deserialize<Absorber>(json, CachedJsonSerializerOptions),
                    "Shield" => JsonSerializer.Deserialize<Shield>(json, CachedJsonSerializerOptions),
                    "StabiliserSword" => JsonSerializer.Deserialize<StabiliserSword>(json, CachedJsonSerializerOptions),
                    "Branch" => JsonSerializer.Deserialize<Branch>(json, CachedJsonSerializerOptions),
                    "MagicBracelet" => JsonSerializer.Deserialize<MagicBracelet>(json, CachedJsonSerializerOptions),
                    "DingDingSword" => JsonSerializer.Deserialize<DingDingSword>(json, CachedJsonSerializerOptions),
                    "Aegis" => JsonSerializer.Deserialize<Aegis>(json, CachedJsonSerializerOptions),
                    "Medic" => JsonSerializer.Deserialize<Medic>(json, CachedJsonSerializerOptions),
                    "PowerSword" => JsonSerializer.Deserialize<PowerSword>(json, CachedJsonSerializerOptions),
                    "ThiefTwinBlade" => JsonSerializer.Deserialize<ThiefTwinBlade>(json, CachedJsonSerializerOptions),
                    "Ninxendo" => JsonSerializer.Deserialize<Ninxendo>(json, CachedJsonSerializerOptions),
                    "InsigniaOfKnighthood" => JsonSerializer.Deserialize<InsigniaOfKnighthood>(json, CachedJsonSerializerOptions),
                    "CeremonialSword" => JsonSerializer.Deserialize<CeremonialSword>(json, CachedJsonSerializerOptions),
                    "Dakimakura" => JsonSerializer.Deserialize<Dakimakura>(json, CachedJsonSerializerOptions),
                    "Icebox" => JsonSerializer.Deserialize<Icebox>(json, CachedJsonSerializerOptions),
                    "ExpensiveToothpick" => JsonSerializer.Deserialize<ExpensiveToothpick>(json, CachedJsonSerializerOptions),
                    "HBGRifle" => JsonSerializer.Deserialize<HBGRifle>(json, CachedJsonSerializerOptions),
                    "BlackGun" => JsonSerializer.Deserialize<BlackGun>(json, CachedJsonSerializerOptions),
                    "ShockShot" => JsonSerializer.Deserialize<ShockShot>(json, CachedJsonSerializerOptions),
                    "Harun" => JsonSerializer.Deserialize<Harun>(json, CachedJsonSerializerOptions),
                    "AscensionStone" => JsonSerializer.Deserialize<AscensionStone>(json, CachedJsonSerializerOptions),
                    "CrescentPendant" => JsonSerializer.Deserialize<CrescentPendant>(json, CachedJsonSerializerOptions),
                    "No1Sword" => JsonSerializer.Deserialize<No1Sword>(json, CachedJsonSerializerOptions),
                    "RobotArm" => JsonSerializer.Deserialize<RobotArm>(json, CachedJsonSerializerOptions),
                    "Eva" => JsonSerializer.Deserialize<Eva>(json, CachedJsonSerializerOptions),
                    _ => JsonSerializer.Deserialize<Artifact>(json, CachedJsonSerializerOptions)
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Artifact] JSON 파싱 오류 (ID: {id}): {ex.Message}");
                return null;
            }
        }

        /*
         * GetArtifactImage(id)
         * - id: 유물 ID
         * - return: 해당 유물의 이미지 반환
         */
        internal static Image GetArtifactImage(string id)
        {
            string imagePath = $@"Resources/image/artifact/{id}.png";
            if (File.Exists(imagePath))
            {
                return Image.FromFile(imagePath);
            }
            else
            {
                throw new FileNotFoundException($"유물 이미지 파일을 찾을 수 없습니다: {imagePath}");
            }
        }

        /*
         * GetEquipmentIcons(type)
         * - type: 장비 타입 (예: "weapon", "armor", "accessory")
         * - return: 해당 타입의 장비 아이콘 목록 반환
         */
        internal static Image? GetStatusEffectImage(StatusEffectType type)
        {
            return type switch
            {
                StatusEffectType.Concentration => Image.FromFile(@"Resources/image/status_effect/concentration.png"),
                StatusEffectType.Plague => Image.FromFile(@"Resources/image/status_effect/plague.png"),
                StatusEffectType.BrokenSword => Image.FromFile(@"Resources/image/status_effect/broken_sword.png"),
                StatusEffectType.BrokenShield => Image.FromFile(@"Resources/image/status_effect/broken_shield.png"),
                StatusEffectType.Resurrection => Image.FromFile(@"Resources/image/status_effect/resurrection.png"),
                StatusEffectType.Immortality => Image.FromFile(@"Resources/image/status_effect/immortality.png"),
                StatusEffectType.Stasis => Image.FromFile(@"Resources/image/status_effect/stasis.png"),
                StatusEffectType.Exhaustion => Image.FromFile(@"Resources/image/status_effect/exhaustion.png"),
                StatusEffectType.Slience => Image.FromFile(@"Resources/image/status_effect/silence.png"),
                StatusEffectType.Blind => Image.FromFile(@"Resources/image/status_effect/blind.png"),
                StatusEffectType.HealingBlock => Image.FromFile(@"Resources/image/status_effect/healing_block.png"),
                StatusEffectType.Stun => Image.FromFile(@"Resources/image/status_effect/stun.png"),
                _ => null,
            };
        }

        /*
         * GetEquipments(type)
         * - type: 유물 타입 (예: Weapon, Armor, Accessory)
         * - return: 해당 타입의 유물과 이미지 목록 반환
         */
        internal static Dictionary<string, (Artifact, Image)> GetEquipments(ArtifactType type)
        {
            Dictionary<string, (Artifact, Image)> equipments = [];
            string[] filenames = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "meta_data", "artifact"), $"{(int)type}_*.json");
            foreach (string filename in filenames)
            {
                string id = Path.GetFileNameWithoutExtension(filename); // 파일 이름에서 ID 추출
                Image image = GetArtifactImage(id);
                Artifact? artifact = ReadArtifactData(id);
                if (artifact != null && image != null)
                {
                    equipments.Add(artifact.Id, (artifact, image));
                }
                else
                {
                    return [];
                }
            }
            return equipments;
        }

        /*
         * GetWeatherImage(type)
         * - type: 장비 타입 (예: "weapon", "armor", "accessory")
         * - return: 해당 타입의 장비 아이콘 목록 반환
         */
        internal static Image? GetWeatherImage(WeatherType type)
        {
            string directory = "Resources/image/weather";
            return type switch
            {
                WeatherType.Clear => Image.FromFile(@$"{directory}/clear.png"),
                WeatherType.Rain => Image.FromFile($@"{directory}/rain.png"),
                WeatherType.Snow => Image.FromFile($@"{directory}/snow.png"),
                WeatherType.Fog => Image.FromFile($@"{directory}/fog.png"),
                _ => null,
            };
        }
    }
}

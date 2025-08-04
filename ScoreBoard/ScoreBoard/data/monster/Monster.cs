using ScoreBoard.data.skill;
using ScoreBoard.data.stat;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScoreBoard.data.monster
{
    public class Monster
    {
        public ushort Grade { get; set; } // 몬스터 등급 (예: 0 보스, 1 엘리트, 2 일반).
        public string Id { get; set; } = string.Empty!; // Id
        public string Name { get; set; } = string.Empty!; // 이름
        public Stat Stat { get; set; } = default!; // 스탯
        public ushort SpawnTurn { get; set; } // 스폰 가능한 턴
        public SkillBase? SpawnElites { get; set; } // 보스 몬스터는 엘리트 몬스터를 소환할 수 있음

        [JsonIgnore]
        public Dictionary<ushort, bool> RequiredDiceValues { get; set; } = []; // 행동하기 위해 필요한 주사위 값이 키, 치명타 여부가 값

        [JsonIgnore] 
        public bool IsReported { get; set; } = false; // 보고되었는지 확인

        [JsonIgnore]
        public ushort Count { get; set; } = 0; // 개체 수
        
        [JsonIgnore]
        public string Note { get; set; } = string.Empty!; // 특이사항

        protected void InitialiseNormalElite(string id, ushort spawnTurn)
        {
            Validator.ValidateNull(id, nameof(id));

            var data = DataReader.ReadMonsterData(id) ?? throw new ArgumentException($"데이터 불러오기 오류: {id}");

            Id = data.Id;
            Grade = data.Grade;
            Name = data.Name;
            SpawnTurn = spawnTurn;
            Stat = new Stat
            {
                Hp = data.Stat.Hp,
                MaxHp = data.Stat.Hp, // 최대 체력은 현재 체력과 동일
                Movement = data.Stat.Movement,
                CombatStats = data.Stat.CombatStats.ToDictionary(
                    kv => kv.Key,
                    kv => new CombatStat
                    {
                        Type = kv.Value.Type,
                        Range = kv.Value.Range,
                        AttackCount = kv.Value.AttackCount,
                        Value = kv.Value.Value
                    }
                ) ?? []
            };
        }
    }
}

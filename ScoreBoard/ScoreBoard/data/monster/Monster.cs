using ScoreBoard.data.skill;
using ScoreBoard.data.stat;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.monster
{
    internal class Monster
    {
        public ushort Grade { get; set; } // 몬스터 등급 (예: 0 보스, 1 엘리트, 2 일반).
        public string Id { get; set; } = string.Empty!; // Id
        public string Name { get; set; } = string.Empty!; // 이름
        public Stat Stat { get; set; } = default!; // 스탯
        public ushort[] AttackDiceValue { get; set; } = default!; // 공격이 유효한 주사위 숫자
        public ushort SpawnTurn { get; set; } // 스폰 가능한 턴
        public SkillBase? SpawnElites { get; set; } // 보스 몬스터는 엘리트 몬스터를 소환할 수 있음

        protected void InitialiseNormalElite(string id, ushort spawnTurn)
        {
            Validator.ValidateNull(id, nameof(id));

            var data = DataReader.ReadMonsterData(id) ?? throw new ArgumentException($"데이터 불러오기 오류: {id}");

            Id = data.Id;
            Grade = data.Grade;
            Name = data.Name;
            AttackDiceValue = data.AttackDiceValue;
            SpawnTurn = spawnTurn;
            Stat = new Stat
            {
                Hp = data.Stat.Hp,
                MaxHp = data.Stat.MaxHp,
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

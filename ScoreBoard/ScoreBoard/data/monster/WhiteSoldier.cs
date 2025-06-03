using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.monster
{
    internal class WhiteSoldier : Monster
    {
        public WhiteSoldier(string id) : base()
        {
            Validator.ValidateNull(id, nameof(id));
            
            var data = DataReader.ReadMonsterData(id) ?? throw new ArgumentException($"데이터 불러오기 오류: {id}");

            // 필드 초기화
            this.Id = data.Id;
            this.Name = data.Name;
            this.AttackDiceValue = data.AttackDiceValue;
            this.Stat = new Stat
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
                ) ?? new Dictionary<string, CombatStat>()
            };
        }
    }
}

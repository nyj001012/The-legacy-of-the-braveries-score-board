using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScoreBoard.data
{
    internal class ActiveSkill : Skill
    {
        public ushort Cooldown { get; set; } = 3; // 기술 사용 후 재사용 대기 시간 (예: 3턴 후 재사용 가능)

        public ActiveSkill() : base("", 0, [])
        {
        }

        [JsonIgnore]
        public new Action? Execute { get; set; }
    }
}

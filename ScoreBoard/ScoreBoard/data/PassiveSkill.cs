using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScoreBoard.data
{
    internal class PassiveSkill : Skill
    {
        public bool isActivated { get; set; } = false; // 지속 시간 (예: 2턴 동안 효과 지속). 0인 경우 영구 효과를 의미합니다.

        public PassiveSkill() : base()
        {
        }

        [JsonIgnore]
        public new Action? Execute { get; set; }
    }
}

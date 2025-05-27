using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data
{
    internal class PassiveSkill : Skill
    {
        public ushort Duration { get; private set; } = 0; // 지속 시간 (예: 2턴 동안 효과 지속). 0인 경우 영구 효과를 의미합니다.
        public PassiveSkill(string name, ushort level, string[] description, ushort duration) : base(name, level, description)
        {
            Duration = duration; // 기술 효과 지속 시간 설정
        }

        public void SetDuration(ushort duration)
        {
            Duration = duration; // 지속 시간 설정
        }
    }
}

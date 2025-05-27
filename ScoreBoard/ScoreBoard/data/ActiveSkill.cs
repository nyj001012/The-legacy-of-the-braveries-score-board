using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data
{
    internal class ActiveSkill : Skill
    {
        public ushort Cooldown { get; private set; }
        public ActiveSkill(string name, ushort level, string[] description, ushort cooldown) : base(name, level, description)
        {
            Cooldown = cooldown; // 기술 사용 후 재사용 대기 시간 (예: 3턴 후 재사용 가능)
        }

        public void SetCooldown(ushort cooldown)
        {
            Cooldown = cooldown; // 재사용 대기 시간 설정
        }
    }
}

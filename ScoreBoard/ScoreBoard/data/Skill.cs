using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data
{
    internal class Skill(string name, ushort requiredLevel, string description)
    {
        public string Name { get; } = name; // 기술 이름 (예: "불꽃의 검", "치유의 빛" 등)
        public ushort RequiredLevel { get; } = requiredLevel; // 기술을 사용할 수 있는 레벨 (예: 1, 2, 3 등)
        public string Description { get; } = description; // 기술 설명 (예: "적에게 불꽃 피해를 입힙니다.", "아군을 치유합니다." 등)
    }

    internal class ActiveSkill : Skill
    {
        public ushort Cooldown { get; private set; }
        public ActiveSkill(string name, ushort level, string description, ushort cooldown) : base(name, level, description)
        {
            Cooldown = cooldown; // 기술 사용 후 재사용 대기 시간 (예: 3턴 후 재사용 가능)
        }

        public void SetCooldown(ushort cooldown)
        {
            Cooldown = cooldown; // 재사용 대기 시간 설정
        }
    }

    internal class PassiveSkill : Skill
    {
        public ushort? Duration { get; private set; } // 지속 시간 (예: 2턴 동안 효과 지속). Null인 경우 영구 효과를 의미합니다.
        public PassiveSkill(string name, ushort level, string description, ushort duration) : base(name, level, description)
        {
            Duration = duration; // 기술 효과 지속 시간 설정
        }

        public void SetDuration(ushort duration)
        {
            Duration = duration; // 지속 시간 설정
        }
    }
}

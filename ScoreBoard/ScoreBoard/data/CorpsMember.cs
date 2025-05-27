using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data
{
    internal class CorpsMember(string id, string name, string corpsId, string[] description, Stat stat)
    {
        protected string Id { get; } = id; // 멤버 ID: [군단 ID]_[번호]_이름
        protected string Name { get; } = name; // 멤버 이름 (예: 루다, 스카이하늘소라텐등)
        protected string CorpsId { get; } = corpsId; // 소속 군단 ID (예: "201", "000" 등)
        protected string[] Description { get; } = description; // 배경 설명
        protected Stat Stat { get; } = stat; // 멤버의 능력치 정보
    }

    internal class Stat(ushort movement, List<CombatStat> combatStats)
    {
        public ushort Movement { get; private set; } = movement; // 이동 가능 거리 (예: 1, 2, 3 등)
        public List<CombatStat> CombatStats { get; } = combatStats; // 공격 타입별 공격력 정보 배열 (예: 물리(atk), 마법(sp) 등)

        public void SetMovement(ushort movement)
        {
            Movement = movement;
        }
    }

    internal class CombatStat(string type, ushort range, ushort attackCount, ushort value)
    {
        public string Type { get; } = type; // 공격 타입 (예: 물리(atk), 마법(sp))
        public ushort Range { get; private set; } = range; // 공격 사거리 (예: 1, 2, 3 등)
        public ushort AttackCount { get; private set; } = attackCount; // 공격 횟수 = 굴릴 수 있는 주사위 개수 (예: 1, 2, 3 등)
        public ushort Value { get; private set; } = value; // 공격력 (예: 10, 20 등)

        public void SetRange(ushort range)
        {
            Range = range;
        }

        public void SetAttackCount(ushort attackCount)
        {
            AttackCount = attackCount;
        }

        public void SetValue(ushort value)
        {
            Value = value;
        }
    }
}

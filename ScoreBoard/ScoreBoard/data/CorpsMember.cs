using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data
{
    internal class CorpsMember(string id, string name, string corpsId, string[] description, Stat stat)
    {
        public string Id { get; set; } = id; // 멤버 ID: [군단 ID]_[번호]_이름
        public string Name { get; set; } = name; // 멤버 이름 (예: 루다, 스카이하늘소라텐등)
        public string CorpsId { get; set; } = corpsId; // 소속 군단 ID (예: "201", "000" 등)
        public string[] Description { get; set; } = description; // 배경 설명
        public Stat Stat { get; set; } = stat; // 멤버의 능력치 정보
        public List<PassiveSkill> Passives { get; set; } = []; // 멤버의 패시브 스킬 정보
        public List<ActiveSkill> Actives { get; set; } = []; // 멤버의 능력치 정보
    }

    internal class Stat(ushort hp, ushort movement, ushort wisdom, Dictionary<string, CombatStat> combatStats)
    {
        public ushort Hp { get; set; } = hp; // 체력 (예: 10, 20 등)
        public ushort Movement { get; set; } = movement; // 이동 가능 거리 (예: 1, 2, 3 등)
        public ushort Wisdom { get; set; } = wisdom; // 지혜 = 카드 뽑는 횟수 (예: 1, 2, 3 등) - 선택적 속성
        public Dictionary<string, CombatStat> CombatStats { get; set; } = combatStats; // 공격 타입별 공격력 정보 배열 (예: 물리(atk), 마법(sp) 등)
    }

    internal class CombatStat(string type, ushort range, ushort attackCount, ushort value)
    {
        public string Type { get; set; } = type; // 공격 타입 (예: 물리(atk), 마법(sp))
        public ushort Range { get; set; } = range; // 공격 사거리 (예: 1, 2, 3 등)
        public ushort AttackCount { get; set; } = attackCount; // 공격 횟수 = 굴릴 수 있는 주사위 개수 (예: 1, 2, 3 등)
        public ushort Value { get; set; } = value; // 공격력 (예: 10, 20 등)
    }
}

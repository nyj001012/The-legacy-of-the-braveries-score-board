using ScoreBoard.data.skill;
using ScoreBoard.data.stat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.minion
{
    internal class Minion
    {
        public string Id { get; set; } = string.Empty; // 소환수 ID: [캐릭터 ID]_[번호]_이름
        public string Name { get; set; } = string.Empty; // 소환수 이름 (예: 햄부기 전차)
        public Stat Stat { get; set; } = new(); // 소환수의 능력치 정보
        public List<PassiveSkill> Passives { get; set; } = []; // 소환수의 패시브 스킬 정보
        public List<ActiveSkill> Actives { get; set; } = []; // 소환수의 능력치 정보
        public int SummonAvailableTurn { get; set; } = 0; // 소환 가능 턴 (예: 0턴 후 소환 가능)
        public int SummonEndTurn { get; set; } = -1; // 소환 종료 턴 (예: 3턴 후 소환 종료, -1이면 죽기 전까지 소환 유지)
        public bool IsSummonable { get; set; } = false; // 소환 가능 여부
    }
}

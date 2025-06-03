using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.monster
{
    internal class Monster
    {
        public ushort Type { get; init; } // 몬스터 등급 (예: 0 보스, 1 엘리트, 2 일반).
        public ushort Id { get; init; } // Id
        public required string Name { get; init; } // 이름
        public required Stat Stat { get; init;} // 스탯
        public required ushort[] AttackDiceValue { get; init; } // 공격이 유효한 주사위 숫자
        public required ushort SpawnTurn { get; init; } // 스폰 가능한 턴
        public Skill? SpawnElites { get; set; } // 보스 몬스터는 엘리트 몬스터를 소환할 수 있음
    }
}

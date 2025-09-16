using ScoreBoard.data.artifact;
using ScoreBoard.data.skill;
using ScoreBoard.data.stat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data
{
    public abstract class UnitBase
    {
        public string Id { get; set; } = string.Empty; // ID
        public string Name { get; set; } = string.Empty; // 이름 (예: 햄부기 전차)
        public Dictionary<ushort, bool> RequiredDiceValues { get; set; } = []; // 행동하기 위해 필요한 주사위 값이 키, 치명타 여부가 값
        public Stat Stat { get; set; } = new(); // 능력치 정보
        public List<PassiveSkill> Passives { get; set; } = []; // 패시브 스킬 정보
        public List<ActiveSkill> Actives { get; set; } = []; // 능력치 정보
        public List<Artifact?> ArtifactSlot { get; set; } = []; // 유물 슬롯 정보 (예: 무기, 방어구 등)
        public ushort MaxArtifactSlot { get; set; } = 3; // 최대 유물 슬롯 수 (예: 3개)
        public int WeatherMovementModifier { get; set; } = 0; // 날씨로 인한 이동속도 보정치
        public int WeatherRangeModifier { get; set; } = 0; // 날씨로 인한 공격 사거리 보정치
        public int WeatherDiceModifier { get; set; } = 0; // 날씨로 인한 주사위 개수 보정치. Slice에 활용
        public double SEAttackValueModifier { get; set; } = 1; // 상태이상 공격력 보정치. 곱연산 활용
        public int ArtifactSpellPowerMultiplier { get; set; } = 1; // 유물에 의한 곱연산 보정치
        public string Note { get; set; } = String.Empty; // 특이사항
    }
}

using ScoreBoard.data.artifact;
using ScoreBoard.data.minion;
using ScoreBoard.data.skill;
using ScoreBoard.data.stat;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScoreBoard.data.character
{
    public class CorpsMemberDTO
    {
        public string Id { get; set; } = string.Empty; // 멤버 ID: [군단 ID]_[번호]_이름
        public string Name { get; set; } = string.Empty; // 멤버 이름 (예: 루다, 스카이하늘소라텐등)
        public string CorpsId { get; set; } = string.Empty; // 소속 군단 ID (예: "201", "000" 등)
        public string[] Description { get; set; } = []; // 배경 설명
        public Stat Stat { get; set; } = new(); // 멤버의 능력치 정보
        public List<PassiveSkill> Passives { get; set; } = []; // 멤버의 패시브 스킬 정보
        public List<ActiveSkill> Actives { get; set; } = []; // 멤버의 능력치 정보
        public List<Artifact?> ArtifactSlot { get; set; } = []; // 멤버의 유물 슬롯 정보 (예: 무기, 방어구 등)
        public ushort MaxArtifactSlot { get; set; } = 3; // 최대 유물 슬롯 수 (예: 3개)
        public ushort Level { get; set; } = 0; // 멤버의 레벨 (예: 1, 2, 3 등)
        public Dictionary<ushort, bool> RequiredDiceValues { get; set; } = []; // 멤버가 행동하기 위해 필요한 주사위 값이 키, 치명타 여부가 값
        public string[] MinionIds { get; set; } = []; // 멤버가 소환한 소환수 정보 배열
    }
}

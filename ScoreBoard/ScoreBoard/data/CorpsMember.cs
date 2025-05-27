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
        public List<Artifact> ArtifactSlot { get; set; } = []; // 멤버의 유물 슬롯 정보 (예: 무기, 방어구 등)
    }
}

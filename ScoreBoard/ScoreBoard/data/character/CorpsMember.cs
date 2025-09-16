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
    public class CorpsMember : UnitBase
    {
        public string CorpsId { get; set; } = string.Empty; // 소속 군단 ID (예: "201", "000" 등)
        public string[] Description { get; set; } = []; // 배경 설명
        public ushort Level { get; set; } = 0; // 멤버의 레벨 (예: 1, 2, 3 등)
        public List<Minion> Minions { get; set; } = []; // 멤버가 소환한 소환수 정보 배열
        public string Note { get; set; } = String.Empty; // 특이사항

        public CorpsMember()
        {
            ArtifactSlot = [.. Enumerable.Repeat<Artifact?>(null,  MaxArtifactSlot)]; // 0: 무기, 1: 방어구, 2~3: 액세서리
        }

        /*
         * Initialise(string id)
         * - id를 바탕으로 캐릭터 json 파일을 읽어 필드 세팅
         * - id: 캐릭터 id
         */
        protected void Initialise(string id)
        {
            Validator.ValidateNull(id, nameof(id));
            var data = DataReader.ReadMemberData(id) ?? throw new ArgumentException($"데이터 불러오기 오류: {id}");
            // 필드 초기화
            InitialiseBasicInfo(data);

            // Stat 초기화
            InitialiseStat(data.Stat);
            InitialisePasssiveSkills(data);
            InitialiseActiveSkills(data);

            if (data.MinionIds.Length > 0)
            {
                Minions = data.MinionIds.Select(mid => DataReader.GetMinion(mid)).Where(m => m != null).ToList() ?? [];
            }
        }

        /*
         * InitialiseBasicInfo(CorpsMemberDTO data)
         * - Id, Name, CorpsId, Description 세팅
         * - data: json 파일을 CorpsMemberDTO로 읽어온 데이터
         */
        protected void InitialiseBasicInfo(CorpsMemberDTO data)
        {
            Id = Validator.ValidateNull(data.Id, nameof(data.Id));
            Name = Validator.ValidateNull(data.Name, nameof(data.Name));
            CorpsId = Validator.ValidateNull(data.CorpsId, nameof(data.CorpsId));
            Description = Validator.ValidateNull(data.Description, nameof(data.Description));
        }

        /*
         * InitialiseStat(Stat statData)
         * - Stat과 관련된 데이터 초기화
         * - statData: json 파일의 stat 오브젝트
         */
        protected void InitialiseStat(Stat statData)
        {
            Validator.ValidateNull(statData, nameof(statData));
            Validator.ValidateNull(statData.CombatStats, nameof(statData.CombatStats));
            Stat = new Stat
            {
                Hp = statData.Hp,
                MaxHp = statData.Hp, // 시작 시, 현재 체력은 최대 체력
                Movement = statData.Movement,
                Wisdom = statData.Wisdom,
                SpellPower = statData.SpellPower,
                CombatStats = statData.CombatStats.ToDictionary(
                    kv => kv.Key,
                    kv => new CombatStat
                    {
                        Type = kv.Value.Type,
                        Range = kv.Value.Range,
                        AttackCount = kv.Value.AttackCount,
                        Value = kv.Value.Value
                    }
                ) ?? []
            };
        }

        /*
         * InitialisePasssiveSkills(CorpsMemberDTO data)
         * - 캐릭터의 패시브 스킬 초기화
         * - 각 내용은 객체에서 구현
         */
        protected virtual void InitialisePasssiveSkills(CorpsMemberDTO data) { }

        /*
         * InitialiseActiveSkills(CorpsMemberDTO data)
         * - 캐릭터의 액티브(사용) 스킬 초기화
         * - 각 내용은 객체에서 구현
         */
        protected virtual void InitialiseActiveSkills(CorpsMemberDTO data) { }
    }
}

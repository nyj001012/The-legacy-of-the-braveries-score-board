using ScoreBoard.data.character;
using ScoreBoard.data.skill;
using ScoreBoard.data.stat;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.minion
{
    public class Minion
    {
        public string Id { get; set; } = string.Empty; // 소환수 ID: [캐릭터 ID]_[번호]_이름
        public string Name { get; set; } = string.Empty; // 소환수 이름 (예: 햄부기 전차)
        public Stat Stat { get; set; } = new(); // 소환수의 능력치 정보
        public List<PassiveSkill> Passives { get; set; } = []; // 소환수의 패시브 스킬 정보
        public List<ActiveSkill> Actives { get; set; } = []; // 소환수의 능력치 정보
        public int SummonAvailableTurn { get; set; } = 0; // 소환 가능 턴 (예: 0턴 후 소환 가능)
        public int SummonEndTurn { get; set; } = -1; // 소환 종료 턴 (예: 3턴 후 소환 종료, -1이면 죽기 전까지 소환 유지)
        public bool IsSummonable { get; set; } = false; // 소환 가능 여부

        public void Initialise(string mid)
        {
            Validator.ValidateNull(mid, nameof(mid));
            var data = DataReader.ReadMinionData(mid) ?? throw new ArgumentException($"데이터 불러오기 오류: {mid}");
            // 필드 초기화
            InitialiseBasicInfo(data);

            // Stat 초기화
            InitialiseStat(data.Stat);
            InitialisePasssiveSkills(data);
            InitialiseActiveSkills(data);
        }


        /*
         * InitialiseBasicInfo(Minion data)
         * - Id, Name, CorpsId, Description 세팅
         * - data: json 파일을 Minion으로 읽어온 데이터
         */
        protected void InitialiseBasicInfo(Minion data)
        {
            Id = Validator.ValidateNull(data.Id, nameof(data.Id));
            Name = Validator.ValidateNull(data.Name, nameof(data.Name));
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
         * InitialisePasssiveSkills(Minion data)
         * - 소환수의 패시브 스킬 초기화
         * - 각 내용은 객체에서 구현
         */
        protected virtual void InitialisePasssiveSkills(Minion data) { }

        /*
         * InitialiseActiveSkills(Minion data)
         * - 소환수의 액티브(사용) 스킬 초기화
         * - 각 내용은 객체에서 구현
         */
        protected virtual void InitialiseActiveSkills(Minion data) { }
    }
}

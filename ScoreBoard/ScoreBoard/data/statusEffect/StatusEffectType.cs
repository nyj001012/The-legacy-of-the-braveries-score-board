using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.statusEffect
{
    public enum StatusEffectType
    {
        Concentration, // 집중
        Plague, // 역병
        BrokenSword, // 검 파괴: 공격력 50% 감소
        BrokenShield, // 방패 파괴: 받는 피해 증가
        Reserection, // 부활: 50% 체력으로 부활
        Immortality, // 불사
        Stasis, // 정지: 행동 불가 및 피해를 받지 않음
        Exhaustion, // 탈진
        Slience, // 침묵: 스킬 사용 불가
        Blind, // 실명: 기본 공격 불가
        Stun, // 기절: 행동 불가
        HealingBlock, // 치유 차단: 치유 효과 무효화
    }
}
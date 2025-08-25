using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.statusEffect
{
    public enum StatusEffectType
    {
        [Display(Name = "집중", Description = "행동 불가")]
        Concentration,

        [Display(Name = "역병", Description = "전이 가능")]
        Plague,

        [Display(Name = "검 파괴", Description = "공격력 -50%")]
        BrokenSword,

        [Display(Name = "방패 파괴", Description = "받는 피해 +50%")]
        BrokenShield,

        [Display(Name = "부활", Description = "50% 체력으로 부활")]
        Resurrection,

        [Display(Name = "불사", Description = "체력 1로 생존")]
        Immortality,

        [Display(Name = "정지", Description = "행동 불가, 피격 면역")]
        Stasis,

        [Display(Name = "탈진", Description = "이동속도 감소")]
        Exhaustion,

        [Display(Name = "침묵", Description = "스킬 사용 불가")]
        Slience,

        [Display(Name = "실명", Description = "기본 공격 불가")]
        Blind,

        [Display(Name = "기절", Description = "행동 불가")]
        Stun,

        [Display(Name = "치유 차단", Description = "치유 효과 무효화")]
        HealingBlock,
    }
}
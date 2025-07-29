using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.utils
{
    public static class EnumHelper
    {
        /*
         * GetEnumName(Enum value)
         * - Enum의 DisplayAttribute에서 Name을 가져오는 메소드
         * - 만약 DisplayAttribute가 없다면 Enum의 ToString() 값을 반환합니다.
         * - 사용 예시: GetEnumName(StatusEffectType.Concentration)
         */
        public static string? GetEnumName(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttributes(typeof(DisplayAttribute), false)
                             .Cast<DisplayAttribute>().FirstOrDefault();
            return attr?.Name ?? value.ToString();
        }

        /*
         * GetEnumDescription(Enum value)
         * - Enum의 DisplayAttribute에서 Description을 가져오는 메소드
         * - 만약 DisplayAttribute가 없다면 Enum의 ToString() 값을 반환합니다.
         * - 사용 예시: GetEnumDescription(StatusEffectType.Concentration)
         */
        public static string? GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttributes(typeof(DisplayAttribute), false)
                             .Cast<DisplayAttribute>().FirstOrDefault();
            return attr?.Description ?? value.ToString();
        }
    }

}

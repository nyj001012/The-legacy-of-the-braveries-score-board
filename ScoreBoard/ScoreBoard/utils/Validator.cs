using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.utils
{
    public static class Validator
    {
        public static string ValidateNull(string? value, string name)
        {
            return value ?? throw new ArgumentNullException(name, $"{name} cannot be null.");
        }

        public static string[] ValidateNull(string[]? value, string name)
        {
            return value ?? throw new ArgumentNullException(name, $"{name} cannot be null.");
        }

        public static T ValidateNull<T>(T? value, string name) where T : class
        {
            return value ?? throw new ArgumentNullException(name, $"{name} cannot be null.");
        }

        /*
         * IsTanker<T>(T member)
         * - T는 CorpsMember의 자식 클래스여야 함
         * - member가 Alber, Karma, Seokbungdius, Petrus, Joshua, Griffin 중 하나인지 확인
         */
        public static bool IsTanker<T>(T member) where T : class
        {
            return member is Alber
                || member is Karma
                || member is Seokbungdius
                || member is Petrus
                || member is Joshua
                || member is Griffin;
        }
    }
}

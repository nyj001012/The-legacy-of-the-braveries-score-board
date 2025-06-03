using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ScoreBoard.data;
using static ScoreBoard.utils.DataReader;

namespace ScoreBoardUnitTest.utils
{
    [TestClass]
    public class DataReader
    {
        [TestMethod]
        public void TestReadCorpsData_ValidFile_ReturnsDictionary()
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "units.json");
            Dictionary<string, string> expectedMap = new()
            {
                { "000", "민간 단체" },
                { "001", "군단장" },
                { "002", "사령관" },
                { "003", "대장" },
                { "004", "원수" },
                { "005", "사관학교" },
                { "006", "군사학교" },
                { "007", "군사학교(고급)" }
            };
            Dictionary<string, string> actualMap = ReadCorpsData(jsonPath);
            CollectionAssert.AreEquivalent(expectedMap, actualMap); // 딕셔너리 레퍼런스 비교가 아닌 값 비교
        }

        [TestMethod]
        public void TestReadCorpsData_InvalidFile_ReturnsEmptyDictionary()
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "invalid.json");
            Dictionary<string, string> actualMap = ReadCorpsData(jsonPath);
            Assert.AreEqual(0, actualMap.Count);
        }

        [TestMethod]
        public void TestReadCorpsMembersData_ValidCorpsId_ReturnsDictionary()
        {
            string corpsId = "201"; // 1군단
            Dictionary<string, string> expectedMembers = new()
            {
                { "201_01_Ruda", "루다" },
                { "201_03_SkyHaneulSoraTen", "스카이하늘소라텐" }
            };
            Dictionary<string, string> actualMembers = ReadCorpsMembersData(corpsId);
            CollectionAssert.AreEquivalent(expectedMembers, actualMembers); // 딕셔너리 레퍼런스 비교가 아닌 값 비교
        }

        [TestMethod]
        public void TestReadMonsterGrade()
        {
            Dictionary<string, MonsterGrade>? actualDict = ReadMonsterGrade();
            Assert.IsNotNull(actualDict);
            Assert.IsNotNull(actualDict["보스"]);
            Assert.IsNotNull(actualDict["엘리트"]);
            Assert.IsNotNull(actualDict["일반"]);
        }

        [TestMethod]
        public void TestReadMonsterDataByGradeId()
        {
            ushort id = 2;
            Dictionary<string, string> actualDict = ReadMonsterDataByGradeId(id);
            Dictionary<string, string> expectedDict = new()
            {
                { "2_01", "백병" },
                { "2_02", "흑기사" }
            };
            CollectionAssert.AreEquivalent(expectedDict, actualDict);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ScoreBoard.utils;

namespace ScoreBoardUnitTest.utils
{
    [TestClass]
    public class JsonReaderTests
    {
        [TestMethod]
        public void TestReadCorpsData_ValidFile_ReturnsDictionary()
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "units.json");
            Dictionary<string, string> expectedMap = new Dictionary<string, string>
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
            Dictionary<string, string> actualMap = JsonReader.ReadCorpsData(jsonPath);
            CollectionAssert.AreEquivalent(expectedMap, actualMap); // 딕셔너리 레퍼런스 비교가 아닌 값 비교
        }

        [TestMethod]
        public void TestReadCorpsData_InvalidFile_ReturnsEmptyDictionary()
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "invalid.json");
            Dictionary<string, string> actualMap = JsonReader.ReadCorpsData(jsonPath);
            Assert.AreEqual(0, actualMap.Count);
        }
    }
}

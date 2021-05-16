using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SearchJson;
using SearchJsonTests.Types;
using System.Collections.Generic;

namespace SearchJsonTests
{
    [TestClass]
    public class IsValidJsonTests
    {
        [TestMethod]
        public void IsValidJsonFlatObject()
        {
            var flatObj = new FlatObject1
            {
                Property1 = 1,
                Property2 = "Test 2",
                Property3 = "Test 3"
            };

            var json = JsonConvert.SerializeObject(flatObj);

            Assert.IsTrue(json.IsValidJson());
        }

        [TestMethod]
        public void IsValidJsonArrayFlatObject()
        {
            var flatObj = new List<FlatObject1>
            {
                new FlatObject1
                {
                    Property1 = 1,
                    Property2 = "Test 2",
                    Property3 = "Test 3"
                }
            };

            var json = JsonConvert.SerializeObject(flatObj);

            Assert.IsTrue(json.IsValidJson());
        }

        [TestMethod]
        public void IsValidJsonSimpleObjectNested()
        {
            var simpleObjectNested = new SimpleNestedObject
            {
                Property1 = 1,
                FlatObject1 = new FlatObject1
                {
                    Property1 = 2,
                    Property2 = "Test 3",
                    Property3 = "Test 4"
                }
            };

            var json = JsonConvert.SerializeObject(simpleObjectNested);

            Assert.IsTrue(json.IsValidJson());
        }

        [TestMethod]
        public void IsValidJsonArraySimpleObjectNested()
        {
            var simpleObjectNested = new List<SimpleNestedObject>
            {
                new SimpleNestedObject
                {
                    Property1 = 1,
                    FlatObject1 = new FlatObject1
                    {
                        Property1 = 2,
                        Property2 = "Test 3",
                        Property3 = "Test 4"
                    }
                }
            };

            var json = JsonConvert.SerializeObject(simpleObjectNested);

            Assert.IsTrue(json.IsValidJson());
        }

        [TestMethod]
        public void NotValidJsonSimpleString()
        {
            var notValidJson = "Test3";

            Assert.IsFalse(notValidJson.IsValidJson());
        }


        [TestMethod]
        public void NotValidJsonNoValue()
        {
            var notValidJson = "{\"Test\" : }";

            Assert.IsFalse(notValidJson.IsValidJson());
        }

        [TestMethod]
        public void NotValidJsonOnlyKey()
        {
            var notValidJson = "{\"Test\"}";

            Assert.IsFalse(notValidJson.IsValidJson());
        }

        [TestMethod]
        public void NotValidJsonNullValue()
        {
            string notValidJson = null;

            Assert.IsFalse(notValidJson.IsValidJson());
        }

        [TestMethod]
        public void NotValidJsonWhitespaceValue()
        {
            var notValidJson = string.Empty;

            Assert.IsFalse(notValidJson.IsValidJson());
        }
    }
}

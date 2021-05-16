using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SearchJson;
using SearchJsonTests.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchJsonTests
{
    [TestClass]
    public class TrySearchItemTests
    {
        [TestMethod]
        public void SearchFlatObjectString()
        {
            var flatObject = new FlatObject1
            {
                Property1 = 1,
                Property2 = "Test 2",
                Property3 = "Test 3"
            };

            var json = JsonConvert.SerializeObject(flatObject);

            var success = json.TrySearchItem(nameof(flatObject.Property2), out var itemValue);

            Assert.IsTrue(success);
            Assert.AreEqual(flatObject.Property2, itemValue);
        }

        [TestMethod]
        public void SearchFlatObjectStringArray()
        {
            var flatObjectArray = new List<FlatObject1>
            {
                new FlatObject1
                {
                    Property1 = 1,
                    Property2 = "Test 2",
                    Property3 = "Test 3"
                }
            };

            var json = JsonConvert.SerializeObject(flatObjectArray);

            var success = json.TrySearchItem(nameof(FlatObject1.Property2), out var itemValue);

            Assert.IsTrue(success);
            Assert.AreEqual(flatObjectArray.FirstOrDefault().Property2, itemValue);
        }

        [TestMethod]
        public void SearchFlatObjectInteger()
        {
            var flatObject = new FlatObject1
            {
                Property1 = 1,
                Property2 = "Test 2",
                Property3 = "Test 3"
            };

            var json = JsonConvert.SerializeObject(flatObject);

            var success = json.TrySearchItem<int>(nameof(flatObject.Property1), out var itemValue);

            Assert.IsTrue(success);
            Assert.AreEqual(flatObject.Property1, itemValue);
        }

        [TestMethod]
        public void SearchFlatObjectDateTime()
        {
            var flatObject = new FlatObject2
            {
                Property6 = DateTime.UtcNow
            };

            var json = JsonConvert.SerializeObject(flatObject);

            var success = json.TrySearchItem<DateTime>(nameof(flatObject.Property6), out var itemValue);

            Assert.IsTrue(success);
            Assert.AreEqual(flatObject.Property6, itemValue);
        }

        [TestMethod]
        public void SearchFlatObjectDecimal()
        {
            var flatObject = new FlatObject2
            {
                Property7 = 100.1m
            };

            var json = JsonConvert.SerializeObject(flatObject);

            var success = json.TrySearchItem<decimal>(nameof(flatObject.Property7), out var itemValue);

            Assert.IsTrue(success);
            Assert.AreEqual(flatObject.Property7, itemValue);
        }

        [TestMethod]
        public void SearchFlatObjectDouble()
        {
            var flatObject = new FlatObject2
            {
                Property8 = 100.1d
            };

            var json = JsonConvert.SerializeObject(flatObject);

            var success = json.TrySearchItem<double>(nameof(flatObject.Property8), out var itemValue);

            Assert.IsTrue(success);
            Assert.AreEqual(flatObject.Property8, itemValue);
        }


        [TestMethod]
        public void SearchFlatObjectBool()
        {
            var flatObject = new FlatObject2
            {
                Property9 = true
            };

            var json = JsonConvert.SerializeObject(flatObject);

            var success = json.TrySearchItem<bool>(nameof(flatObject.Property9), out var itemValue);

            Assert.IsTrue(success);
            Assert.AreEqual(flatObject.Property9, itemValue);
        }


        [TestMethod]
        public void SearchFlatObjectIntegerArray()
        {
            var flatObjectArray = new List<FlatObject1>
            {
                new FlatObject1
                {
                    Property1 = 1,
                    Property2 = "Test 2",
                    Property3 = "Test 3"
                }
            };

            var json = JsonConvert.SerializeObject(flatObjectArray);

            var success = json.TrySearchItem<int>(nameof(FlatObject1.Property1), out var itemValue);

            Assert.IsTrue(success);
            Assert.AreEqual(flatObjectArray.FirstOrDefault().Property1, itemValue);
        }

        [TestMethod]
        public void SimpleObjectNestedString()
        {
            var simpleObjectNested = new SimpleNestedObject
            {
                Property1 = 1,
                FlatObject1 = new FlatObject1
                {
                    Property1 = 2,
                    Property2 = "Test 3",
                    Property3 = "Test 4"
                },
                FlatObject2 = new FlatObject2
                {
                    Property3 = "Test 5",
                    Property4 = 6,
                    Property5 = "Test 7",
                }
            };

            var json = JsonConvert.SerializeObject(simpleObjectNested);

            var success = json.TrySearchItem(nameof(FlatObject1.Property3), out var itemValue);

            Assert.IsTrue(success);
            Assert.AreEqual(simpleObjectNested.FlatObject1.Property3, itemValue);
        }

        [TestMethod]
        public void SimpleObjectNestedStringArray()
        {
            var simpleObjectNestedArray = new List<SimpleNestedObject>
            {
                new SimpleNestedObject
                {
                    Property1 = 1,
                    FlatObject1 = new FlatObject1
                    {
                        Property1 = 2,
                        Property2 = "Test 3",
                        Property3 = "Test 4"
                    },
                    FlatObject2 = new FlatObject2
                    {
                        Property3 = "Test 5",
                        Property4 = 6,
                        Property5 = "Test 7",
                    }
                },
                new SimpleNestedObject
                {
                    Property1 = 8,
                    FlatObject1 = new FlatObject1
                    {
                        Property1 = 9,
                        Property2 = "Test 10",
                        Property3 = "Test 11"
                    },
                    FlatObject2 = new FlatObject2
                    {
                        Property3 = "Test 12",
                        Property4 = 13,
                        Property5 = "Test 14",
                    }
                }
            };

            var json = JsonConvert.SerializeObject(simpleObjectNestedArray);

            var success = json.TrySearchItem(nameof(FlatObject1.Property3), out var itemValue);

            Assert.IsTrue(success);
            Assert.AreEqual(simpleObjectNestedArray.FirstOrDefault().FlatObject1.Property3, itemValue);
        }

        [TestMethod]
        public void SimpleObjectNestedInteger()
        {
            var simpleObjectNested = new SimpleNestedObject
            {
                Property1 = 1,
                FlatObject1 = new FlatObject1
                {
                    Property1 = 2,
                    Property2 = "Test 3",
                    Property3 = "Test 4"
                },
                FlatObject2 = new FlatObject2
                {
                    Property3 = "Test 5",
                    Property4 = 6,
                    Property5 = "Test 7",
                }
            };

            var json = JsonConvert.SerializeObject(simpleObjectNested);

            var success = json.TrySearchItem<int>(nameof(FlatObject2.Property4), out var itemValue);

            Assert.IsTrue(success);
            Assert.AreEqual(simpleObjectNested.FlatObject2.Property4, itemValue);
        }

        [TestMethod]
        public void SimpleObjectNestedIntegerArray()
        {
            var simpleObjectNestedArray = new List<SimpleNestedObject>
            {
                new SimpleNestedObject
                {
                    Property1 = 1,
                    FlatObject1 = new FlatObject1
                    {
                        Property1 = 2,
                        Property2 = "Test 3",
                        Property3 = "Test 4"
                    },
                    FlatObject2 = new FlatObject2
                    {
                        Property3 = "Test 5",
                        Property4 = 6,
                        Property5 = "Test 7",
                    }
                },
                new SimpleNestedObject
                {
                    Property1 = 8,
                    FlatObject1 = new FlatObject1
                    {
                        Property1 = 9,
                        Property2 = "Test 10",
                        Property3 = "Test 11"
                    },
                    FlatObject2 = new FlatObject2
                    {
                        Property3 = "Test 12",
                        Property4 = 13,
                        Property5 = "Test 14",
                    }
                }
            };

            var json = JsonConvert.SerializeObject(simpleObjectNestedArray);

            var success = json.TrySearchItem<int>(nameof(FlatObject2.Property4), out var itemValue);

            Assert.IsTrue(success);
            Assert.AreEqual(simpleObjectNestedArray.FirstOrDefault().FlatObject2.Property4, itemValue);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SearchJson;
using SearchJsonTests.Types;
using System.Collections.Generic;
using System.Linq;

namespace SearchJsonTests
{
    [TestClass]
    public class TrySearchItemsTests
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

            var success = json.TrySearchItems(nameof(FlatObject1.Property2), out var itemValues);

            Assert.IsTrue(success);
            Assert.AreEqual(flatObject.Property2, itemValues.FirstOrDefault());
        }

        [TestMethod]
        public void SearchFlatObjectStringArray()
        {
            var flatObj = new List<FlatObject1>
            {
                new FlatObject1
                {
                    Property1 = 1,
                    Property2 = "Test 2",
                    Property3 = "Test 3"
                },
                new FlatObject1
                {
                    Property1 = 3,
                    Property2 = "Test 4",
                    Property3 = "Test 5"
                }
            };
            var json = JsonConvert.SerializeObject(flatObj);

            var success = json.TrySearchItems(nameof(FlatObject1.Property2), out var itemValues);

            Assert.IsTrue(success);
            Assert.IsTrue(itemValues.Distinct().Count() == 2);
            Assert.IsTrue(
                itemValues.Any(x =>
                    flatObj.FirstOrDefault().Property2 == x
                )
                && itemValues.Any(x =>
                   flatObj.LastOrDefault().Property2 == x
                )
            );
        }

        [TestMethod]
        public void SearchFlatObjectInt()
        {
            var flatObject = new FlatObject1
            {
                Property1 = 1,
                Property2 = "Test 2",
                Property3 = "Test 3"
            };

            var json = JsonConvert.SerializeObject(flatObject);

            var success = json.TrySearchItems<int>(nameof(FlatObject1.Property1), out var itemValues);

            Assert.IsTrue(success);
            Assert.AreEqual(flatObject.Property1, itemValues.FirstOrDefault());
        }

        [TestMethod]
        public void SearchFlatObjectIntArray()
        {
            var flatObj = new List<FlatObject1>
            {
                new FlatObject1
                {
                    Property1 = 1,
                    Property2 = "Test 2",
                    Property3 = "Test 3"
                },
                new FlatObject1
                {
                    Property1 = 3,
                    Property2 = "Test 4",
                    Property3 = "Test 5"
                }
            };
            var json = JsonConvert.SerializeObject(flatObj);

            var success = json.TrySearchItems<int>(nameof(FlatObject1.Property1), out var itemValues);

            Assert.IsTrue(success);
            Assert.IsTrue(itemValues.Distinct().Count() == 2);
            Assert.IsTrue(
                itemValues.Any(x =>
                    flatObj.FirstOrDefault().Property1 == x
                )
                && itemValues.Any(x =>
                   flatObj.LastOrDefault().Property1 == x
                )
            );
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

            var success = json.TrySearchItems(nameof(FlatObject1.Property3), out var itemValues);

            Assert.IsTrue(success);
            Assert.IsTrue(itemValues.Distinct().Count() == 2);
            Assert.IsTrue(
                itemValues.Any(x =>
                    simpleObjectNested.FlatObject1.Property3 == x
                )
                && itemValues.Any(x =>
                   simpleObjectNested.FlatObject2.Property3 == x
                )
            );
        }

        [TestMethod]
        public void SimpleObjectNestedStringArray()
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

            var json = JsonConvert.SerializeObject(simpleObjectNested);

            var success = json.TrySearchItems(nameof(FlatObject1.Property3), out var itemValues);

            Assert.IsTrue(success);
            Assert.IsTrue(itemValues.Distinct().Count() == 4);
            Assert.IsTrue(
                itemValues.Any(x =>
                    simpleObjectNested.FirstOrDefault().FlatObject1.Property3 == x
                )
                && itemValues.Any(x =>
                    simpleObjectNested.LastOrDefault().FlatObject1.Property3 == x
                )
                && itemValues.Any(x =>
                   simpleObjectNested.FirstOrDefault().FlatObject2.Property3 == x
                )
                && itemValues.Any(x =>
                   simpleObjectNested.LastOrDefault().FlatObject2.Property3 == x
                )
            );
        }

        [TestMethod]
        public void SimpleObjectNestedInt()
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

            var success = json.TrySearchItems<int>(nameof(FlatObject1.Property1), out var itemValues);

            Assert.IsTrue(success);
            Assert.IsTrue(itemValues.Distinct().Count() == 2);
            Assert.IsTrue(
                itemValues.Any(x =>
                    simpleObjectNested.Property1 == x
                )
                && itemValues.Any(x =>
                   simpleObjectNested.FlatObject1.Property1 == x
                )
            );
        }

        [TestMethod]
        public void SimpleObjectNestedIntArray()
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

            var json = JsonConvert.SerializeObject(simpleObjectNested);

            var success = json.TrySearchItems<int>(nameof(FlatObject1.Property1), out var itemValues);

            Assert.IsTrue(success);
            Assert.IsTrue(itemValues.Distinct().Count() == 4);
            Assert.IsTrue(
                itemValues.Any(x =>
                    simpleObjectNested.FirstOrDefault().Property1 == x
                )
                && itemValues.Any(x =>
                    simpleObjectNested.LastOrDefault().Property1 == x
                )
                && itemValues.Any(x =>
                   simpleObjectNested.FirstOrDefault().FlatObject1.Property1 == x
                )
                && itemValues.Any(x =>
                   simpleObjectNested.LastOrDefault().FlatObject1.Property1 == x
                )
            );
        }


        [TestMethod]
        public void ComplexNestedObject()
        {
            var complexNestedObject = new ComplexNestedObject
            {
                Property1 = new FlatObject1
                {
                    Property1 = 1,
                    Property2 = "Test 2",
                    Property3 = "Test 3"
                },
                Property2 = new SimpleNestedObject
                {
                    Property1 = 4,
                    FlatObject1 = new FlatObject1
                    {
                        Property1 = 5,
                        Property2 = "Test 6",
                        Property3 = "Test 7"
                    },
                    FlatObject2 = new FlatObject2
                    {
                        Property3 = "Test 8",
                        Property4 = 9,
                        Property5 = "Test 10",
                    }
                },
                Property3 = "Test 11"
            };

            var json = JsonConvert.SerializeObject(complexNestedObject);

            var success = json.TrySearchItems(nameof(FlatObject1.Property3), out var itemValues);

            Assert.IsTrue(success);
            Assert.IsTrue(itemValues.Distinct().Count() == 4);
            Assert.IsTrue(
                itemValues.Any(x =>
                    complexNestedObject.Property3 == x
                )
                && itemValues.Any(x =>
                    complexNestedObject.Property1.Property3 == x
                )
                && itemValues.Any(x =>
                   complexNestedObject.Property2.FlatObject1.Property3 == x
                )
                && itemValues.Any(x =>
                   complexNestedObject.Property2.FlatObject2.Property3 == x
                )
            );
        }

        [TestMethod]
        public void ComplexNestedObjectArray()
        {
            var complexNestedObject = new List<ComplexNestedObject>
            {
                new ComplexNestedObject
                {
                    Property1 = new FlatObject1
                    {
                        Property1 = 1,
                        Property2 = "Test 2",
                        Property3 = "Test 3"
                    },
                    Property2 = new SimpleNestedObject
                    {
                        Property1 = 4,
                        FlatObject1 = new FlatObject1
                        {
                            Property1 = 5,
                            Property2 = "Test 6",
                            Property3 = "Test 7"
                        },
                        FlatObject2 = new FlatObject2
                        {
                            Property3 = "Test 8",
                            Property4 = 9,
                            Property5 = "Test 10",
                        }
                    },
                    Property3 = "Test 11"
                },
                new ComplexNestedObject
                {
                    Property1 = new FlatObject1
                    {
                        Property1 = 12,
                        Property2 = "Test 13",
                        Property3 = "Test 14"
                    },
                    Property2 = new SimpleNestedObject
                    {
                        Property1 = 15,
                        FlatObject1 = new FlatObject1
                        {
                            Property1 = 16,
                            Property2 = "Test 17",
                            Property3 = "Test 18"
                        },
                        FlatObject2 = new FlatObject2
                        {
                            Property3 = "Test 19",
                            Property4 = 20,
                            Property5 = "Test 21",
                        }
                    },
                    Property3 = "Test 2"
                }
            };

            var json = JsonConvert.SerializeObject(complexNestedObject);

            var success = json.TrySearchItems(nameof(FlatObject1.Property3), out var itemValues);

            Assert.IsTrue(success);
            Assert.IsTrue(itemValues.Distinct().Count() == 8);
            Assert.IsTrue(
                itemValues.Any(x =>
                    complexNestedObject.FirstOrDefault().Property3 == x
                )
                && itemValues.Any(x =>
                    complexNestedObject.LastOrDefault().Property3 == x
                )
                && itemValues.Any(x =>
                    complexNestedObject.FirstOrDefault().Property1.Property3 == x
                )
                && itemValues.Any(x =>
                    complexNestedObject.LastOrDefault().Property1.Property3 == x
                )
                && itemValues.Any(x =>
                   complexNestedObject.FirstOrDefault().Property2.FlatObject1.Property3 == x
                )
                && itemValues.Any(x =>
                   complexNestedObject.LastOrDefault().Property2.FlatObject1.Property3 == x
                )
                && itemValues.Any(x =>
                   complexNestedObject.FirstOrDefault().Property2.FlatObject2.Property3 == x
                )
                && itemValues.Any(x =>
                   complexNestedObject.LastOrDefault().Property2.FlatObject2.Property3 == x
                )
            );
        }

        [TestMethod]
        public void ComplexNestedObjectDifferentTypes()
        {
            var complexNestedObject = new ComplexNestedObject
            {
                Property1 = new FlatObject1
                {
                    Property1 = 1,
                    Property2 = "Test 2",
                    Property3 = "Test 3"
                },
                Property2 = new SimpleNestedObject
                {
                    Property1 = 4,
                    FlatObject1 = new FlatObject1
                    {
                        Property1 = 5,
                        Property2 = "Test 6",
                        Property3 = "Test 7"
                    },
                    FlatObject2 = new FlatObject2
                    {
                        Property3 = "Test 8",
                        Property4 = 9,
                        Property5 = "Test 10",
                    }
                },
                Property3 = "Test 11"
            };

            var json = JsonConvert.SerializeObject(complexNestedObject);

            var success = json.TrySearchItems(nameof(FlatObject1.Property2), out var itemValues);

            Assert.IsTrue(success);
            Assert.IsTrue(itemValues.Distinct().Count() == 2);
            Assert.IsTrue(
                itemValues.Any(x =>
                    complexNestedObject.Property1.Property2 == x
                )
                && itemValues.Any(x =>
                    complexNestedObject.Property2.FlatObject1.Property2 == x
                )
            );
        }

        [TestMethod]
        public void ComplexNestedObjectArrayDifferentTypes()
        {
            var complexNestedObject = new List<ComplexNestedObject>
            {
                new ComplexNestedObject
                {
                    Property1 = new FlatObject1
                    {
                        Property1 = 1,
                        Property2 = "Test 2",
                        Property3 = "Test 3"
                    },
                    Property2 = new SimpleNestedObject
                    {
                        Property1 = 4,
                        FlatObject1 = new FlatObject1
                        {
                            Property1 = 5,
                            Property2 = "Test 6",
                            Property3 = "Test 7"
                        },
                        FlatObject2 = new FlatObject2
                        {
                            Property3 = "Test 8",
                            Property4 = 9,
                            Property5 = "Test 10",
                        }
                    },
                    Property3 = "Test 11"
                },
                new ComplexNestedObject
                {
                    Property1 = new FlatObject1
                    {
                        Property1 = 12,
                        Property2 = "Test 13",
                        Property3 = "Test 14"
                    },
                    Property2 = new SimpleNestedObject
                    {
                        Property1 = 15,
                        FlatObject1 = new FlatObject1
                        {
                            Property1 = 16,
                            Property2 = "Test 17",
                            Property3 = "Test 18"
                        },
                        FlatObject2 = new FlatObject2
                        {
                            Property3 = "Test 19",
                            Property4 = 20,
                            Property5 = "Test 21",
                        }
                    },
                    Property3 = "Test 2"
                }
            };

            var json = JsonConvert.SerializeObject(complexNestedObject);

            var success = json.TrySearchItems(nameof(FlatObject1.Property2), out var itemValues);

            Assert.IsTrue(success);
            Assert.IsTrue(itemValues.Distinct().Count() == 4);
            Assert.IsTrue(
                itemValues.Any(x =>
                    complexNestedObject.FirstOrDefault().Property1.Property2 == x
                )
                && itemValues.Any(x =>
                    complexNestedObject.LastOrDefault().Property1.Property2 == x
                )
                && itemValues.Any(x =>
                    complexNestedObject.FirstOrDefault().Property2.FlatObject1.Property2 == x
                )
                && itemValues.Any(x =>
                    complexNestedObject.LastOrDefault().Property2.FlatObject1.Property2 == x
                )
            );
        }
    }
}

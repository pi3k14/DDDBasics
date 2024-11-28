using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kodefabrikken.DDD.Specification.UnitTests
{
    public class SpecificationTests
    {
        protected class TestData
        {
            public int Id { get; set; }
            public
#if NET6_0_OR_GREATER
                required
#endif
                string Name
            { get; set; }
        }

        protected static TestData[] TestDataArray = new TestData[] { new TestData { Id = 1, Name = "One" },
                                                                     new TestData { Id = 2, Name = "Two" }};
        protected static IQueryable<TestData> QueryableTestData = TestDataArray.AsQueryable();
        protected static IEnumerable<TestData> EnumerableTestData = TestDataArray.AsEnumerable();

        [TestClass]
        public class static_Not
        {
            [TestMethod]
            public void TrueSpecification_FindNone()
            {
                Assert.IsNull(Array.Find(TestDataArray, Specification<TestData>.Not(p => true)));
            }

            [TestMethod]
            public void FalseSpecification_FindAll()
            {
                CollectionAssert.AreEquivalent(TestDataArray, Array.FindAll(TestDataArray, Specification<TestData>.Not(p => false)));
            }
        }

        [TestClass]
        public class Not
        {
            [TestMethod]
            public void TrueSpecification_FindNone()
            {
                Specification<TestData> spec = new Specification<TestData>(p => true);

                Assert.IsNull(Array.Find(TestDataArray, spec.Not()));
            }

            [TestMethod]
            public void FalseSpecification_FindAll()
            {
                Specification<TestData> spec = new Specification<TestData>(p => false);

                CollectionAssert.AreEquivalent(TestDataArray, Array.FindAll(TestDataArray, spec.Not()));
            }
        }

        [TestClass]
        public class static_And
        {
            [TestMethod]
            public void FalseAndTrueSpecification_FindNone()
            {
                Assert.IsNull(Array.Find(TestDataArray, Specification<TestData>.And(p => false, p => true)));
            }

            [TestMethod]
            public void TrueAndTrueSpecification_FindAll()
            {
                CollectionAssert.AreEquivalent(TestDataArray, Array.FindAll(TestDataArray, Specification<TestData>.And(p => true, p => true)));
            }
        }

        [TestClass]
        public class And
        {
            [TestMethod]
            public void FalseAndTrueSpecification_FindNone()
            {
                Specification<TestData> spec1 = new Specification<TestData>(p => false);
                Specification<TestData> spec2 = new Specification<TestData>(p => true);

                Specification<TestData> test = spec1.And(spec2);

                Assert.IsNull(Array.Find(TestDataArray, test));
            }

            [TestMethod]
            public void TrueAndTrueSpecification_FindAll()
            {
                Specification<TestData> spec1 = new Specification<TestData>(p => true);
                Specification<TestData> spec2 = new Specification<TestData>(p => true);

                Specification<TestData> test = spec1.And(spec2);

                CollectionAssert.AreEquivalent(TestDataArray, Array.FindAll(TestDataArray, test));
            }
        }

        [TestClass]
        public class static_Or
        {
            [TestMethod]
            public void FalseOrTrueSpecification_FindAll()
            {
                CollectionAssert.AreEquivalent(TestDataArray, Array.FindAll(TestDataArray, Specification<TestData>.Or(p => false, p => true)));
            }

            [TestMethod]
            public void FalseOrFalseSpecification_FindNone()
            {
                Assert.IsNull(Array.Find(TestDataArray, Specification<TestData>.Or(p => false, p => false)));
            }
        }

        [TestClass]
        public class Or
        {
            [TestMethod]
            public void FalseOrTrueSpecification_FindAll()
            {
                Specification<TestData> spec1 = new Specification<TestData>(p => false);
                Specification<TestData> spec2 = new Specification<TestData>(p => true);

                Specification<TestData> test = spec1.Or(spec2);

                CollectionAssert.AreEquivalent(TestDataArray, Array.FindAll(TestDataArray, test));
            }

            [TestMethod]
            public void FalseOrFalseSpecification_FindNone()
            {
                Specification<TestData> spec1 = new Specification<TestData>(p => false);
                Specification<TestData> spec2 = new Specification<TestData>(p => false);

                Specification<TestData> test = spec1.Or(spec2);

                Assert.IsNull(Array.Find(TestDataArray, test));
            }
        }

        [TestClass]
        public class static_All
        {
            [TestMethod]
            public void Specification_FindAll()
            {
                CollectionAssert.AreEquivalent(TestDataArray, Array.FindAll(TestDataArray, Specification<TestData>.All));
            }
        }

        [TestClass]
        public class static_None
        {
            [TestMethod]
            public void Specification_FindNone()
            {
                Assert.IsNull(Array.Find(TestDataArray, Specification<TestData>.None));
            }
        }

        [TestClass]
        public class Expression
        {
            [TestMethod]
            public void SearchForId1_FindId1()
            {
                Specification<TestData> spec = new Specification<TestData>(p => p.Id == 1);

                Assert.AreEqual(1, QueryableTestData.Where(spec.Expression).Single().Id);
            }
        }

        [TestClass]
        public class operator_Expression
        {
            [TestMethod]
            public void SearchForId1_FindId1()
            {
                Specification<TestData> spec = new Specification<TestData>(p => p.Id == 1);

                Assert.AreEqual(1, QueryableTestData.Where(spec).Single().Id);
            }
        }

        [TestClass]
        public class Delegate
        {
            [TestMethod]
            public void SearchForId1_FindId1()
            {
                Specification<TestData> spec = new Specification<TestData>(p => p.Id == 1);

                Assert.AreEqual(1, EnumerableTestData.Where(spec.Delegate).Single().Id);
            }
        }

        [TestClass]
        public class operator_Delegate
        {
            [TestMethod]
            public void SearchForId1_FindId1()
            {
                Specification<TestData> spec = new Specification<TestData>(p => p.Id == 1);

                Assert.AreEqual(1, EnumerableTestData.Where(spec).Single().Id);
            }
        }

        [TestClass]
        public class Predicate
        {
            [TestMethod]
            public void SearchForId1_FindId1()
            {
                Specification<TestData> spec = new Specification<TestData>(p => p.Id == 1);

                TestData[] result = Array.FindAll(TestDataArray, spec.Predicate);
                Assert.AreEqual(1, result.Length);
                Assert.AreEqual(1, result[0].Id);
            }
        }

        [TestClass]
        public class operator_Predicate
        {
            [TestMethod]
            public void SearchForId1_FindId1()
            {
                Specification<TestData> spec = new Specification<TestData>(p => p.Id == 1);

                TestData[] result = Array.FindAll(TestDataArray, spec);
                Assert.AreEqual(1, result.Length);
                Assert.AreEqual(1, result[0].Id);
            }
        }
    }
}

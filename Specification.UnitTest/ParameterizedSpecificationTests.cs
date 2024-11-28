using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kodefabrikken.DDD.Specification.UnitTests
{
    public class ParameterizedSpecificationTests
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
        [TestClass]
        public class Bind
        {
            [TestMethod]
            public void Binding2ToParametereInIdSearch_FindId2()
            {
                ParameterizedSpecification<TestData, int> specification = new ParameterizedSpecification<TestData, int>((p, x) => p.Id == x);

                TestData[] result = Array.FindAll(TestDataArray, specification.Bind(2));

                Assert.AreEqual(1, result.Length);
                Assert.AreEqual(2, result[0].Id);
            }
        }
    }
}

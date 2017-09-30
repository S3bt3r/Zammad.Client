using System;
using Xunit;

namespace Zammad.Client.Core.Internal
{
    public class TypeUtilityTest
    {
        [Theory]
        [InlineData("Zammad", "Zammad")]
        [InlineData(null, null)]
        public void CopyProperties_Success_Test(string value1, string value3)
        {
            var testBaseObject = new TypeUtilityTestBaseObject
            {
                Value1 = value1
            };

            var testDerivedObject = new TypeUtilityTestDerivedObject
            {
                Value3 = value3
            };

            TypeUtility.CopyProperties(testBaseObject, testDerivedObject);

            Assert.Equal(value1, testDerivedObject.Value1);
            Assert.Equal(value3, testDerivedObject.Value3);
        }

        [Theory]
        [InlineData("Zammad", "Zammad")]
        [InlineData(null, null)]
        public void CopyProperties_Fail_Test(string value1, string value3)
        {
            var testBaseObject = new TypeUtilityTestBaseObject
            {
                Value1 = value1
            };

            var testDerivedObject = new TypeUtilityTestDerivedObject
            {
                Value3 = value3
            };

            Assert.ThrowsAny<ArgumentException>(() =>
            {
                TypeUtility.CopyProperties(testDerivedObject, testBaseObject);
            });
        }
    }

    public class TypeUtilityTestBaseObject
    {

        public string Value1 { get; set; }
        public string Value2 { get; }
    }

    public class TypeUtilityTestDerivedObject : TypeUtilityTestBaseObject
    {
        public string Value3 { get; set; }
    }
}

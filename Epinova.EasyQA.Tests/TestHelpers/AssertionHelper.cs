using NUnit.Framework;

namespace Epinova.EasyQA.Tests.TestHelpers
{
    public static class AssertionHelper
    {
        public static void ShouldBe(this int actual, int expected)
        {
            Assert.AreEqual(expected, actual);
        }

        public static void ShouldBe(this string actual, string expected)
        {
            Assert.AreEqual(expected, actual);
        }

        public static void ShouldBe(this bool actual, bool expected)
        {
            Assert.AreEqual(expected, actual);
        }
    }
}
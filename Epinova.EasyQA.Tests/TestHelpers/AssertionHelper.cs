using NUnit.Framework;

namespace Epinova.EasyQA.Tests.TestHelpers
{
    public static class AssertionHelper
    {
        public static void ShouldEqual(this int actual, int expected)
        {
            Assert.AreEqual(expected, actual);
        }

        public static void ShouldEqual(this string actual, string expected)
        {
            Assert.AreEqual(expected, actual);
        }

        public static void ShouldEqual(this bool actual, bool expected)
        {
            Assert.AreEqual(expected, actual);
        }
    }
}
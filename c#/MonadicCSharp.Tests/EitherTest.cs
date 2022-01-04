using NUnit.Framework;
using static MonadicCSharp.EitherFactory;

namespace MonadicCSharp.Tests;

[TestFixture]
public class EitherTest
{
    [Test]
    public void ConsumeLeft()
    {
        Left<int, string>(0).ConsumeLeft(actual => Assert.AreEqual(0, actual));
        Left<int, string>(0).ConsumeRight(_ => Assert.Fail());
    }

    [Test]
    public void ConsumeRight()
    {
        Right<int, string>("0").ConsumeLeft(_ => Assert.Fail());
        Right<int, string>("0").ConsumeRight(actual => Assert.AreEqual("0", actual));
    }
}
using NUnit.Framework;

namespace Canvas.Tests;

[TestFixture]
public class EitherTest
{
    [Test]
    public void ConsumeLeft()
    {
        Either<int, string>.MakeLeft(0).ConsumeLeft(actual => Assert.AreEqual(0, actual));
        Either<int, string>.MakeLeft(0).ConsumeRight(_ => Assert.Fail());
    }

    [Test]
    public void ConsumeRight()
    {
        Either<int, string>.MakeRight("0").ConsumeLeft(_ => Assert.Fail());
        Either<int, string>.MakeRight("0").ConsumeRight(actual => Assert.AreEqual("0", actual));
    }
}
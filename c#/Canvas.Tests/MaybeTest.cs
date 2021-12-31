using NUnit.Framework;

namespace Canvas.Tests;

[TestFixture]
public class MaybeTest
{
    [Test]
    public void ConsumeNone()
    {
        MaybeFactory.None<int>()
            .ConsumeNone(Assert.Pass)
            .ConsumeSome(_ => Assert.Fail());
    }

    [Test]
    public void ConsumeSome()
    {
        MaybeFactory.Some(1)
            .ConsumeNone(Assert.Fail)
            .ConsumeSome(v => Assert.AreEqual(1, v));
    }
}
using System;
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

static class MaybeFactory
{
    public static Maybe<S> Some<S>(S someValue)
    {
        return Maybe<S>.MakeSome(someValue);
    }

    public static Maybe<S> None<S>()
    {
        return Maybe<S>.MakeNone();
    }
}

public abstract record Maybe<S>
{
    public static Maybe<S> MakeSome(S someValue)
    {
        return new Some(someValue);
    }

    public static Maybe<S> MakeNone()
    {
        return new None();
    }

    public abstract Maybe<S> ConsumeNone(Action action);
    public abstract Maybe<S> ConsumeSome(Action<S> action);

    sealed record Some(S SomeValue) : Maybe<S>
    {
        public override Maybe<S> ConsumeNone(Action action)
        {
            return this;
        }

        public override Maybe<S> ConsumeSome(Action<S> action)
        {
            action.Invoke(SomeValue);
            return this;
        }
    }

    sealed record None : Maybe<S>
    {
        public override Maybe<S> ConsumeNone(Action action)
        {
            action.Invoke();
            return this;
        }

        public override Maybe<S> ConsumeSome(Action<S> action)
        {
            return this;
        }
    }
}
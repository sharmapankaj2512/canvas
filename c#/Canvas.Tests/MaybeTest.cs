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

public record Maybe<S>
{
    public static Maybe<S> MakeSome(S someValue)
    {
        return new Some(someValue);
    }

    public static Maybe<S> MakeNone()
    {
        return new None();
    }

    sealed record Some(S SomeValue) : Maybe<S>;

    sealed record None : Maybe<S>;

    public Maybe<S> ConsumeNone(Action action)
    {
        action.Invoke();
        return this;
    }

    public Maybe<S> ConsumeSome(Action<S> action)
    {
        return this;
    }
}
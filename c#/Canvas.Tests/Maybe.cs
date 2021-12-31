using System;

namespace Canvas.Tests;

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
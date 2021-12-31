namespace Canvas.Tests;

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
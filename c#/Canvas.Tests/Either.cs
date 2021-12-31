using System;

namespace Canvas.Tests;

public abstract record Either<TL, TR>
{
    public static Either<TL, TR> MakeLeft(TL leftValue)
    {
        return new Left(leftValue);
    }

    public static Either<TL, TR> MakeRight(TR rightValue)
    {
        return new Right(rightValue);
    }

    public abstract Either<TL, TR> ConsumeLeft(Action<TL> leftConsumer);

    public abstract Either<TL, TR> ConsumeRight(Action<TR> rightConsumer);

    sealed record Left(TL _leftValue) : Either<TL, TR>
    {
        private readonly TL _leftValue = _leftValue;

        public override Either<TL, TR> ConsumeLeft(Action<TL> leftConsumer)
        {
            leftConsumer(_leftValue);
            return this;
        }

        public override Either<TL, TR> ConsumeRight(Action<TR> rightConsumer)
        {
            return this;
        }
    }

    sealed record Right(TR _rightValue) : Either<TL, TR>
    {
        private readonly TR _rightValue = _rightValue;

        public override Either<TL, TR> ConsumeLeft(Action<TL> leftConsumer)
        {
            return this;
        }

        public override Either<TL, TR> ConsumeRight(Action<TR> rightConsumer)
        {
            rightConsumer(_rightValue);
            return this;
        }
    }
}
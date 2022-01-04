using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Canvas.Tests;

[TestFixture]
public class CommandSourceTest
{
    [Test]
    public void CreateCanvasCommand()
    {
        var source = new ConsoleCommandSource();

        Assert.AreEqual(true, source.MoveNext());
        Assert.AreEqual(new CreateCanvas(0, 0), source.Current);
    }
}

public class ConsoleCommandSource : ICommandSource
{
    public bool MoveNext()
    {
        return true;
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public ICommand Current => new CreateCanvas(0, 0);

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IEnumerator<CreateCanvas> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
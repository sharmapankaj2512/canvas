using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Canvas.Tests;

[TestFixture]
public class CommandSourceTest
{
    [Test]
    public void CreateCanvasCommand()
    {
        var reader = new StringReader("create 0 0");
        var old = Console.In;
        var source = new ConsoleCommandSource(reader);

        Assert.AreEqual(true, source.MoveNext());
        Assert.AreEqual(new CreateCanvas(0, 0), source.Current);
    }
}

public class ConsoleCommandSource : ICommandSource
{
    private readonly StringReader _reader;

    public ConsoleCommandSource(StringReader reader)
    {
        _reader = reader;
    }

    public bool MoveNext()
    {
        return true;
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public ICommand Current
    {
        get
        {
            var rawCommand = _reader.ReadLine();
            var tokens = rawCommand.Split(" ");
            return new CreateCanvas(int.Parse(tokens[1]), int.Parse(tokens[2]));
        }
    }

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
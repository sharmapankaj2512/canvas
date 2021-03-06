using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Canvas.Tests;

public class ConsoleCommandSource : ICommandSource
{
    private readonly TextReader _reader;

    public ConsoleCommandSource(TextReader reader)
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
            var rawCommand = _reader.ReadLine().Trim();
            return ICommand.MakeCommand(rawCommand);
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        _reader.Dispose();
    }

    public IEnumerator<ICommand> GetEnumerator()
    {
        return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
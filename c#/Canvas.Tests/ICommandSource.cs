using System.Collections;
using System.Collections.Generic;

namespace Canvas.Tests;

public interface ICommandSource : IEnumerator<ICommand>, IEnumerable<CreateCanvas>
{
    bool IEnumerator.MoveNext()
    {
        return true;
    }
}
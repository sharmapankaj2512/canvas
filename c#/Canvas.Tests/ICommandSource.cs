using System.Collections;
using System.Collections.Generic;

namespace Canvas.Tests;

public interface ICommandSource : IEnumerator<CreateCanvas>, IEnumerable<CreateCanvas>
{
    bool IEnumerator.MoveNext()
    {
        return true;
    }
}
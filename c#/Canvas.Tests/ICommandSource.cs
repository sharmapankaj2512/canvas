using System.Collections;
using System.Collections.Generic;

namespace Canvas.Tests;

public interface ICommandSource : IEnumerator<ICommand>, IEnumerable<ICommand> { }
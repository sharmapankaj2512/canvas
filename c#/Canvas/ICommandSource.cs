namespace Canvas;

public interface ICommandSource : IEnumerator<ICommand>, IEnumerable<ICommand> { }
using System.Collections.Generic;

public interface IConsoleService
{
    void Write(string message);
    void RunCommand(string line);
    IEnumerable<IConsoleCommand> Commands { get; }
}

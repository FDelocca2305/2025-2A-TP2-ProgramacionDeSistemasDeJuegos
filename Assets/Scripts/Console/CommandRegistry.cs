using System;
using System.Collections.Generic;

public class CommandRegistry
{
    private readonly Dictionary<string, IConsoleCommand> _commands = new(StringComparer.OrdinalIgnoreCase);

    public void Register(IConsoleCommand command)
    {
        _commands[command.Name] = command;
        foreach (var alias in command.Aliases)
            _commands[alias] = command;
    }

    public bool TryGet(string name, out IConsoleCommand command)
        => _commands.TryGetValue(name, out command);

    public IEnumerable<IConsoleCommand> GetAll() => new HashSet<IConsoleCommand>(_commands.Values);
}

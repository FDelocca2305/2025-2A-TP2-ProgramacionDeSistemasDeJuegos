using System.Linq;

public class HelpCommand : IConsoleCommand
{
    private readonly IConsoleService _console;

    public string Name => "help";
    public string[] Aliases => new[] { "?" };
    public string Description => "help <command>: Shows the command description.";
    
    public HelpCommand(IConsoleService console)
    {
        _console = console;
    }

    public void Execute(string[] args)
    {
        if (args.Length == 0)
        {
            _console.Write("Available Commands:");
            foreach (var cmd in _console.Commands)
                _console.Write($"- <b>{cmd.Name}</b>: {cmd.Description}");
            return;
        }

        var cmdName = args[0];
        var command = _console.Commands.FirstOrDefault(c => c.Name == cmdName || c.Aliases.Contains(cmdName));
        if (command != null)
            _console.Write($"<b>{command.Name}</b> ({string.Join(", ", command.Aliases)}): {command.Description}");
        else
            _console.Write($"Command not found: {cmdName}");
    }
}

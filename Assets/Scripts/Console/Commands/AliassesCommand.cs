using System.Linq;

public class AliassesCommand : IConsoleCommand
{
    private readonly IConsoleService _console;

    public string Name => "aliases";
    public string[] Aliases => new[] { "alias" };
    public string Description => "aliases <command>: Shows a command alias.";

    public AliassesCommand(IConsoleService console)
    {
        _console = console;
    }

    public void Execute(string[] args)
    {
        if (args.Length == 0)
        {
            _console.Write("Use: aliases <command>");
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

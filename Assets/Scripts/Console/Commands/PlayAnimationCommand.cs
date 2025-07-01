using System.Linq;

public class PlayAnimationCommand : IConsoleCommand
{
    private readonly IConsoleService _console;
    private readonly AnimationCommandLibrary  _library;
    
    public string Name => "playanimation";
    public string[] Aliases => new[] { "anim", "playanim" };
    public string Description => "playanimation <animation>: Play an animation in all the characters.";

    public PlayAnimationCommand(IConsoleService console, AnimationCommandLibrary  library)
    {
        _console = console;
        _library = library;
    }

    public void Execute(string[] args)
    {
        if (args.Length == 0)
        {
            _console.Write("Use: playanimation <animation>. Available Animations:");
            foreach (var a in _library.animations)
                _console.Write($"- {a}");
            return;
        }

        var cmdName = args[0];
        var animCommand = _library.animations.FirstOrDefault(c => c.commandName == cmdName);

        if (animCommand == null)
        {
            _console.Write($"Command <b>{cmdName}</b> not found.");
            return;
        }

        foreach (var charAnim in CharacterAnimator.Instances)
        {
            charAnim.ForceAnimation(animCommand, 2f);
        }

        _console.Write($"Animation command <b>{cmdName}</b> executed on all characters.");
    }
}

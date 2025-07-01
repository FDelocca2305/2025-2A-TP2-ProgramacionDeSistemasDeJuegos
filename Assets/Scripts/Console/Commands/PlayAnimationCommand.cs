using System.Linq;
using System.Text;

public class PlayAnimationCommand : IConsoleCommand
{
    private readonly IConsoleService _console;
    private readonly AnimationCommandLibrary  _library;
    
    public string Name => "playanimation";
    public string[] Aliases => new[] { "anim", "playanim" };
    public string Description
    {
        get { return $"playanimation <animation>: Play an animation in all the characters. Available Animations: {GetAvailableAnimations()}"; }
    }

    private string GetAvailableAnimations()
    {
        StringBuilder availableAnimations = new StringBuilder();
        availableAnimations.Append("\n");
        foreach (var a in _library.animations)
        {
            availableAnimations.Append($"- {a.ToString()}");
            availableAnimations.Append("\n");
        }
        return availableAnimations.ToString();
    }

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
            _console.Write(GetAvailableAnimations());
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

using System;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private AnimationCommandLibrary  library;
    
    private void Awake()
    {
        ServiceLocator.Register<ICharacterFactory>(new CharacterFactory());
        
        var console = new ConsoleService();
        ServiceLocator.Register<IConsoleService>(console);
        ServiceLocator.Register<ILogHandler>(console);
        
        console.RegisterCommand(new HelpCommand(console));
        console.RegisterCommand(new AliassesCommand(console));
        console.RegisterCommand(new PlayAnimationCommand(console, library));
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<ICharacterFactory>();
        ServiceLocator.Unregister<IConsoleService>();
        ServiceLocator.Unregister<ILogHandler>();
    }
}

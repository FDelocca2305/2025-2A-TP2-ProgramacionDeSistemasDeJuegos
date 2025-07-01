using UnityEngine;
using UnityEngine.UI;

public class Startup : MonoBehaviour
{
    [SerializeField] private AnimationCommandLibrary library;
    [SerializeField] private Button characterButtonPrefab;
    
    private void Awake()
    {
        var defaultFactory = new SimpleCharacterFactory();
        var abstractFactory = new CharacterAbstractFactory(defaultFactory);
        ServiceLocator.Register<ICharacterAbstractFactory>(abstractFactory);
        
        var buttonAbstractFactory = new MenuAbstractFactory();
        var characterButtonFactory = new CharacterButtonFactory(characterButtonPrefab);
        buttonAbstractFactory.RegisterFactory(characterButtonFactory);
        ServiceLocator.Register<IMenuAbstractFactory>(buttonAbstractFactory);
        
        var console = new ConsoleService();
        ServiceLocator.Register<IConsoleService>(console);
        ServiceLocator.Register<ILogHandler>(console);
        
        console.RegisterCommand(new HelpCommand(console));
        console.RegisterCommand(new AliassesCommand(console));
        console.RegisterCommand(new PlayAnimationCommand(console, library));
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<ICharacterAbstractFactory>();
        ServiceLocator.Unregister<IMenuAbstractFactory>();
        ServiceLocator.Unregister<IConsoleService>();
        ServiceLocator.Unregister<ILogHandler>();
    }
}

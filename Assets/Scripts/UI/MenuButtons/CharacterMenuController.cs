using UnityEngine;

public class CharacterMenuController : MonoBehaviour
{
    [SerializeField] private ButtonMenuConfig menuConfig;
    [SerializeField] private Transform buttonLayout;
    private MenuBuilder<ButtonMenuConfig.ButtonEntry> _menuBuilder;
    private ICharacterSpawner _spawner;

    private void Start()
    {
        _spawner = ServiceLocator.Get<ICharacterSpawner>();
        _menuBuilder = new MenuBuilder<ButtonMenuConfig.ButtonEntry>();
        
        _menuBuilder.BuildMenu(
            menuConfig.buttons,
            buttonLayout,
            entry => entry.buttonTitle,
            entry => _spawner.Spawn(entry.characterConfig)
        );
    }
}

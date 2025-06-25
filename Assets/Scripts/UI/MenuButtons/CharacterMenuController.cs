using UnityEngine;

public class CharacterMenuController : MonoBehaviour
{
    [SerializeField] private CharacterMenuBuilder menuBuilder;
    [SerializeField] private ButtonMenuConfig menuConfig;
    private ICharacterSpawner _spawner;

    private void Start()
    {
        _spawner = ServiceLocator.Get<ICharacterSpawner>();
        _spawner.Setup(ServiceLocator.Get<ICharacterFactory>());

        menuBuilder.BuildMenu(
            menuConfig.buttons,
            entry => entry.buttonTitle,
            entry => _spawner.Spawn(entry.characterConfig)
        );
    }
}

using UnityEngine;

public class CharacterMenuController : MonoBehaviour
{
    [SerializeField] private CharacterMenuBuilder menuBuilder;
    [SerializeField] private ButtonMenuConfig menuConfig;
    private CharacterSpawner _spawner;

    private void Start()
    {
        _spawner = FindFirstObjectByType<CharacterSpawner>();
        _spawner.Setup(new CharacterFactory());

        menuBuilder.BuildMenu(
            menuConfig.buttons,
            entry => entry.buttonTitle,
            entry => _spawner.Spawn(entry.characterConfig)
        );
    }
}

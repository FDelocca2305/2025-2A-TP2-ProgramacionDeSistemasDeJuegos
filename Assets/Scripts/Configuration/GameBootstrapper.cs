using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    private void Awake()
    {
        ServiceLocator.Register<ICharacterFactory>(new CharacterFactory());
    }
}

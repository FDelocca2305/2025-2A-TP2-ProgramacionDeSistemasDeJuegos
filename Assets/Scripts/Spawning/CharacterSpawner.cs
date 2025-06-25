using UnityEngine;

public class CharacterSpawner : MonoBehaviour, ICharacterSpawner
{
    private ICharacterFactory _factory;
    
    private void Awake()
    {
        ServiceLocator.Register<ICharacterSpawner>(this);
    }
    
    public void Setup(ICharacterFactory factory)
    {
        _factory = factory;
    }

    public void Spawn(CharacterConfig config)
    {
        if (_factory == null)
        {
            Debug.LogError("CharacterSpawner: No Factory Set");
            return;
        }
        _factory.CreateCharacter(config, transform.position, transform.rotation);
    }
}

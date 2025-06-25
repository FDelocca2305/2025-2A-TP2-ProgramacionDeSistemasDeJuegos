using UnityEngine;

public class CharacterSpawner : MonoBehaviour, ISetupSpawner
{
    private ICharacterFactory _factory;
    
    public void Setup(ICharacterFactory factory)
    {
        _factory = factory;
    }

    public void Spawn(CharacterConfig config)
    {
        if (_factory == null)
        {
            Debug.LogError("CharacterSpawner: Factory not set!");
            return;
        }
        _factory.CreateCharacter(config, transform.position, transform.rotation);
    }
}

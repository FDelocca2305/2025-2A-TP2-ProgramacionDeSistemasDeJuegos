using UnityEngine;

public class CharacterSpawner : MonoBehaviour
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
            Debug.LogError("CharacterSpawner: No Factory Set");
            return;
        }
        _factory.CreateCharacter(config, transform.position, transform.rotation);
    }
}

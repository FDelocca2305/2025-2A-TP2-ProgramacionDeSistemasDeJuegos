using UnityEngine;

public class CharacterSpawner : MonoBehaviour, ICharacterSpawner
{
    private ICharacterAbstractFactory _factory;
    
    private void Awake()
    {
        ServiceLocator.Register<ICharacterSpawner>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<ICharacterSpawner>();
    }

    public void Setup(ICharacterAbstractFactory factory)
    {
        _factory = factory;
    }
    
    public void Spawn(CharacterConfig config)
    {
        var abstractFactory = ServiceLocator.Get<ICharacterAbstractFactory>();
        var factory = abstractFactory.GetFactory(config);
        factory.CreateCharacter(config, transform.position, transform.rotation);
    }
}

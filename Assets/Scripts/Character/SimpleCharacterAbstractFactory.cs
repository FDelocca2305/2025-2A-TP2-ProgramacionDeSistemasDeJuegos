public class SimpleCharacterAbstractFactory : ICharacterAbstractFactory
{
    private readonly ICharacterFactory _defaultFactory;
    public SimpleCharacterAbstractFactory(ICharacterFactory defaultFactory) 
    {
        _defaultFactory = defaultFactory;
    }

    public ICharacterFactory GetFactory(CharacterConfig config)
        => _defaultFactory;
}

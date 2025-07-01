public interface ICharacterAbstractFactory
{
    ICharacterFactory GetFactory(CharacterConfig config);
}

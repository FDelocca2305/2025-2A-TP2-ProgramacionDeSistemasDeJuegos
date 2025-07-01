public interface ICharacterSpawner
{
    void Setup(ICharacterAbstractFactory factory);
    void Spawn(CharacterConfig config);
}

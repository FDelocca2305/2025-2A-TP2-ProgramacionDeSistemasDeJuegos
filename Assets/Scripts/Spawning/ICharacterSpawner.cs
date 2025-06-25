public interface ICharacterSpawner
{
    void Setup(ICharacterFactory factory);
    void Spawn(CharacterConfig config);
}

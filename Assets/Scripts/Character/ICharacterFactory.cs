using UnityEngine;

public interface ICharacterFactory
{
    GameObject CreateCharacter(CharacterConfig config, Vector3 position, Quaternion rotation);
}

using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Scriptable Objects/CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
    public Character prefab;
    public CharacterModel characterModel;
    public PlayerControllerModel controllerModel;
    public RuntimeAnimatorController animatorController;
}

using UnityEngine;

[CreateAssetMenu(fileName = "ButtonMenuConfig", menuName = "Scriptable Objects/ButtonMenuConfig")]
public class ButtonMenuConfig : ScriptableObject
{
    public ButtonEntry[] buttons;

    [System.Serializable]
    public class ButtonEntry
    {
        public string buttonTitle;
        public CharacterConfig characterConfig;
    }
}

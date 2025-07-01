using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenericButtonFactory<T> : MonoBehaviour, IButtonFactory<T>
{
    [SerializeField] private Button buttonPrefab;
    private Transform _parent;

    public void Setup(Transform parent)
    {
        _parent = parent;
    }

    public virtual Button CreateButton(T entry, Action<T> onClick)
    {
        var buttonInstance = Instantiate(buttonPrefab, _parent);
        var text = buttonInstance.GetComponentInChildren<TextMeshProUGUI>();
        text.text = entry.ToString();

        buttonInstance.onClick.AddListener(() => onClick?.Invoke(entry));
        return buttonInstance;
    }
}

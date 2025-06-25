using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuBuilder<T> : MonoBehaviour
{
    [SerializeField] private Transform buttonLayout;
    [SerializeField] private Button buttonPrefab;
    
    public void BuildMenu(IEnumerable<T> entries, Func<T, string> getTitle, Action<T> onClick)
    {
        foreach (Transform child in buttonLayout)
            Destroy(child.gameObject);

        foreach (var entry in entries)
        {
            var buttonInstance = Instantiate(buttonPrefab, buttonLayout);
            buttonInstance.GetComponentInChildren<TextMeshProUGUI>().text = getTitle(entry);
            buttonInstance.onClick.AddListener(() => onClick?.Invoke(entry));
        }
    }
}

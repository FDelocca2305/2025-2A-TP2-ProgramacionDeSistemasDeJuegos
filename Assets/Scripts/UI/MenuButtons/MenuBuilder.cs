using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuBuilder<T> : MonoBehaviour
{
    [SerializeField] private Transform buttonLayout;

    public void BuildMenu(IEnumerable<T> entries, Func<T, string> getTitle, Action<T> onClick)
    {
        foreach (Transform child in buttonLayout)
            Destroy(child.gameObject);

        var abstractFactory = ServiceLocator.Get<IMenuAbstractFactory>();
        var factory = abstractFactory.GetFactory<T>();
        factory.Setup(buttonLayout);

        foreach (var entry in entries)
        {
            var button = factory.CreateButton(entry, onClick);
            button.GetComponentInChildren<TextMeshProUGUI>().text = getTitle(entry);
        }
    }
}

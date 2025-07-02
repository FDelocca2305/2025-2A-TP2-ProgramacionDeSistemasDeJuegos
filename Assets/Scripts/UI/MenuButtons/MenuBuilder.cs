using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuBuilder<T>
{
    public void BuildMenu(IEnumerable<T> entries, Transform buttonLayout, Func<T, string> getTitle, Action<T> onClick)
    {
        foreach (Transform child in buttonLayout)
            UnityEngine.Object.Destroy(child.gameObject);

        var abstractFactory = ServiceLocator.Get<IButtonAbstractFactory>();
        var factory = abstractFactory.GetFactory<T>();
        factory.Setup(buttonLayout);

        foreach (var entry in entries)
        {
            var button = factory.CreateButton(entry, onClick);
            button.GetComponentInChildren<TextMeshProUGUI>().text = getTitle(entry);
        }
    }
}

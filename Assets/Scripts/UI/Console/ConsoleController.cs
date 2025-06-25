using UnityEngine;
using UnityEngine.UI;

public class ConsoleController : MonoBehaviour
{
    [SerializeField] private GameObject consolePanel;
    [SerializeField] private Button toggleButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private KeyCode toggleKey = KeyCode.F1;

    private void Awake()
    {
        if (toggleButton != null)
            toggleButton.onClick.AddListener(ToggleConsole);
        if (closeButton != null)
            closeButton.onClick.AddListener(ToggleConsole);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
            ToggleConsole();
    }

    private void ToggleConsole()
    {
        if (consolePanel != null)
            consolePanel.SetActive(!consolePanel.activeSelf);
    }
}

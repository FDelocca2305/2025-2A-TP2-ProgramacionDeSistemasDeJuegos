using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ConsoleController : MonoBehaviour, IConsoleUI
{
    [SerializeField] private GameObject consolePanel;
    [SerializeField] private Button toggleButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TMP_Text outputText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button sendButton;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private InputActionReference toggleConsoleAction;
    
    private IConsoleService _consoleService;
    
    private void Awake()
    {
        ServiceLocator.Register<IConsoleUI>(this);
        
        if (toggleButton != null)
            toggleButton.onClick.AddListener(ToggleConsole);
        if (closeButton != null)
            closeButton.onClick.AddListener(ToggleConsole);
        if (sendButton != null)
            sendButton.onClick.AddListener(RunCommand);
        
        inputField.onSubmit.AddListener(OnInputSubmit);
        
        _consoleService = ServiceLocator.Get<IConsoleService>();
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<IConsoleUI>();
    }

    private void OnEnable()
    {
        if (toggleConsoleAction != null)
        {
            toggleConsoleAction.action.Enable();
            toggleConsoleAction.action.performed += OnToggleConsoleAction;
        }
    }

    private void OnDisable()
    {
        if (toggleConsoleAction != null)
        {
            toggleConsoleAction.action.performed -= OnToggleConsoleAction;
            toggleConsoleAction.action.Disable();
        }
    }

    private void OnToggleConsoleAction(InputAction.CallbackContext ctx)
    {
        ToggleConsole();
    }
    
    private void OnInputSubmit(string command)
    {
        RunCommand();
    }

    private void ToggleConsole()
    {
        if (consolePanel != null)
        {
            bool isActive = !consolePanel.activeSelf;
            consolePanel.SetActive(isActive);
            if (isActive && inputField != null)
            {
                inputField.ActivateInputField();
            }
        }
    }

    private void RunCommand()
    {
        if (inputField == null || string.IsNullOrWhiteSpace(inputField.text))
            return;

        var commandText = inputField.text;
        inputField.text = "";
        inputField.ActivateInputField();

        _consoleService.RunCommand(commandText);
    }

    public void UpdateLog(Queue<string> history)
    {
        outputText.text = string.Join("\n", history);
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}

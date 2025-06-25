using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleController : MonoBehaviour, IConsoleUI
{
    [SerializeField] private GameObject consolePanel;
    [SerializeField] private Button toggleButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private KeyCode toggleKey = KeyCode.F1;
    [SerializeField] private TMP_Text outputText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button sendButton;
    [SerializeField] private ScrollRect scrollRect;
    
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

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
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
    
    public void ClearInput()
    {
        if (inputField != null)
            inputField.text = "";
    }
    
    public void UpdateLog(Queue<string> history)
    {
        outputText.text = string.Join("\n", history);
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}

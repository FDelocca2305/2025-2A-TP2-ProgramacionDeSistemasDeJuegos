using System.Collections.Generic;

public interface IConsoleUI
{
    void UpdateLog(Queue<string> _history);
    void ClearInput();
}

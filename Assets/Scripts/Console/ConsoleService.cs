using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ConsoleService : IConsoleService, ILogHandler
{
    private readonly CommandRegistry _registry = new();
    private readonly Queue<string> _history = new();
    private ILogHandler _unityLogHandler;
    public IEnumerable<IConsoleCommand> Commands => _registry.GetAll();
    
    public ConsoleService()
    {
        _unityLogHandler = Debug.unityLogger.logHandler;
        Debug.unityLogger.logHandler = this;
    }
    
    public void RegisterCommand(IConsoleCommand command) => _registry.Register(command);
    
    public void Write(string message)
    {
        _history.Enqueue(message);
        while (_history.Count > 100)
            _history.Dequeue();
        
        if (ServiceLocator.TryGet<IConsoleUI>(out var ui) && ui != null)
            ui.UpdateLog(_history);
    }

    public void RunCommand(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            return;
        
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            return;
        
        var cmdName = parts[0];
        var args = parts.Length > 1 ? parts[1..] : Array.Empty<string>();
        if (_registry.TryGet(cmdName, out var command))
        {
            try
            {
                command.Execute(args);
            }
            catch (Exception e)
            {
                Write($"<color=red>Error: {e.Message}</color>");
            }
        }
        else
        {
            Write($"Unknown Command: <b>{cmdName}</b>");
        }
    }
    
    public void LogFormat(LogType logType, Object context, string format, params object[] args)
    {
        var log = string.Format(format, args);
        Write($"[{logType}] {log}");
        _unityLogHandler.LogFormat(logType, context, format, args);
    }

    public void LogException(Exception exception, Object context)
    {
        Write($"<color=red>Exception:</color> {exception.Message}\n{exception.StackTrace}");
        _unityLogHandler.LogException(exception, context);
    }
}

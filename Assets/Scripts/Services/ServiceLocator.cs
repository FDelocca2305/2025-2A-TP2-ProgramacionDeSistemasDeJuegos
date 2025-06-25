using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = new();

    public static void Register<T>(T service)
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
            _services[type] = service;
        else
            _services.Add(type, service);
    }

    public static void Unregister<T>()
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
            _services.Remove(type);
    }

    public static T Get<T>()
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var service))
            return (T)service;
        throw new Exception($"Service {type.Name} not registered in ServiceLocator!");
    }

    public static bool TryGet<T>(out T service)
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var obj))
        {
            service = (T)obj;
            return true;
        }
        service = default;
        return false;
    }

    public static void Clear()
    {
        _services.Clear();
    }
}

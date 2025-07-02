using System;
using System.Collections.Generic;

public class ButtonAbstractFactory : IButtonAbstractFactory
{
    private readonly Dictionary<Type, object> _factories = new();

    public void RegisterFactory<T>(IButtonFactory<T> factory)
    {
        _factories[typeof(T)] = factory;
    }

    public IButtonFactory<T> GetFactory<T>()
    {
        if (_factories.TryGetValue(typeof(T), out var factory))
            return factory as IButtonFactory<T>;
        throw new Exception($"No button factory registered for {typeof(T).Name}");
    }
}

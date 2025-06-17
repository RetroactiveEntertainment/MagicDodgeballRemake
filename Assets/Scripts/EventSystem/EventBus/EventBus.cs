using System.Collections.Generic;
using EventSystem.EventBus;
using UnityEngine;

public static class EventBus<T> where T : IEvent
{
    static readonly HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();

    public static void Register(EventBinding<T> eventBinding) => bindings.Add(eventBinding);
    public static void Deregister(EventBinding<T> eventBinding) => bindings.Remove(eventBinding);

    public static void Raise(T @event)
    {
        foreach (var binding in bindings)
        {
            binding.OnEvent.Invoke(@event);
            binding.OnEventNoArgs.Invoke();
        }
    }
}
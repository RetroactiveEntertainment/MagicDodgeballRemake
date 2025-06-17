using System;
using EventSystem.EventBus;
using Unity.VisualScripting;
using UnityEngine;

public class EventBinding<T> : IEventBinding<T> where T : IEvent
{
    // Initialize these with delegates to not bother if null checks
    private Action<T> onEvent = _ => { };
    private Action onEventNoArgs = () => { };

    public EventBinding(Action<T> onEvent)
    {
        this.onEvent = onEvent;
    }

    public EventBinding(Action onEventNoArgs)
    {
        this.onEventNoArgs = onEventNoArgs;
    }

    Action<T> IEventBinding<T>.OnEvent
    {
        get => onEvent;
        set => onEvent = value;
    }

    Action IEventBinding<T>.OnEventNoArgs
    {
        get => onEventNoArgs;
        set => onEventNoArgs = value;
    }

    public void Add(Action<T> onEvent)
    {
        this.onEvent += onEvent;
    }

    public void Remove(Action<T> onEvent)
    {
        this.onEvent -= onEvent;
    }

    public void Add(Action onEventNoArgs)
    {
        this.onEventNoArgs += onEventNoArgs;
    }

    public void Remove(Action onEventNoArgs)
    {
        this.onEventNoArgs -= onEventNoArgs;
    }
}
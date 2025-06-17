using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    //[CreateAssetMenu(fileName = "EventChannel", menuName = "Scriptable Objects/EventChannel")]
    public abstract class EventChannel<T> : ScriptableObject
    {
        private readonly HashSet<EventListener<T>> _observers = new();

        public void Invoke(T value)
        {
            foreach (var observer in _observers)
            {
                observer.Raise(value);
            }
        }

        public void Register(EventListener<T> observer) => _observers.Add(observer);

        public void DeRegister(EventListener<T> observer) => _observers.Remove(observer);
    }

    // Make its own class in a separate file

    public readonly struct Empty
    {
    }

    [CreateAssetMenu(fileName = "EventChannel", menuName = "Scriptable Objects/Events/Event Channel Empty")]
    public class EventChannel : EventChannel<Empty>
    {
    }
}
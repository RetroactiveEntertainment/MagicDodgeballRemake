using EventSystem;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "EventChannel", menuName = "Scriptable Objects/Events/Event Channel Int")]
    public class IntEventChannel : EventChannel<int>
    {
    }
}
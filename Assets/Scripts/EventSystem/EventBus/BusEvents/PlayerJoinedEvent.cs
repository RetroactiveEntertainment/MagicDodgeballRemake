using EventSystem.EventBus;
using UnityEngine;

public struct PlayerJoinedEvent : IEvent
{
    public int PlayerIndex;
}

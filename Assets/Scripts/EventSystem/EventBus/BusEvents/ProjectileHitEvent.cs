using EventSystem.EventBus;

public struct ProjectileHitEvent : IEvent
{
    public int ProjectileSpawnedByPlayerIndex;
    public int PlayerIndex;
}
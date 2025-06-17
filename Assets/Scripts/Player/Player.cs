using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private static int _lastJoinedPlayerIndex = -1;
    [SerializeField] private int playerIndex;


    private void Start()
    {
        if (playerIndex > _lastJoinedPlayerIndex)
            _lastJoinedPlayerIndex = playerIndex;

        if (playerIndex == -1)
        {
            playerIndex = _lastJoinedPlayerIndex + 1;
            _lastJoinedPlayerIndex = playerIndex;
        }

        EventBus<PlayerJoinedEvent>.Raise(new PlayerJoinedEvent { PlayerIndex = playerIndex });
        Debug.Log($"Last player joined is: {_lastJoinedPlayerIndex}.");
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.TryGetComponent<Projectile>(out Projectile hitByProjectile))
            return;

        EventBus<ProjectileHitEvent>.Raise(new ProjectileHitEvent
            { ProjectileSpawnedByPlayerIndex = hitByProjectile.SpawnedByPlayerIndex, PlayerIndex = playerIndex });
    }
}
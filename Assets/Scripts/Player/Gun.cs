using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private ProjectileData modifiedProjectileData;
    private ProjectileSpawner _projectileSpawner = new ProjectileSpawner(3);

    private void Start()
    {
        _projectileSpawner.SetPlayerIndex(playerIndex);
    }

    public void OnShoot(InputValue value)
    {
        _projectileSpawner.SpawnNewProjectile(projectilePrefab, modifiedProjectileData, projectileSpawnPoint);
    }

    public void SetMaxAllowedAliveProjectiles(int maxAllowedAliveProjectiles)
    {
        _projectileSpawner.SetMaxAllowedAliveProjectiles(maxAllowedAliveProjectiles);
    }
}
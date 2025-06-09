using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private ProjectileData modifiedProjectileData;
    private ProjectileSpawner _projectileSpawner = new ProjectileSpawner();

    public void OnShoot(InputValue value)
    {
        _projectileSpawner.SpawnNewProjectile(projectilePrefab, modifiedProjectileData, projectileSpawnPoint);
    }

    public void SetMaxAllowedAliveProjectiles(int maxAllowedAliveProjectiles)
    {
        _projectileSpawner.SetMaxAllowedAliveProjectiles(maxAllowedAliveProjectiles);
    }
}
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner
{
    private int _maxAllowedAliveProjectiles;

    private List<Projectile> _projectiles = new List<Projectile>();

    public ProjectileSpawner()
    {
        _maxAllowedAliveProjectiles = 3;
    }

    public ProjectileSpawner(int maxAllowedAliveProjectiles)
    {
        _maxAllowedAliveProjectiles = maxAllowedAliveProjectiles;
    }

    public void SpawnNewProjectile(GameObject projectilePrefab, ProjectileData projectileData,
        Transform projectileSpawnPoint)
    {
        if (!CanSpawnNewProjectile())
            return;

        GameObject projectileGameObject = GameObject.Instantiate(projectilePrefab, projectileSpawnPoint.position,
            projectileSpawnPoint.rotation);
        Projectile projectile = projectileGameObject.GetComponent<Projectile>();
        projectile.SetProjectileData(projectileData);
        projectile.OnProjectileDeath += RemoveProjectileFromAliveList;
        _projectiles.Add(projectile);
        Debug.Log($"Projectile count: {_projectiles.Count}");
    }

    private bool CanSpawnNewProjectile()
    {
        return _projectiles.Count < _maxAllowedAliveProjectiles;
    }

    private void RemoveProjectileFromAliveList(Projectile projectile)
    {
        // Check if it exists, edge case!
        _projectiles.Remove(projectile);
    }

    public int GetMaxAllowedAliveProjectiles()
    {
        return _maxAllowedAliveProjectiles;
    }

    public void SetMaxAllowedAliveProjectiles(int maxAllowedAliveProjectiles)
    {
        _maxAllowedAliveProjectiles = maxAllowedAliveProjectiles;
    }
}
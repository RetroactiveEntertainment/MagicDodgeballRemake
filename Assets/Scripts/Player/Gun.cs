using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private ProjectileData modifiedProjectileData;

    private GameObject _cachedProjectile = null;
    //private ProjectileSpawner _projectileSpawner = new ProjectileSpawner(3);

    private int _maxAllowedAliveProjectiles = 3;
    private List<Projectile> _projectiles = new List<Projectile>();

    private void Start()
    {
        CreateCachedProjectile();
    }


    // Creates a projectile that can be used to implement buffs in. All projectiles are based off this cached version.
    private void CreateCachedProjectile()
    {
        _cachedProjectile = Instantiate(projectilePrefab);
        _cachedProjectile.SetActive(false);
    }

    public void OnShoot(InputValue value)
    {
        SpawnNewProjectile(_cachedProjectile, modifiedProjectileData);
    }

    private bool CanSpawnNewProjectile()
    {
        return _projectiles.Count < _maxAllowedAliveProjectiles;
    }

    private void RemoveProjectileFromAliveList(Projectile projectile)
    {
        // Check if it exists, edge case!
        if (_projectiles.Contains(projectile))
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

    private void SpawnNewProjectile(GameObject projectileToSpawn, ProjectileData projectileData)
    {
        if (!CanSpawnNewProjectile())
            return;

        GameObject projectileGameObject = Instantiate(projectileToSpawn, projectileSpawnPoint.position,
            projectileSpawnPoint.rotation);
        Projectile projectile = projectileGameObject.GetComponent<Projectile>();
        projectile.SetProjectileData(projectileData);
        projectile.OnProjectileDeath += RemoveProjectileFromAliveList;
        _projectiles.Add(projectile);
        projectile.SpawnedByPlayerIndex = playerIndex;

        projectile.gameObject.SetActive(true);
        //Debug.Log($"Projectile count: {_projectiles.Count}");
    }
}
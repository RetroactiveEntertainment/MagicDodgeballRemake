using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileData projectileData;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider coll;
    private Vector3 _lastLinearVelocity; // Necessary to calculate reflection 
    private int _currentBounceCount = 0;

    public Action<Projectile> OnProjectileDeath;

    private void Start()
    {
        InitializeValues();
    }

    private void Update()
    {
        // Keep at constant speed while retaining direction
        rb.linearVelocity = projectileData.speed * rb.linearVelocity.normalized;
        _lastLinearVelocity = rb.linearVelocity;
    }

    public void SetProjectileData(ProjectileData targetProjectileData)
    {
        projectileData = new ProjectileData(targetProjectileData);
        InitializeValues();
    }

    private void OnCollisionEnter(Collision other)
    {
        _currentBounceCount++;
        if (_currentBounceCount >= projectileData.maxBounceCount)
            Destroy(this.gameObject);

        rb.linearVelocity = projectileData.speed *
                            Vector3.Reflect(_lastLinearVelocity.normalized, other.contacts[0].normal);
    }

    private void OnCollisionExit(Collision other)
    {
        // Deal damage
    }

    private void InitializeValues()
    {
        rb.linearVelocity = transform.forward * projectileData.speed;
    }

    public void OnDestroy()
    {
        OnProjectileDeath?.Invoke(this);
    }
}
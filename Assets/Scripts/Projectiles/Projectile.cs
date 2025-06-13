using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public const int PROJECTILE_IN_FOREIGN_AREA_LAYER = 9;
    public const string BARRIER_TAG = "Barrier";

    [SerializeField] private ProjectileData projectileData;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider coll;
    private int _currentBounceCount = 0;
    private Vector3 _lastLinearVelocity; // Necessary to calculate reflection 


    public Action<Projectile> OnProjectileDeath;

    private void Start()
    {
        InitializeValues();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = projectileData.speed * rb.linearVelocity.normalized;
        _lastLinearVelocity = rb.linearVelocity;
    }

    public void OnDestroy()
    {
        OnProjectileDeath?.Invoke(this);
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


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(BARRIER_TAG))
        {
            gameObject.layer = PROJECTILE_IN_FOREIGN_AREA_LAYER;
        }
    }

    public void SetProjectileData(ProjectileData targetProjectileData)
    {
        projectileData = new ProjectileData(targetProjectileData);
        InitializeValues();
    }

    private void InitializeValues()
    {
        rb.linearVelocity = transform.forward * projectileData.speed;
    }
}
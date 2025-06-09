using System;
using UnityEngine;

[Serializable]
public class ProjectileData
{
    public float speed;
    public int maxBounceCount;

    public ProjectileData()
    {
        speed = 6f;
        maxBounceCount = 4;
    }

    public ProjectileData(float speed, int maxBounceCount)
    {
        this.speed = speed;
        this.maxBounceCount = maxBounceCount;
    }


    public ProjectileData(ProjectileData projectileData)
    {
        speed = projectileData.speed;
        maxBounceCount = projectileData.maxBounceCount;
    }
}
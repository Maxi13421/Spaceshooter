


using System;
using UnityEngine;

public abstract class Turret : Enemy
{
    
    public ObjectPool ProjectilePool;
    public float shootingFrequency;
    protected float LastShot;
    protected Vector3[] WeaponPositions;

    protected virtual void Start()
    {
        ProjectilePool = Level.TurretShrapnelProjectilePool;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Shoot();
        
    }

    protected void Shoot()
    {
        if (Time.time - LastShot >= shootingFrequency)
        {
            LastShot = Time.time;
            Vector3[] weaponPositions = new[] { transform.GetChild(0).position-transform.position, transform.GetChild(1).position - transform.position};
            foreach (var weaponPosition in weaponPositions)
            {
                GameObject o = ProjectilePool.GetPooledObject();
                o.transform.position = transform.position + weaponPosition;
                o.transform.right = -transform.up;
            }
        }
    }
}

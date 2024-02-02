


using System;
using UnityEngine;

public abstract class Turret : Enemy
{
    
    public Projectile.ProjectileType projectileType;
    public float shootingFrequency;
    protected float LastShot;
    protected Vector3[] WeaponPositions;
    protected bool CanShoot = false;

    protected virtual void Start()
    {
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Shoot();
        
    }
    

    protected void Shoot()
    {
        
        if (Time.time - LastShot >= shootingFrequency && visible)
        {
            LastShot = Time.time;
            Vector3[] weaponPositions = null;
            switch (projectileType)
            {
                case Projectile.ProjectileType.Homing or Projectile.ProjectileType.Standard:
                    AudioManager.instance.PlayLevelOneShot(FMODEvents.instance.fireSmall,transform.position);
                    weaponPositions = new[] { transform.GetChild(0).position-transform.position, transform.GetChild(1).position - transform.position};
                    Debug.Log(weaponPositions[0].ToString());
                    break;
                case Projectile.ProjectileType.Shrapnel or Projectile.ProjectileType.Big:
                    AudioManager.instance.PlayLevelOneShot(FMODEvents.instance.fireBig,transform.position);
                    weaponPositions = new[] { transform.up*1.8f };
                    break;
            }
            
            foreach (var weaponPosition in weaponPositions)
            {
                GameObject o = GetProjectile();
                o.transform.position = transform.position + weaponPosition;
                o.transform.right = -transform.up;
                o.transform.parent = transform.parent.parent;
                o.GetComponent<EnemyProjectile>().LifeStart = Time.time;
            }
        }
    }


    

    protected GameObject GetProjectile()
    {
        switch (projectileType)
        {
            case Projectile.ProjectileType.Standard:
                return Level.TurretStandardProjectilePool.GetPooledObject();
            case Projectile.ProjectileType.Homing:
                return Level.TurretHomingProjectilePool.GetPooledObject();
            case Projectile.ProjectileType.Shrapnel:
                return Level.TurretShrapnelProjectilePool.GetPooledObject();
            case Projectile.ProjectileType.Big:
                return Level.TurretBigProjectilePool.GetPooledObject();
        }

        return null;
    }
}

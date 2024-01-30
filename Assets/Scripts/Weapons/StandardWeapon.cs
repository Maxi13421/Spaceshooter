using System;
using System.Collections.Generic;
using UnityEngine;

    
public class StandardWeapon : Weapon
{
    public Vector3[] WeaponPositions;
    
    
    
    private float _lastShot = float.MinValue;


    private void Awake()
    {
        WeaponPositions = new[]
            { transform.parent.GetChild(0).position-transform.parent.position, transform.parent.GetChild(1).position-transform.parent.position };
    }
    
    
    public override void Shoot()
    {
        AudioManager.instance.PlayLevelOneShot(FMODEvents.instance.fireSmall,transform.position);
        if (Time.time - _lastShot > _cooldownMicro)
        {
            _lastShot = Time.time;
            foreach (var weaponPosition in WeaponPositions)
            {
                GameObject o = Level.HomingProjectilePool.GetPooledObject();
                o.transform.position = transform.parent.position + weaponPosition;
                o.transform.right = Vector3.right;
            }
        }
    }

    public override void StopShooting()
    {
        
    }
}
    
    

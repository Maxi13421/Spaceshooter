using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    public float shootingFrequency;
    protected float lastShot;
    protected Vector3[] WeaponPositions;

    protected override void Awake()
    {
        base.Awake();
        WeaponPositions = new[] { transform.GetChild(0).position-transform.position, transform.GetChild(1).position-transform.position};
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Time.time - lastShot >= shootingFrequency)
        {
            lastShot = Time.time;
            foreach (var weaponPosition in WeaponPositions)
            {
                GameObject o = Level.TurretStandardProjectilePool.GetPooledObject();
                o.transform.position = transform.position + weaponPosition;
            }
        }
    }
    
}

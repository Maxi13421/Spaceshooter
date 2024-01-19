using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingTurret : Enemy
{
    public float shootingFrequency;
    protected float lastShot;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        //transform. ursprüngliches vorne im Sprite
        transform.up = (Player.transform.position - transform.position);
        if (Time.time - lastShot >= shootingFrequency)
        {
            lastShot = Time.time;
            Vector3[] weaponPositions = new[] { transform.GetChild(0).position-transform.position, transform.GetChild(1).position - transform.position};
            foreach (var weaponPosition in weaponPositions)
            {
                GameObject o = Level.TurretDirectionalProjectilePool.GetPooledObject();
                o.transform.position = transform.position + weaponPosition;
                o.GetComponent<TurretDirectionalProtectile>().direction =
                    Player.transform.position - transform.position;
            }
        }
    }
}

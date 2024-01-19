using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDirectionalProtectile : EnemyProjectile
{

    public Vector3 direction;
    
    protected override void UpdatePosition()
    {
        float amtToMove = - projectileSpeed * Time.fixedDeltaTime;
        transform.Translate(-direction * amtToMove, Space.World);
    }
}

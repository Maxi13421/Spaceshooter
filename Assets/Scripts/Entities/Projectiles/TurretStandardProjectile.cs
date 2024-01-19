using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStandardProjectile : EnemyProjectile
{
    
    protected override void UpdatePosition()
    {
        float amtToMove = - projectileSpeed * Time.fixedDeltaTime;
        transform.Translate(Vector3.right * amtToMove, Space.World);
    }
    
    
}

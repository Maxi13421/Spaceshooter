using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StandardProjectile : PlayerProjectile
{
    
    
    void Start()
    {
    }

    

    
    

    protected override void UpdatePosition()
    {
        float amtToMove = projectileSpeed * Time.fixedDeltaTime;
        transform.Translate(Vector3.right * amtToMove, Space.Self);
    }
    
    
}

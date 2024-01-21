using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingTurret : Turret
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        //transform. ursprüngliches vorne im Sprite
        transform.up = (Player.transform.position - transform.position);
        
    }
}

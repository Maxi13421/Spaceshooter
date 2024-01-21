using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTurret : Turret
{
    

    protected override void Awake()
    {
        base.Awake();
        WeaponPositions = new[] { transform.GetChild(0).position-transform.position, transform.GetChild(1).position-transform.position};
    }

    
    
}

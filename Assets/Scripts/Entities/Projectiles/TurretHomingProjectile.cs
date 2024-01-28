using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHomingProjectile : EnemyProjectile
{
    
    public float ThresholdAngle;
    public float TurningAnglePerSecond;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.right.ToString());
    }

    protected override void UpdatePosition()
    {




        if ((Math.Abs(transform.rotation.eulerAngles.z -
                      Vector3.Angle(GameSystem.Player.transform.position - transform.position, Vector3.left)) <=
             ThresholdAngle
             || Math.Abs(360 - transform.rotation.eulerAngles.z +
                         Vector3.Angle(GameSystem.Player.transform.position - transform.position, Vector3.left)) <=
             ThresholdAngle))
        {

            transform.right = -Vector3.RotateTowards(-transform.right, GameSystem.Player.transform.position - transform.position,
                (float)(TurningAnglePerSecond * Time.fixedDeltaTime/180*Math.PI), 0);
            
        }
        float amtToMove = projectileSpeed * Time.fixedDeltaTime;
        transform.Translate(Vector3.left * amtToMove, Space.Self);
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HomingProjectile : PlayerProjectile
{

    public float ThresholdAngle;
    public float TurningAnglePerSecond;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void UpdatePosition()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(new Vector2(0.5f, 0),
            new Vector2((GameSystem.MainCamera.orthographicSize * GameSystem.MainCamera.aspect) * 2 + 1,
                GameSystem.MainCamera.orthographicSize * 2), 0);

        Collider2D[] filteredCollider2Ds = Array.FindAll<Collider2D>(collider2Ds, collider2D1 => 
            collider2D1.gameObject.GetComponent<Enemy>() != null 
            && (Math.Abs(transform.rotation.eulerAngles.z-Vector3.Angle(collider2D1.transform.position-transform.position,Vector3.right)) <= ThresholdAngle
            || Math.Abs(360-transform.rotation.eulerAngles.z+Vector3.Angle(collider2D1.transform.position-transform.position,Vector3.right)) <= ThresholdAngle));
        
        if (filteredCollider2Ds.Length != 0)
        {
            
            Collider2D closest = filteredCollider2Ds[0];
            float closestLength = Vector3.Distance(closest.transform.position, transform.position);
            for (int aaa = 1; aaa < filteredCollider2Ds.Length; aaa++)
            {
                if (Vector3.Distance(filteredCollider2Ds[aaa].transform.position, transform.position) < closestLength)
                {
                    closest = filteredCollider2Ds[aaa];
                    closestLength = Vector3.Distance(closest.transform.position, transform.position);
                }
            }
            float f1 = (transform.rotation.eulerAngles.z-Vector3.Angle(closest.transform.position-transform.position,Vector3.right));
            float f2 = (360-transform.rotation.eulerAngles.z+Vector3.Angle(closest.transform.position-transform.position,Vector3.right));

            transform.right = Vector3.RotateTowards(transform.right, closest.transform.position - transform.position,
                (float)(TurningAnglePerSecond * Time.fixedDeltaTime/360*2*Math.PI), 0);
            
        }
        float amtToMove = projectileSpeed * Time.fixedDeltaTime;
        transform.Translate(Vector3.right * amtToMove, Space.Self);
        
    }
    
}

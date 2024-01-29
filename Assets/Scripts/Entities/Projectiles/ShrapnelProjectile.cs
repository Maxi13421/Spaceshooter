using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShrapnelProjectile : PlayerProjectile
{
    
    
    public int childrenAmount;
    public float childLifeSpan;
    public float childSpeed;
    public float childDamage;
    
    
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
        float amtToMove = projectileSpeed * Time.fixedDeltaTime;
        transform.Translate(amtToMove * Vector3.right, Space.Self);
    }


    protected override void CheckLifespan()
    {
        if (Time.time - LifeStart >= lifespan)
        {
            gameObject.SetActive(false);
            for (int aaa = 0; aaa < childrenAmount; aaa++)
            {
                GameObject o = Level.ShrapnelProjectileChildPool.GetPooledObject();
                o.transform.position = transform.position;
                o.transform.right = Quaternion.AngleAxis(((float)(aaa*360))/childrenAmount, Vector3.forward) * transform.right;
                o.GetComponent<StandardProjectile>().damage = childDamage;
                o.GetComponent<StandardProjectile>().projectileSpeed = childSpeed;
                o.GetComponent<StandardProjectile>().lifespan = childLifeSpan;
                o.transform.parent = transform.parent;
            }
        }
    }
    
    
}
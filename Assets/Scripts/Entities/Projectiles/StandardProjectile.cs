using System.Collections;
using System.Collections.Generic;
using Code.Scripts;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StandardProjectile : Projectile
{
    
    
    void Start()
    {
        projectileSpeed = 5;
        damage = 6;
    }

    

    
    

    protected override void UpdatePosition()
    {
        float amtToMove = projectileSpeed * Time.fixedDeltaTime;
        transform.Translate(Vector3.right * amtToMove, Space.World);
    }
    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        Enemy collideWith = other.GetComponent<Enemy>();
        if (collideWith != null)
        {
            gameObject.SetActive(false);
            collideWith.currenthp -= damage;


        }
    }
}

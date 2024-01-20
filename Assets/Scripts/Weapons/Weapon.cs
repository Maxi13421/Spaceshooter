using System;
using UnityEngine;


public abstract class Weapon
{
    protected Player Player;
    
    public Weapon()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    public Weapon(Vector3[] weaponPositions)
    {
        
    }
    
    public abstract void Shoot();
    public abstract void StopShooting();
}

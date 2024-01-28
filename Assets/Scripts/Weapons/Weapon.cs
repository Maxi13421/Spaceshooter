using System;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    protected Player Player;
    
    public Weapon()
    {
        Player = transform.parent.GetComponent<Player>();
    }
    public Weapon(Vector3[] weaponPositions)
    {
        
    }
    
    public abstract void Shoot();
    public abstract void StopShooting();
}

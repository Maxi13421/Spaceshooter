using System;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    public float consumption;
    
    public Weapon()
    {
    }
    public Weapon(Vector3[] weaponPositions)
    {
        
    }
    
    public abstract void Shoot();
    public abstract void StopShooting();
}

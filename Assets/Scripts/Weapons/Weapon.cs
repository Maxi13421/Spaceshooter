using System;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    public float consumption;
    
    
    
    protected float _cooldownMicro = 0.1f; //Sonst würde in einem Frame der ganze Cooldown verschossen werden.
    public Weapon()
    {
    }
    public Weapon(Vector3[] weaponPositions)
    {
        
    }
    
    public abstract void Shoot();
    public abstract void StopShooting();
}

using System;
using UnityEngine;

namespace Code.Scripts
{
    public abstract class Weapon
    {
        public Weapon()
        {
            
        }
        public Weapon(Vector3[] weaponPositions, GameObject player)
        {
            
        }
        
        public abstract void Shoot();
    }
}
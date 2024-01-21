using System.Collections.Generic;
using UnityEngine;

    
public class StandardWeapon : Weapon
{
    public Vector3[] WeaponPositions;
    
    
    
    private float _cooldown = 0.2f;
    private float _cooldownMax = 2;
    private float _cooldownCur;
    private float _cooldownMicro = 0.1f; //Sonst würde in einem Frame der ganze Cooldown verschossen werden.
    private float _lastShot = float.MinValue;
    
    
    
    
    public StandardWeapon(Vector3[] weaponPositions)
    {
        this.WeaponPositions = weaponPositions;
        _cooldownCur = _cooldownMax;
        
    }
    
    public override void Shoot()
    {
        if (Time.time - _lastShot + _cooldownCur >= _cooldown && Time.time - _lastShot > _cooldownMicro)
        {
            _cooldownCur += (Time.time - _lastShot);
            if (_cooldownCur > _cooldownMax)
            {
                _cooldownCur = _cooldownMax;
            }
            _cooldownCur -= _cooldown;
            _lastShot = Time.time;
            foreach (var weaponPosition in WeaponPositions)
            {
                GameObject o = Level.HomingProjectilePool.GetPooledObject();
                o.transform.position = Player.transform.position + weaponPosition;
                o.transform.right = Vector3.right;
            }
        }
    }

    public override void StopShooting()
    {
        
    }
}
    
    

using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts
{
    
    public class StandardWeapon : Weapon
    {
        public Vector3[] WeaponPositions;
        private Player _player;
        private GameObject projectile = (GameObject)Resources.Load("Projectile/StandardProjectile", typeof(GameObject));
        private ObjectPool _projectilePool;
        private const int AmountToPool = 10;
        
        private float _cooldown = 0.2f;
        private float _cooldownMax = 2;
        private float _cooldownCur;
        private float _cooldownMicro = 0.1f; //Sonst würde in einem Frame der ganze Cooldown verschossen werden.
        private float _lastShot = float.MinValue;
        
        
        
        
        public StandardWeapon(Vector3[] weaponPositions, Player player)
        {
            this.WeaponPositions = weaponPositions;
            this._player = player;
            _projectilePool = new ObjectPool(projectile, 10);
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
                    GameObject o = _projectilePool.GetPooledObject();
                    o.transform.position = _player.transform.position + weaponPosition;
                }
            }
        }
    }
}
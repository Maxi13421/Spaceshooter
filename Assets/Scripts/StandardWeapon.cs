using UnityEngine;

namespace Code.Scripts
{
    
    public class StandardWeapon : Weapon
    {
        public Vector3[] WeaponPositions;
        private Player _player;
        private GameObject projectile = (GameObject)Resources.Load("Projectile/StandardProjectile", typeof(GameObject));
        
        private float _cooldown = 1;
        private float _cooldownMax = 2;
        private float _cooldownCur;
        private float _cooldownMicro = 0.2f; //Sonst würde in einem Frame der ganze Cooldown verschossen werden.
        private float _lastShot = float.MinValue;
        
        public StandardWeapon(Vector3[] weaponPositions, Player player)
        {
            this.WeaponPositions = weaponPositions;
            this._player = player;
            _cooldownCur = _cooldownMax;
        }
        
        public override void Shoot()
        {
            if (Time.time - _lastShot + _cooldownCur >= _cooldown && Time.time - _lastShot > _cooldownMicro)
            {
                _cooldownCur += (Time.time - _lastShot);
                Debug.Log((Time.time - _lastShot).ToString());
                if (_cooldownCur > _cooldownMax)
                {
                    _cooldownCur = _cooldownMax;
                }
                Debug.Log(_cooldownCur.ToString());
                _cooldownCur -= _cooldown;
                Debug.Log(_cooldownCur.ToString());
                _lastShot = Time.time;
                foreach (var weaponPosition in WeaponPositions)
                {
                     Object.Instantiate(projectile, weaponPosition + _player.transform.position, Quaternion.identity);
                }
            }
        }
    }
}
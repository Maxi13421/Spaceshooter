using UnityEngine;

namespace Code.Scripts
{
    
    public class StandardWeapon : Weapon
    {
        public Vector3[] WeaponPositions;
        private float cooldown = 1;
        private float lastShot = float.MinValue;
        private GameObject player;
        private GameObject projectile = (GameObject)Resources.Load("Projectile/Projectile", typeof(GameObject));
        
        public StandardWeapon(Vector3[] WeaponPositions, GameObject player)
        {
            this.WeaponPositions = WeaponPositions;
            this.player = player;
        }
        
        public override void Shoot()
        {
            if (Time.time - lastShot >= cooldown)
            {
                Debug.Log("Shoot");
                lastShot = Time.time;
                foreach (var weaponPosition in WeaponPositions)
                {
                     //player.Instantiate(projectile, weaponPosition, Quaternion.identity);
                }
            }
        }
    }
}
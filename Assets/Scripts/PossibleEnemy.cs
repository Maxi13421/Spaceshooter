using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PossibleEnemy : Entity
{
    public bool isStatic;
    public Enemy.EnemyType[] possibleEnemies;
    public Projectile.ProjectileType[] possibleProjectiles;
    void Start()
    {
        
    }

    private void Awake()
    {
        Enemy.EnemyType enemy = possibleEnemies[Random.Range(0, possibleEnemies.Length)];
        Projectile.ProjectileType projectile = possibleProjectiles[Random.Range(0, possibleProjectiles.Length)];
        GameObject enemyObject = null;
        switch (enemy)
        {
            case Enemy.EnemyType.StandardTurret:
                enemyObject = Level.StandardTurretPool.GetPooledObject();
                break;
            case Enemy.EnemyType.HomingTurret:
                enemyObject = Level.HomingTurretPool.GetPooledObject();
                break;
            case Enemy.EnemyType.TravellingTurret:
                Debug.LogError("TravellingTurret not possible");
                break; 
                
        }
        enemyObject.transform.position = transform.position;
        enemyObject.transform.eulerAngles = new Vector3(0, 0, 90);
        enemyObject.GetComponent<Turret>().projectileType = projectile;
        enemyObject.transform.parent = transform.parent;

        enemyObject.GetComponent<Turret>().isStatic = isStatic;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position,1f);
    }
}

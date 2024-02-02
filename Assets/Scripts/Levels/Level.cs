using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Level : MonoBehaviour
{
    
    public float speed = 2;
    private float space = 10;
    public float levelEnd = 10000;

    protected String PiecesDirectory;

    public static UnityEngine.Object[][] allLevels = null;

    protected virtual void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * speed * Vector3.left, Space.World);
    }


    public static ObjectPool CoinPool;
    public static GameObject Coin;
    
    public static ObjectPool Coin10Pool;
    public static GameObject Coin10;
    
    public static GameObject StandardProjectile;
    public static ObjectPool StandardProjectilePool;
    
    public static GameObject HomingProjectile;
    public static ObjectPool HomingProjectilePool;
    
    public static GameObject ShrapnelProjectile;
    public static ObjectPool ShrapnelProjectilePool;
    
    public static GameObject ShrapnelProjectileChild;
    public static ObjectPool ShrapnelProjectileChildPool;
    
    public static GameObject BigProjectile;
    public static ObjectPool BigProjectilePool;
    
    public static GameObject StandardTurret;
    public static ObjectPool StandardTurretPool;
    
    public static GameObject TurretStandardProjectile;
    public static ObjectPool TurretStandardProjectilePool;
    
    public static GameObject TurretHomingProjectile;
    public static ObjectPool TurretHomingProjectilePool;
    
    public static GameObject TurretShrapnelProjectile;
    public static ObjectPool TurretShrapnelProjectilePool;
    
    public static GameObject TurretShrapnelProjectileChild;
    public static ObjectPool TurretShrapnelProjectileChildPool;
    
    public static GameObject TurretBigProjectile;
    public static ObjectPool TurretBigProjectilePool;
    
    public static GameObject HomingTurret;
    public static ObjectPool HomingTurretPool;
    
    public static GameObject Asteroid;
    public static ObjectPool AsteroidPool;


    

    protected virtual void Awake()
    {
        if (allLevels == null)
        {
            allLevels = new[]
            {
                Resources.LoadAll("Levels/ObstacleSpam", typeof(GameObject)),
                Resources.LoadAll("Levels/ShrapnelSpam", typeof(GameObject)),
                Resources.LoadAll("Levels/MachineGunSpam", typeof(GameObject)),
                Resources.LoadAll("Levels/BigSpam",typeof(GameObject)),
                Resources.LoadAll("Levels/HomingSpam",typeof(GameObject))
            };
        }
        
        if (Coin == null)
        {
            Coin = (GameObject)Resources.Load("Coin/Coin", typeof(GameObject));
            CoinPool = new ObjectPool(Coin, 0);
            
            Coin10 = (GameObject)Resources.Load("Coin/Coin10", typeof(GameObject));
            Coin10Pool = new ObjectPool(Coin10, 0);

            StandardProjectile = (GameObject)Resources.Load("Projectile/StandardProjectile", typeof(GameObject));
            StandardProjectilePool = new ObjectPool(StandardProjectile, 0);

            HomingProjectile = (GameObject)Resources.Load("Projectile/HomingProjectile", typeof(GameObject));
            HomingProjectilePool = new ObjectPool(HomingProjectile, 0);
            
            ShrapnelProjectile =
                (GameObject)Resources.Load("Projectile/ShrapnelProjectile", typeof(GameObject));
            ShrapnelProjectilePool = new ObjectPool(ShrapnelProjectile, 0);

            ShrapnelProjectileChild =
                (GameObject)Resources.Load("Projectile/ShrapnelProjectileChild", typeof(GameObject));
            ShrapnelProjectileChildPool = new ObjectPool(ShrapnelProjectileChild, 0);
            
            BigProjectile =
                (GameObject)Resources.Load("Projectile/BigProjectile", typeof(GameObject));
            BigProjectilePool = new ObjectPool(BigProjectile, 0);

            StandardProjectile =
                (GameObject)Resources.Load("Projectile/TurretStandardProjectile", typeof(GameObject));
            StandardProjectilePool = new ObjectPool(StandardProjectile, 0);
            
            TurretStandardProjectile =
                (GameObject)Resources.Load("Projectile/TurretStandardProjectile", typeof(GameObject));
            TurretStandardProjectilePool = new ObjectPool(TurretStandardProjectile, 0);


            TurretHomingProjectile =
                (GameObject)Resources.Load("Projectile/TurretHomingProjectile", typeof(GameObject));
            TurretHomingProjectilePool = new ObjectPool(TurretHomingProjectile, 0);

            TurretShrapnelProjectile =
                (GameObject)Resources.Load("Projectile/TurretShrapnelProjectile", typeof(GameObject));
            TurretShrapnelProjectilePool = new ObjectPool(TurretShrapnelProjectile, 0);

            TurretShrapnelProjectileChild =
                (GameObject)Resources.Load("Projectile/TurretShrapnelProjectileChild", typeof(GameObject));
            TurretShrapnelProjectileChildPool = new ObjectPool(TurretShrapnelProjectileChild, 0);
            
            TurretBigProjectile =
                (GameObject)Resources.Load("Projectile/TurretBigProjectile", typeof(GameObject));
            TurretBigProjectilePool = new ObjectPool(TurretBigProjectile, 0);

            StandardTurret = (GameObject)Resources.Load("Enemies/Turrets/StandardTurret", typeof(GameObject));
            StandardTurretPool = new ObjectPool(StandardTurret, 0);

            HomingTurret = (GameObject)Resources.Load("Enemies/Turrets/HomingTurret", typeof(GameObject));
            HomingTurretPool = new ObjectPool(HomingTurret, 0);

            Asteroid = (GameObject)Resources.Load("Enemies/Asteroid/Asteroid", typeof(GameObject));
            AsteroidPool = new ObjectPool(Asteroid, 0);
        }
        
        GenerateLevel();
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(-10000,Camera.main.orthographicSize,0),new Vector3(+10000,+Camera.main.orthographicSize,0));
        Gizmos.DrawLine(new Vector3(-10000,-Camera.main.orthographicSize,0),new Vector3(+10000,-Camera.main.orthographicSize,0));
        Gizmos.DrawLine(new Vector3(-0,Camera.main.orthographicSize,0),new Vector3(0,-Camera.main.orthographicSize,0));
    }

    protected virtual void GenerateLevel()
    {
        float curSpawn = 0;
        GameObject lastSpawn = null;
        for (int aaa = 0; aaa < 10 + GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().LevelCount/3; aaa++)
        {
            int nextTileType = GetNextTileType();
            if (aaa == 0)
            {
                
                lastSpawn = Instantiate((GameObject)allLevels[nextTileType][Random.Range(0, allLevels[nextTileType].Length)],new Vector3(curSpawn,0,0), Quaternion.identity,transform);
            }
            else
            {
                UnityEngine.Object[] filtered = Array.FindAll(allLevels[nextTileType], o =>
                {
                    for (int aaa = 0; aaa<5;aaa++)
                    {
                        if (lastSpawn.GetComponent<LevelPiece>().connectionRight[aaa] &&
                            ((GameObject)o).GetComponent<LevelPiece>().connectionLeft[aaa])
                        {
                            return true;
                        }
                    }

                    return false;
                });
                lastSpawn = Instantiate((GameObject)filtered[Random.Range(0, filtered.Length)], new Vector3(curSpawn,0,0), Quaternion.identity,
                    transform);
            }

            curSpawn += space + lastSpawn.GetComponent<LevelPiece>().GetComponentInChildren<LevelPieceEnd>().transform.localPosition.x;
        }

        levelEnd = curSpawn + 20;

    }

    protected abstract int GetNextTileType();
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Level : MonoBehaviour
{

    public static ObjectPool CoinPool;
    public static GameObject Coin;
    
    public static GameObject StandardProjectile;
    public static ObjectPool StandardProjectilePool;
    
    public static GameObject Turret;
    public static ObjectPool TurretPool;
    
    public static GameObject TurretStandardProjectile;
    public static ObjectPool TurretStandardProjectilePool;
    
    public static GameObject TurretDirectionalProjectile;
    public static ObjectPool TurretDirectionalProjectilePool;
    
    public static GameObject HomingTurret;
    public static ObjectPool HomingTurretPool;
    
    public static GameObject Asteroid;
    public static ObjectPool AsteroidPool;


    

    protected virtual void Awake()
    {
        Coin = (GameObject)Resources.Load("Coin/Coin", typeof(GameObject));
        CoinPool = new ObjectPool(Coin, 20);
        
        StandardProjectile = (GameObject)Resources.Load("Projectile/StandardProjectile", typeof(GameObject));
        StandardProjectilePool = new ObjectPool(StandardProjectile, 20);
        
        TurretStandardProjectile = (GameObject)Resources.Load("Projectile/TurretStandardProjectile", typeof(GameObject));
        TurretStandardProjectilePool = new ObjectPool(TurretStandardProjectile, 20);
        
        TurretDirectionalProjectile = (GameObject)Resources.Load("Projectile/TurretDirectionalProjectile", typeof(GameObject));
        TurretDirectionalProjectilePool = new ObjectPool(TurretDirectionalProjectile, 20);
        
        Turret = (GameObject)Resources.Load("Enemies/Turrets/Turret/Turret", typeof(GameObject));
        TurretPool = new ObjectPool(Turret, 10);
        
        HomingTurret = (GameObject)Resources.Load("Enemies/Turrets/HomingTurret/HomingTurret", typeof(GameObject));
        HomingTurretPool = new ObjectPool(HomingTurret, 10);
        
        Asteroid = (GameObject)Resources.Load("Enemies/Asteroid/Asteroid", typeof(GameObject));
        AsteroidPool = new ObjectPool(Asteroid, 10);
    }
}

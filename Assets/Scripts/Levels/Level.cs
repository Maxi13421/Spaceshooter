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
    
    public static GameObject HomingProjectile;
    public static ObjectPool HomingProjectilePool;
    
    public static GameObject StandardTurret;
    public static ObjectPool StandardTurretPool;
    
    public static GameObject TurretStandardProjectile;
    public static ObjectPool TurretStandardProjectilePool;
    
    public static GameObject TurretDirectionalProjectile;
    public static ObjectPool TurretDirectionalProjectilePool;
    
    public static GameObject TurretHomingProjectile;
    public static ObjectPool TurretHomingProjectilePool;
    
    public static GameObject TurretShrapnelProjectile;
    public static ObjectPool TurretShrapnelProjectilePool;
    
    public static GameObject TurretShrapnelProjectileChild;
    public static ObjectPool TurretShrapnelProjectileChildPool;
    
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
        
        HomingProjectile = (GameObject)Resources.Load("Projectile/HomingProjectile", typeof(GameObject));
        HomingProjectilePool = new ObjectPool(HomingProjectile, 20);
        
        TurretStandardProjectile = (GameObject)Resources.Load("Projectile/TurretStandardProjectile", typeof(GameObject));
        TurretStandardProjectilePool = new ObjectPool(TurretStandardProjectile, 20);
        
        TurretDirectionalProjectile = (GameObject)Resources.Load("Projectile/TurretDirectionalProjectile", typeof(GameObject));
        TurretDirectionalProjectilePool = new ObjectPool(TurretDirectionalProjectile, 20);
        
        TurretHomingProjectile = (GameObject)Resources.Load("Projectile/TurretHomingProjectile", typeof(GameObject));
        TurretHomingProjectilePool = new ObjectPool(TurretHomingProjectile, 20);
        
        TurretShrapnelProjectile = (GameObject)Resources.Load("Projectile/TurretShrapnelProjectile", typeof(GameObject));
        TurretShrapnelProjectilePool = new ObjectPool(TurretShrapnelProjectile, 20);
        
        TurretShrapnelProjectileChild = (GameObject)Resources.Load("Projectile/TurretShrapnelProjectileChild", typeof(GameObject));
        TurretShrapnelProjectileChildPool = new ObjectPool(TurretShrapnelProjectileChild, 20);
        
        StandardTurret = (GameObject)Resources.Load("Enemies/Turrets/StandardTurret", typeof(GameObject));
        StandardTurretPool = new ObjectPool(StandardTurret, 10);
        
        HomingTurret = (GameObject)Resources.Load("Enemies/Turrets/HomingTurret", typeof(GameObject));
        HomingTurretPool = new ObjectPool(HomingTurret, 10);
        
        Asteroid = (GameObject)Resources.Load("Enemies/Asteroid/Asteroid", typeof(GameObject));
        AsteroidPool = new ObjectPool(Asteroid, 10);
    }
}

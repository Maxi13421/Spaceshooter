using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Level : MonoBehaviour
{

    public static ObjectPool CoinPool;
    public static GameObject Coin;


    public Level()
    {
        
    }

    protected void Awake()
    {
        Coin = (GameObject)Resources.Load("Coin/Coin", typeof(GameObject));
        CoinPool = new ObjectPool(Coin, 20);
    }
}

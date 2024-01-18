using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{

    private float _speed = 2;
    private GameObject _asteroid;
    private ObjectPool _asteroidPool;
    
    void Start()
    {
        
    }

    private void Awake()
    {
        _asteroid = (GameObject)Resources.Load("Asteroid/Asteroid", typeof(GameObject));
        _asteroidPool = new ObjectPool(_asteroid, 10);
    }

    private float _lastSpawn = float.MinValue;
    void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * _speed * Vector3.left, Space.World);
        float currentSpawnXCoordinate = 9f + 5f;
        if (Time.time-_lastSpawn>2f)
        {
            _lastSpawn = Time.time;
            GameObject o = _asteroidPool.GetPooledObject();
            o.transform.parent = transform;
            o.GetComponent<Asteroid>().hp = 15;
            o.transform.position = new Vector3(currentSpawnXCoordinate, 0, 0);
        }
    }
    
    
}

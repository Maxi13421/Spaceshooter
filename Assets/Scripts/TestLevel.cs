using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class TestLevel : Level
{
    [FormerlySerializedAs("_speed")] public float speed = 2;
    private GameObject _asteroid;
    private ObjectPool _asteroidPool;
    
    void Start()
    {
        
    }

    private void Awake()
    {
        base.Awake();
        _asteroid = (GameObject)Resources.Load("Asteroid/Asteroid", typeof(GameObject));
        _asteroidPool = new ObjectPool(_asteroid, 10);
    }

    private float _lastSpawn = float.MinValue;
    void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * speed * Vector3.left, Space.World);
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

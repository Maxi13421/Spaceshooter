using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class TestLevel : Level
{
    public float speed = 2;
    
    void Start()
    {
        
    }

    

    private float _lastSpawn = float.MinValue;
    void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * speed * Vector3.left, Space.World);
        float currentSpawnXCoordinate = 9f + 5f;
        if (Time.time-_lastSpawn>4f)
        {
            _lastSpawn = Time.time;
            GameObject o = ObstacleGenerator.GetObstacle(2,2);
            o.transform.parent = transform;
            //o.GetComponent<Enemy>().hp = 15;
            //o.GetComponent<Enemy>().currenthp = 15;
            o.transform.position = new Vector3(currentSpawnXCoordinate, 0, 0);
        }
    }
}

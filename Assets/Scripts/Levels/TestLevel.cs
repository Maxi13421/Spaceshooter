using UnityEngine;

public class TestLevel : Level
{
    
    
    void Start()
    {
        
    }

    private bool _obstacle = false;
    

    private float _lastSpawn = float.MinValue;
    override protected void FixedUpdate()
    {
        base.FixedUpdate();
        float currentSpawnXCoordinate = 9f + 5f;
        if (Time.time-_lastSpawn>4f)
        {
            if (_obstacle)
            {
                _lastSpawn = Time.time;
                GameObject o = ObstacleGenerator.GetObstacle(2, 2);
                o.transform.parent = transform;
                //o.GetComponent<Enemy>().hp = 15;
                //o.GetComponent<Enemy>().currenthp = 15;
                o.transform.position = new Vector3(currentSpawnXCoordinate, 0, 0);
            }
            else
            {
                _lastSpawn = Time.time;
                GameObject o = Level.AsteroidPool.GetPooledObject();
                o.transform.parent = transform;
                o.GetComponent<Enemy>().hp = 15;
                o.GetComponent<Enemy>().currenthp = 15;
                o.transform.position = new Vector3(currentSpawnXCoordinate, 0, 0);
            }

            _obstacle = !_obstacle;
        }
    }

    protected override int GetNextTileType()
    {
        return 0;
    }

    protected override void GenerateLevel()
    {
        
    }
}



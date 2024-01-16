using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject projectilePrefab;

    

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float random = Random.value;
        if (Random.value < 0.1f)
        {
            Debug.Log("Asteroid");
            Instantiate(projectilePrefab, new Vector3(Random.value * 20 - 10, 5, 0), Random.rotation);
        }


    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Asteroid : Enemy
{
    
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;


    private float speed;

    

    private void OnBecameInvisible()
    {
        //gameObject.SetActive(false);
        
    }
    
    
    
    
    
}

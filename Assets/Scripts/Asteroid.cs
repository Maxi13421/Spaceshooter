using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Asteroid : Enemy
{
    
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;


    private float speed;

    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        hp = 15;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            gameObject.SetActive(false);
            GameObject coin = Level.CoinPool.GetPooledObject();
            coin.transform.position = transform.position;
            coin.transform.parent = transform.parent;
        }
    }

    private void OnBecameInvisible()
    {
        //gameObject.SetActive(false);
        
    }
    
    
    
    
    
}

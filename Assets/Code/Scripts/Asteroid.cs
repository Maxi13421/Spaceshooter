using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Asteroid : MonoBehaviour
{
    
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    private float speed;

    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        setSpeedAndPosition();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float amtToMove = speed * Time.deltaTime;
        transform.Translate(-new UnityEngine.Vector3(0,1,0) * amtToMove, Space.World);
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        
    }
    
    
    
    
    public void setSpeedAndPosition()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        transform.position = new Vector3(Random.value * 20 - 10, 5, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardProjectile : MonoBehaviour
{
    public float projectileSpeed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float amtToMove = projectileSpeed * Time.deltaTime;
        transform.Translate(Vector3.right * amtToMove, Space.World);
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //transform.localPosition = new Vector3(0, -1, 0);
        Debug.Log("Collision");
        Asteroid collideWith = other.GetComponent<Asteroid>();
        if (collideWith != null)
        {
            Debug.Log("Collide with: " + collideWith.name);
            collideWith.audioSource.Play();
            collideWith.setSpeedAndPosition();
            
        }
    }
}

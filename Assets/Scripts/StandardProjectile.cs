using System.Collections;
using System.Collections.Generic;
using Code.Scripts;
using UnityEngine;

public class StandardProjectile : MonoBehaviour
{
    public float projectileSpeed = 5;
    public float damage = 5;
    
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
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy collideWith = other.GetComponent<Enemy>();
        if (collideWith != null)
        {
            gameObject.SetActive(false);
            collideWith.hp -= damage;


        }
    }
}

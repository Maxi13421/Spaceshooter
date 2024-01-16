using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float playerSpeed = 0.5f;

    public GameObject projectilePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float amtToMove = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        transform.Translate(Vector3.right * amtToMove);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Console.WriteLine(transform.position.ToString());
            Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + (0.01f * transform.localScale.y ), transform.position.z) , Quaternion.identity);
        }
    }
}

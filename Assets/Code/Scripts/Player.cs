using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float playerSpeed = 0.5f;
    private float _inputHorizontal;
    private float _inputVertical;
    [SerializeField] private float boundariesHorizontal = 8;
    [SerializeField] private float boundariesVertical = 3;

    public GameObject projectilePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        _inputHorizontal = Input.GetAxis("Horizontal");
        _inputVertical = Input.GetAxis("Vertical");
    }

    private void Move()
    {
        transform.Translate( playerSpeed * Time.fixedDeltaTime * _inputHorizontal * Vector3.right, Space.World);
        transform.Translate( playerSpeed * Time.fixedDeltaTime * _inputVertical * Vector3.up, Space.World);
        if (transform.position.x > boundariesHorizontal)
        {
            transform.position = new Vector3(boundariesHorizontal, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -boundariesHorizontal)
        {
            transform.position = new Vector3(-boundariesHorizontal, transform.position.y, transform.position.z);
        }
        if (transform.position.y > boundariesVertical)
        {
            transform.position = new Vector3(transform.position.x, boundariesVertical, transform.position.z);
        }
        if (transform.position.y < -boundariesVertical)
        {
            transform.position = new Vector3(transform.position.x, -boundariesVertical, transform.position.z);
        }
    }
}

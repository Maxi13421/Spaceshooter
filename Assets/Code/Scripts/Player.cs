using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float playerSpeed = 0.5f;
    private float _inputHorizontal;
    private float _inputVertical;
    private bool _inputFireMain;
    private Weapon MainWeapon;
    [SerializeField] private float boundariesHorizontal = 8;
    [SerializeField] private float boundariesVertical = 3;

    public GameObject projectilePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        MainWeapon = new StandardWeapon(new[] { transform.GetChild(0).position, transform.GetChild(1).position }, this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
        FireMain();
        Debug.Log(_inputFireMain.ToString());
    }

    private void GetInput()
    {
        _inputHorizontal = Input.GetAxis("Horizontal");
        _inputVertical = Input.GetAxis("Vertical");
        _inputFireMain = Input.GetButton("FireMain");
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

    private void FireMain()
    {
        if (_inputFireMain)
        {
            MainWeapon.Shoot();
        }
    }
}

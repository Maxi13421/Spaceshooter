using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts;
using UnityEngine;

public class Player : Entity
{
    
    private float _inputHorizontal;
    private float _inputVertical;
    private bool _inputFireMain;
    private bool _inputBoost;
    private Weapon _mainWeapon;
    public float cooldownUsagePerSecond = 1f;
    public float cooldownRechargePerSecond = 0.2f;
    public float cooldownMax = 2;
    private float _cooldownCur;
    private bool _useBoost;
    private bool _boostEmpty = false;
    
    
    [SerializeField] private float boundariesHorizontal = 8;
    [SerializeField] private float boundariesVertical = 3;
    
    public int money;
    public float currenthp;
    public float hp;
    public float playerSpeed = 0.5f;
    public float playerSpeedWithBoost = 5;
    

    public GameObject projectilePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        _cooldownCur = cooldownMax;
        _mainWeapon = new StandardWeapon(new[] { transform.GetChild(0).position-transform.position, transform.GetChild(1).position-transform.position }, this);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        UpdateBoostCooldown();
        Move();
        FireMain();
    }

    private void GetInput()
    {
        _inputHorizontal = Input.GetAxis("Horizontal");
        _inputVertical = Input.GetAxis("Vertical");
        _inputFireMain = Input.GetButton("FireMain");
        _inputBoost = Input.GetButton("Boost");
    }

    private void Move()
    {
        if (_useBoost)
        {
            transform.Translate( playerSpeedWithBoost * Time.fixedDeltaTime * _inputHorizontal * Vector3.right, Space.World);
            transform.Translate( playerSpeedWithBoost * Time.fixedDeltaTime * _inputVertical * Vector3.up, Space.World);
        }
        else
        {
            transform.Translate( playerSpeed * Time.fixedDeltaTime * _inputHorizontal * Vector3.right, Space.World);
            transform.Translate( playerSpeed * Time.fixedDeltaTime * _inputVertical * Vector3.up, Space.World);
        }
        
        
        
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
            _mainWeapon.Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            money++;
            Debug.Log(money.ToString());
        }
    }

    private void UpdateBoostCooldown()
    {
        _cooldownCur += cooldownRechargePerSecond * Time.fixedDeltaTime;
        if (_cooldownCur > cooldownMax)
        {
            _cooldownCur = cooldownMax;
        }

        if (_inputBoost)
        {
            if (_cooldownCur >= cooldownUsagePerSecond * Time.fixedDeltaTime && !_boostEmpty)
            {
                _cooldownCur -= cooldownUsagePerSecond * Time.fixedDeltaTime;
                _useBoost = true;

            }
            else
            {
                _boostEmpty = true;
                _useBoost = false;
            }
            
        }
        else
        {
            _useBoost = false;
            _boostEmpty = false;
        }
    }
}

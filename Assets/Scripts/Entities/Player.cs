using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : Entity
{
    
    private float _inputHorizontal;
    private float _inputVertical;
    private bool _inputFireMain;
    private bool _inputBoost;
    private bool _inputShield;
    private Weapon _mainWeapon;
    public float boostCooldownUsagePerSecond = 1f;
    public float boostCooldownRechargePerSecond = 0.2f;
    public float boostCooldownMax = 2;
    private float _boostCooldownCur;
    private bool _useBoost;
    private bool _boostEmpty = false;
    
    public float shieldCooldownUsagePerSecond = 1f;
    public float shieldCooldownRechargePerSecond = 0.2f;
    public float shieldCooldownMax = 2;
    private float _shieldCooldownCur;
    private bool _useShield;
    private bool _shieldEmpty = false;
    
    
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
        _boostCooldownCur = boostCooldownMax;
        _mainWeapon = new StandardWeapon(new[] { transform.GetChild(0).position-transform.position, transform.GetChild(1).position-transform.position });
        //_mainWeapon = new Laser();
        GameSystem.Player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        UpdateBoostCooldown();
        UpdateShieldCooldown();
        Move();
        FireMain();
        Shield();

    }

    private void GetInput()
    {
        _inputHorizontal = Input.GetAxis("Horizontal");
        _inputVertical = Input.GetAxis("Vertical");
        _inputFireMain = Input.GetButton("FireMain");
        _inputBoost = Input.GetButton("Boost");
        _inputShield = Input.GetButton("Shield");
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
        else
        {
            _mainWeapon.StopShooting();
        }
    }

    private void Shield()
    {
        transform.GetComponentInChildren<Shield>().gameObject.SetActive(_useShield);
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
        _boostCooldownCur += boostCooldownRechargePerSecond * Time.fixedDeltaTime;
        if (_boostCooldownCur > boostCooldownMax)
        {
            _boostCooldownCur = boostCooldownMax;
        }

        if (_inputBoost)
        {
            if (_boostCooldownCur >= boostCooldownUsagePerSecond * Time.fixedDeltaTime && !_boostEmpty)
            {
                _boostCooldownCur -= boostCooldownUsagePerSecond * Time.fixedDeltaTime;
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
    
    private void UpdateShieldCooldown()
    {
        _shieldCooldownCur += shieldCooldownRechargePerSecond * Time.fixedDeltaTime;
        if (_shieldCooldownCur > shieldCooldownMax)
        {
            _shieldCooldownCur = shieldCooldownMax;
        }

        if (_inputShield)
        {
            if (_shieldCooldownCur >= shieldCooldownUsagePerSecond * Time.fixedDeltaTime && !_shieldEmpty)
            {
                _shieldCooldownCur -= shieldCooldownUsagePerSecond * Time.fixedDeltaTime;
                _useShield = true;

            }
            else
            {
                _shieldEmpty = true;
                _useShield = false;
            }
            
        }
        else
        {
            _useShield = false;
            _shieldEmpty = false;
        }
    }
}

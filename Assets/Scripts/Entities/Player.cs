using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : Entity
{
    
    private float _inputHorizontal;
    private float _inputVertical;
    private bool _inputFireMain;
    private bool _inputBoost;
    private bool _inputShield;
    public Weapon _mainWeapon;
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
    
    public float weaponCooldownRechargePerSecond = 0.2f;
    public float weaponCooldownMax = 2;
    private float _weaponCooldownCur;
    
    
    public int money;
    public float currenthp;
    public float hp;
    public float playerSpeed = 0.5f;
    public float playerSpeedWithBoost = 5;

    public int hpLevel = 0;
    public int shieldLevel = 0;
    public int boostLevel = 0;
    public int munitionRegenerationLevel = 0;
    public int lifeSpanLevel = 0;
    public int speedLevel = 0;
    public bool gotStandard = true;
    public bool gotLaser = false;
    public bool gotBig = false;
    public bool gotShrapnel = false;

    public Weapon standardWeapon;
    public Weapon laser;
    public Weapon bigWeapon;
    public Weapon shrapnelWeapon;
    

    public GameObject projectilePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        _boostCooldownCur = boostCooldownMax;
        _shieldCooldownCur = shieldCooldownMax;
        _weaponCooldownCur = weaponCooldownMax;
        standardWeapon = Instantiate(Resources.Load("Player/StandardWeapon", typeof(GameObject)), transform)
            .GetComponent<Weapon>();
        laser = Instantiate(Resources.Load("Player/Laser", typeof(GameObject)), transform)
            .GetComponent<Weapon>();
        bigWeapon = Instantiate(Resources.Load("Player/BigWeapon", typeof(GameObject)), transform)
            .GetComponent<Weapon>();
        shrapnelWeapon = Instantiate(Resources.Load("Player/ShrapnelWeapon", typeof(GameObject)), transform)
            .GetComponent<Weapon>();
        _mainWeapon = standardWeapon;
        //_mainWeapon = new Laser();
        GameSystem.Player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        UpdateOverlay();
    }

    override protected void FixedUpdate()
    {
        base.FixedUpdate();
        if (GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().ZoomStatus == GameSystem.Zoom.Level)
        {
            UpdateBoostCooldown();
            UpdateShieldCooldown();
            UpdateWeaponCooldown();
            Move();
            FireMain();
            Shield();
        }
        

    }

    private void GetInput()
    {
        InputSystem inputSystem = GameObject.FindWithTag("InputSystem").GetComponent<InputSystem>();
        _inputHorizontal = inputSystem.moveAction.ReadValue<Vector2>().x;
        _inputVertical = inputSystem.moveAction.ReadValue<Vector2>().y;
        _inputFireMain = inputSystem.fireAction.ReadValue<float>()>=0.5;
        _inputBoost = inputSystem.boostAction.ReadValue<float>()>=0.5;
        _inputShield = inputSystem.shieldAction.ReadValue<float>()>=0.5;
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
        
        
        
        if (transform.position.x > GameSystem.MainCamera.orthographicSize*GameSystem.MainCamera.aspect*0.8f)
        {
            transform.position = new Vector3(GameSystem.MainCamera.orthographicSize*GameSystem.MainCamera.aspect*0.8f, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -GameSystem.MainCamera.orthographicSize*GameSystem.MainCamera.aspect*0.8f)
        {
            transform.position = new Vector3(-GameSystem.MainCamera.orthographicSize*GameSystem.MainCamera.aspect*0.8f, transform.position.y, transform.position.z);
        }
        if (transform.position.y > GameSystem.MainCamera.orthographicSize*0.8f)
        {
            transform.position = new Vector3(transform.position.x, GameSystem.MainCamera.orthographicSize*0.8f, transform.position.z);
        }
        if (transform.position.y < -GameSystem.MainCamera.orthographicSize*0.8f)
        {
            transform.position = new Vector3(transform.position.x, -GameSystem.MainCamera.orthographicSize*0.8f, transform.position.z);
        }
    }

    private void FireMain()
    {
        if (_inputFireMain && _weaponCooldownCur >= _mainWeapon.consumption)
        {
            _mainWeapon.Shoot();
            _weaponCooldownCur -= _mainWeapon.consumption;
        }
        else 
        {
            _mainWeapon.StopShooting();
        }
    }

    private void Shield()
    {
        transform.GetComponentInChildren<Shield>(true).gameObject.SetActive(_useShield);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.coin, transform.position);
            money++;
        }
        
        if (other.CompareTag("Coin10"))
        {
            other.gameObject.SetActive(false);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.coin, transform.position);
            money+=10;
        }

        if (other.CompareTag("ObstacleTile"))
        {
            currenthp = 0;
        }

        if (other.GetComponent<Enemy>() != null)
        {
            currenthp -= other.GetComponent<Enemy>().currenthp;
            other.GetComponent<Enemy>().currenthp = 0;
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

    private void UpdateWeaponCooldown()
    {
        _weaponCooldownCur += weaponCooldownRechargePerSecond * Time.fixedDeltaTime;
        if (_weaponCooldownCur > weaponCooldownMax)
        {
            _weaponCooldownCur = weaponCooldownMax;
        }
    }

    private void UpdateOverlay()
    {
        GameObject.FindWithTag("LifeBox").transform.localScale = new Vector3(344 * Math.Max((currenthp / hp),0), 16, 1);
        GameObject.FindWithTag("LifeBox").transform.localPosition = new Vector3(-8.5f-(1-currenthp/hp)*172f/32, 8.937f, 0);
        GameObject.FindWithTag("ShieldBox").transform.localScale = new Vector3(344 * _shieldCooldownCur/shieldCooldownMax, 16, 1);
        GameObject.FindWithTag("ShieldBox").transform.localPosition = new Vector3(-8.5f-(1-_shieldCooldownCur/shieldCooldownMax)*172f/32, 7.437f, 0);
        GameObject.FindWithTag("BoostBox").transform.localScale = new Vector3(344 * _boostCooldownCur/boostCooldownMax, 16, 1);
        GameObject.FindWithTag("BoostBox").transform.localPosition = new Vector3(-8.5f-(1-_boostCooldownCur/boostCooldownMax)*172f/32, 5.977f, 0);
        GameObject.FindWithTag("AmmoBox").transform.localScale = new Vector3(344 * _weaponCooldownCur/weaponCooldownMax, 16, 1);
        GameObject.FindWithTag("AmmoBox").transform.localPosition = new Vector3(-8.5f-(1-_weaponCooldownCur/weaponCooldownMax)*172f/32, 4.477f, 0);
        GameObject.FindWithTag("CoinCounter").GetComponent<TextMeshPro>().text = money.ToString();
    }
}

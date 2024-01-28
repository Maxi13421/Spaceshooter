using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Enemy : Entity
{
    public float hp;
    public float currenthp;
    public bool isStatic = false;
    

    protected GameObject Player;

    private void OnEnable()
    {
        currenthp = hp;
    }

    protected virtual void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }

    

    override protected void FixedUpdate()
    {
        base.FixedUpdate();
        CheckDeath();
        if (visible && !isStatic)
        {
              
            transform.Translate(new Vector3(GameObject.FindWithTag("Level").GetComponent<Level>().speed/4 * Time.fixedDeltaTime,0,0),Space.World);
            
            
        } 
    }

    void CheckDeath()
    {
        if (currenthp <= 0)
        {
            gameObject.SetActive(false);
            GameObject coin = Level.CoinPool.GetPooledObject();
            coin.transform.position = transform.position;
            coin.transform.parent = transform.parent;
        }
    }
    
    public enum EnemyType
    {
        StandardTurret,
        HomingTurret,
        TravellingTurret
    }
}

using System;
using UnityEngine;

public abstract class Enemy : Entity
{
    public float hp;
    public float currenthp;

    protected GameObject Player;

    private void OnEnable()
    {
        currenthp = hp;
    }

    protected virtual void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }

    protected virtual void FixedUpdate()
    {
        CheckDeath();
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
}

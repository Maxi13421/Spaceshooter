
using System;
using UnityEngine;

public abstract class Projectile: Entity
{
    public float projectileSpeed;
    public float damage;
    public float lifespan;
    protected float LifeStart;
    
    
    // Start is called before the first frame update
    void Start()
    {
    }

    

    private void OnEnable()
    {
        LifeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        UpdatePosition();
        CheckLifespan();
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    

    protected abstract void UpdatePosition();

    protected virtual void CheckLifespan()
    {
        if (Time.time - LifeStart >= lifespan)
        {
            gameObject.SetActive(false);
        }
    }

    
}

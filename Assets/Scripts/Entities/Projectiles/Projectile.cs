
using System;
using UnityEngine;

public abstract class Projectile: Entity
{
    public float projectileSpeed;
    public float damage;
    public float lifespan;
    private float _lifeStart;
    
    
    // Start is called before the first frame update
    void Start()
    {
    }

    

    private void OnEnable()
    {
        _lifeStart = Time.time;
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

    protected void CheckLifespan()
    {
        if (Time.time - _lifeStart >= lifespan)
        {
            gameObject.SetActive(false);
        }
    }
}

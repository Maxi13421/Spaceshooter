using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Enemy : Entity
{
    public float hp;
    public float currenthp;
    public bool isStatic = false;
    protected GameObject LifeBoxEdge;
    protected GameObject LifeBox;
    

    protected GameObject Player;

    private void OnEnable()
    {
        currenthp = hp;
    }

    protected virtual void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        LifeBoxEdge = Instantiate((GameObject)Resources.Load("Enemies/LifeBoxEdge", typeof(GameObject)),
            transform.position + new Vector3(0, 2f, -0.05f), Quaternion.identity, GameObject.FindWithTag("Level").transform);
        LifeBox = Instantiate((GameObject)Resources.Load("Enemies/LifeBox", typeof(GameObject)),
            transform.position + new Vector3(0, 2f, -0.05f), Quaternion.identity, GameObject.FindWithTag("Level").transform);
    }

    

    override protected void FixedUpdate()
    {
        base.FixedUpdate();
        LifeBoxEdge.transform.position = transform.position  + new Vector3(0, 2f, -0.05f);
        LifeBox.transform.position = transform.position  + new Vector3(-(1-currenthp/hp)*40/32, 2f, -0.05f);
        //LifeBox.transform.position = transform.position  + new Vector3(0, 2f, -0.05f);
        LifeBox.transform.localScale = new Vector3((currenthp/hp)*80, 4, 1);
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
            AudioManager.instance.PlayOneShot(FMODEvents.instance.explosionBig,transform.position);
            gameObject.SetActive(false);
            LifeBoxEdge.SetActive(false);
            LifeBox.SetActive(false);
            GameObject coin = Level.Coin10Pool.GetPooledObject();
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


using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    
    public bool visible = false;
    public Vector2 GetVector2D()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }

    protected virtual void FixedUpdate()
    {

        if (transform.position.x < Camera.main.orthographicSize * Camera.main.aspect + 2)
        {
            visible = true;
        }

    }

    
}

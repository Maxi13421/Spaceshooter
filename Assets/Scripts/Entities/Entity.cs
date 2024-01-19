
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Vector2 GetVector2D()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }
}

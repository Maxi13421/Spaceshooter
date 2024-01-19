using UnityEngine;

public abstract class EnemyProjectile : Projectile
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Player collideWith = other.GetComponent<Player>();
        if (collideWith != null)
        {
            gameObject.SetActive(false);
            collideWith.currenthp -= damage;


        }
    }
}

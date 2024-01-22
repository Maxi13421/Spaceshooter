using UnityEngine;

public abstract class EnemyProjectile : Projectile
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ObstacleTile"))
        {
            gameObject.SetActive(false);
            return;
        }
        Player collideWith = other.GetComponent<Player>();
        if (collideWith != null)
        {
            gameObject.SetActive(false);
            collideWith.currenthp -= damage;


        }
        Shield collideWith2 = other.GetComponent<Shield>();
        if (collideWith2 != null)
        {
            gameObject.SetActive(false);
            other.transform.parent.GetComponent<Player>().currenthp -= damage * (1- collideWith2.damageReduction);


        }

    }
}

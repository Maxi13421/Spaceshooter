using UnityEngine;

public abstract class EnemyProjectile : Projectile
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ObstacleTile"))
        {
            gameObject.SetActive(false);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.explosionSmall,transform.position);
            return;
        }
        Shield collideWith2 = other.GetComponent<Shield>();
        if (collideWith2 != null)
        {
            Debug.Log("Shield");
            gameObject.SetActive(false);
            other.transform.parent.GetComponent<Player>().currenthp -= damage * (1- collideWith2.damageReduction);


        }
        
        Player collideWith = other.GetComponent<Player>();
        if (collideWith != null)
        {
            gameObject.SetActive(false);
            collideWith.currenthp -= damage;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.explosionSmall,transform.position);


        }
        

    }
}

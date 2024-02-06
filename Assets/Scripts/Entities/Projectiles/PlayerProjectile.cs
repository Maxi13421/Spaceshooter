
using UnityEngine;

public abstract class PlayerProjectile : Projectile
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ObstacleTile"))
        {
            gameObject.SetActive(false);
            return;
        }
        Enemy collideWith = other.GetComponent<Enemy>();
        if (collideWith != null)
        {
            gameObject.SetActive(false);
            collideWith.currenthp -= damage *(GameObject.FindWithTag("GameSystem")
                                                  .GetComponent<GameSystem>()
                                                  .difficulty ==
                                              GameSystem.Difficulty.Easy
                ? GameObject.FindWithTag("GameSystem")
                    .GetComponent<GameSystem>()
                    .easyMultiplier
                : 1);
            AudioManager.instance.PlayLevelOneShot(FMODEvents.instance.explosionSmall,transform.position);


        }
    }
}

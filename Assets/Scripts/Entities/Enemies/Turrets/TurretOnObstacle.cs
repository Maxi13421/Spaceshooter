
using UnityEngine;

public class TurretOnObstacle : StandardTurret
{
    private static Sprite _spriteAsteroid;
    private static Sprite _spriteFireIce;
    private static Sprite _spriteNature;
    
    protected override void Awake()
    {
        base.Awake();
        if (_spriteAsteroid == null)
        {
            _spriteAsteroid = Resources.Load<Sprite>("Enemies/Turrets/TurretOnObstacleAsteroid");
            _spriteFireIce = Resources.Load<Sprite>("Enemies/Turrets/TurretOnObstacleFireIce");
            _spriteNature = Resources.Load<Sprite>("Enemies/Turrets/TurretOnObstacleNature");
        }
        switch (GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().colorScheme)
        {
            case GameSystem.ColorScheme.Asteroid:
                gameObject.GetComponent<SpriteRenderer>().sprite = _spriteAsteroid;
                break;
            case GameSystem.ColorScheme.FireIce:
                gameObject.GetComponent<SpriteRenderer>().sprite = _spriteFireIce;
                break;
            case GameSystem.ColorScheme.Nature:
                gameObject.GetComponent<SpriteRenderer>().sprite = _spriteNature;
                break;
        }
    }
}


using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Laser : Weapon
{
    public float Width = 0.4f;
    public float DamagePerSecond = 10;
    public GameObject LaserGraphic;

    private void Awake()
    {
        LaserGraphic = Instantiate((GameObject)Resources.Load("Projectile/Laser", typeof(GameObject)));
        LaserGraphic.SetActive(false);
    }

    private void Update()
    {
        if (GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().ZoomStatus != GameSystem.Zoom.Level)
        {
            LaserGraphic.SetActive(false);
        }
    }

    public override void Shoot()
    { 
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.parent.position, new Vector2(Width/2,Width/2), 0, Vector2.right,
            Camera.main.orthographicSize * Camera.main.aspect - transform.parent.position.x);

        float xEnd = GameSystem.MainCamera.orthographicSize * GameSystem.MainCamera.aspect + 1;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.CompareTag("ObstacleTile"))
            {
                xEnd = hits[i].collider.transform.position.x - 1.28f;
                break;
            }
            if (hits[i].collider.gameObject.GetComponent<Enemy>() != null)
            {
                hits[i].collider.gameObject.GetComponent<Enemy>().currenthp -= DamagePerSecond * Time.fixedDeltaTime;
                
            }
        }

        LaserGraphic.transform.position =
            new Vector3((transform.parent.position.x + xEnd)/2, transform.parent.position.y, 0);
        LaserGraphic.transform.localScale = new Vector3((xEnd - transform.parent.position.x) * 32, 8, 1);
        LaserGraphic.SetActive(true);
        
        
    
    }

    public override void StopShooting()
    {
        LaserGraphic.SetActive(false);
    }
}

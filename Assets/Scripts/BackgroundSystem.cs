using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSystem : MonoBehaviour
{
    private static Sprite[] _spritesStandard;
    private static Sprite[] _spritesFireIce;
    private static Sprite[] _spritesNature;
    private float nextSpawn = float.MinValue;
    private int lastPlanet = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        _spritesStandard = Resources.LoadAll<Sprite>("Background/Asteroid");
        _spritesFireIce = Resources.LoadAll<Sprite>("Background/FireIce");
        _spritesNature = Resources.LoadAll<Sprite>("Background/Nature");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-Time.deltaTime * GetSpeed(),0,0));

        if (Time.time > nextSpawn)
        {
            
            GameSystem.ColorScheme colorScheme =
                GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().colorScheme;
            int rand = Random.Range(0, 6);
            GameSystem.ColorScheme colour = GameSystem.ColorScheme.Asteroid;
            switch (colorScheme)
            {
                case GameSystem.ColorScheme.Asteroid:
                    if (rand < 4) colour = GameSystem.ColorScheme.Asteroid;
                    else if (rand < 5) colour = GameSystem.ColorScheme.FireIce;
                    else if (rand < 6) colour = GameSystem.ColorScheme.Nature;
                    break;
                case GameSystem.ColorScheme.FireIce:
                    if (rand < 1) colour = GameSystem.ColorScheme.Asteroid;
                    else if (rand < 5) colour = GameSystem.ColorScheme.FireIce;
                    else if (rand < 6) colour = GameSystem.ColorScheme.Nature;
                    break;
                case GameSystem.ColorScheme.Nature:
                    if (rand < 1) colour = GameSystem.ColorScheme.Asteroid;
                    else if (rand < 2) colour = GameSystem.ColorScheme.FireIce;
                    else if (rand < 6) colour = GameSystem.ColorScheme.Nature;
                    break;
            }

            GameObject g = null;
            if (GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().ZoomStatus != GameSystem.Zoom.Menu)
            {
                g = Instantiate(Resources.Load<GameObject>("Background/Planet"),new Vector3(40,Random.Range(-10f,10f),10),Quaternion.identity,transform);

            }
            else
            {
                g = Instantiate(Resources.Load<GameObject>("Background/Planet"),new Vector3(GameObject.FindWithTag("Player").transform.position.x+20,GameObject.FindWithTag("Player").transform.position.y+Random.Range(-4f,4f),10),Quaternion.identity,transform);

            }
            Debug.Log(colour);
            int index = Random.Range(0, 9);
            while (index / 3 == lastPlanet)
            {
                index = Random.Range(0, 9);
            }

            lastPlanet = index;
            switch (colour)
            {
                case GameSystem.ColorScheme.Asteroid:
                    g.GetComponent<SpriteRenderer>().sprite = _spritesStandard[index];
                    Debug.Log(g.GetComponent<SpriteRenderer>().sprite.ToString());
                    break;
                case GameSystem.ColorScheme.FireIce:
                    g.GetComponent<SpriteRenderer>().sprite = _spritesFireIce[index];
                    break;
                case GameSystem.ColorScheme.Nature:
                    g.GetComponent<SpriteRenderer>().sprite = _spritesNature[index];
                    break;
                    
                
            }
            nextSpawn = Time.time + Random.Range(6f, 15f);

        }
        
        
    }

    float GetSpeed()
    {
       return (10+(GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().LevelCount/6))/3;
    }
}

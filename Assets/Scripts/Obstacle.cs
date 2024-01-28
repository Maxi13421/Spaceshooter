using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Entity
{
    public int xLength;
    public int yLength;
    public bool dontRoundTop;
    public bool dontRoundBottom;
    public bool dontRoundLeft;
    public bool dontRoundRight;
    private bool _super;
    void Start()
    {
        
    }

    protected virtual void Awake()
    {
        if (transform.childCount == GetComponentsInChildren<Entity>().Length-1)
        {
            _super = false;
            ObstacleGenerator.GetObstacle(xLength, yLength, gameObject, transform.parent.GetComponent<LevelPiece>().ColorScheme, dontRoundTop,dontRoundBottom,dontRoundLeft,dontRoundRight);
        }
        else
        {
            Transform[] transforms = Array.FindAll(GetComponentsInChildren<Transform>(),
                entity => entity.GetComponent<Entity>() == null);
            _super = true;
            bool[][] tiles = new bool[16][];
            int width = 0;
            int height = 0;
            for (int aaa = 0;aaa<16;aaa++)
            {
                tiles[aaa] = new bool[16];
            }
            for (int aaa = 0; aaa < transforms.Length; aaa++)
            {
                int x = Mathf.RoundToInt((transforms[aaa].position.x - transform.position.x));
                int y = -Mathf.RoundToInt((transforms[aaa].position.y - transform.position.y));
                if (x/2 >= width)
                {
                    width = x/2 + 1;
                }
                if (y/2 >= height)
                {
                    height = y/2 + 1;
                }
                tiles[x/2][y/2] = true;

            }
            bool[][] trimmedTiles = new bool[width][];
            for (int aaa = 0;aaa<width;aaa++)
            {
                trimmedTiles[aaa] = new bool[height];
                Array.Copy(tiles[aaa], 0, trimmedTiles[aaa], 0, height);
            }

            ObstacleGenerator.GetSuperObstacle(trimmedTiles, gameObject,
                transform.parent.GetComponent<LevelPiece>().ColorScheme, dontRoundTop, dontRoundBottom, dontRoundLeft,
                dontRoundRight);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnDrawGizmos()
    {
        if (transform.childCount != 0)
        {
            for (int aaa = 0; aaa < transform.childCount; aaa++)
            {
                int x = Mathf.RoundToInt((transform.GetChild(aaa).position.x - transform.position.x));
                int y = -Mathf.RoundToInt((transform.GetChild(aaa).position.y - transform.position.y));
                Gizmos.DrawWireCube(
                    new Vector3(transform.position.x + x, transform.position.y - y, 0),
                    new Vector3(2f, 2f, 0));
                Gizmos.DrawSphere(transform.GetChild(aaa).position,0.1f);
                

            }
        }
        else
        {
            Gizmos.DrawWireCube(
                new Vector3(transform.position.x + xLength * 1f - 1f, transform.position.y - yLength * 1f + 1f, 0),
                new Vector3(2f * xLength, 2f * yLength, 0));
        }
    }
    
    
    public enum ColorScheme
    {
        Standard,
        LavaIce
    }
}

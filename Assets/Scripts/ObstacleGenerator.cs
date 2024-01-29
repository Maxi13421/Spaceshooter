

using UnityEditor;
using UnityEngine;

public class ObstacleGenerator
{
    private const int TileWidth = 16;
    private const int TileHeight = 16;

    private static Sprite[] _spritesStandard = Resources.LoadAll<Sprite>("tileset");
    private static Sprite[] _spritesFireIce = Resources.LoadAll<Sprite>("tilesetFireIce");
    private static Sprite[] _spritesNature = Resources.LoadAll<Sprite>("tilesetNature");
    
    
    public static GameObject GetObstacle(int width, int height, GameObject obstacle = null,
      bool dontRoundTop = false,
    bool dontRoundBottom = false,
    bool dontRoundLeft = false,
    bool dontRoundRight = false)
    {
        if (obstacle == null)
        {
            obstacle = Object.Instantiate((GameObject)Resources.Load("Obstacle", typeof(GameObject)));
        }
        int xOffset = Random.Range(0, TileWidth - width +1);
        int yOffset = Random.Range(0, TileHeight - height +1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject tile = Object.Instantiate((GameObject)Resources.Load("Tile", typeof(GameObject)),obstacle.transform);
                int spriteNumber = 16 * ((x + xOffset)%4) + y + yOffset + 64*16*((x+xOffset)/4);
                if (x == 0 && y == 0 && !dontRoundTop && !dontRoundLeft)
                {
                    spriteNumber += 64;
                }
                if (x == 0 && y == height-1 && !dontRoundBottom && !dontRoundLeft)
                {
                    spriteNumber += 128;
                }
                if (x == width-1 && y == 0 && !dontRoundTop && !dontRoundRight)
                {
                    spriteNumber += 256;
                }
                if (x == width-1 && y == height-1 && !dontRoundBottom && !dontRoundRight)
                {
                    spriteNumber += 512;
                }

                Sprite sprite = null;
                switch (GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().colorScheme)
                {
                    case GameSystem.ColorScheme.Asteroid:
                        sprite = _spritesStandard[spriteNumber];
                        break;
                    case GameSystem.ColorScheme.FireIce:
                        sprite = _spritesFireIce[spriteNumber];
                        break;
                    case GameSystem.ColorScheme.Nature:
                        sprite = _spritesNature[spriteNumber];
                        break;
                }
                tile.GetComponent<SpriteRenderer>().sprite = sprite;
                //tile.GetComponent<SpriteRenderer>().sortingOrder = 1;
                tile.transform.localPosition = new Vector3(0.5f * x , -0.5f * y, 0);
                
            }
        }

        obstacle.transform.localScale = new Vector3(4, 4, 1);
        
        return obstacle;
    }
    
    public static GameObject GetSuperObstacle(bool[][] tiles, GameObject obstacle = null,
      bool dontRoundTop = false,
    bool dontRoundBottom = false,
    bool dontRoundLeft = false,
    bool dontRoundRight = false)
    {
        if (obstacle == null)
        {
            obstacle = Object.Instantiate((GameObject)Resources.Load("Obstacle", typeof(GameObject)));
        }
        int xOffset = Random.Range(0, TileWidth - tiles.Length +1);
        int yOffset = Random.Range(0, TileHeight - tiles[0].Length +1);

        for (int x = 0; x < tiles.Length; x++)
        {
            for (int y = 0; y < tiles[0].Length; y++)
            {
                if (!tiles[x][y])
                {
                    continue;
                }
                GameObject tile = Object.Instantiate((GameObject)Resources.Load("Tile", typeof(GameObject)),obstacle.transform);
                int spriteNumber = 16 * ((x + xOffset)%4) + y + yOffset + 64*16*((x+xOffset)/4);
                if ((x == 0 || !tiles[x-1][y]) && (y == 0 || !tiles[x][y-1]) && !dontRoundTop && !dontRoundLeft)
                {
                    spriteNumber += 64;
                }
                if ((x == 0 || !tiles[x-1][y]) && (y == tiles[0].Length-1 || !tiles[x][y+1]) && !dontRoundBottom && !dontRoundLeft)
                {
                    spriteNumber += 128;
                }
                if ((x == tiles.Length-1 || !tiles[x+1][y]) && (y == 0 || !tiles[x][y-1]) && !dontRoundTop && !dontRoundRight)
                {
                    spriteNumber += 256;
                }
                if ((x == tiles.Length-1 || !tiles[x+1][y]) && (y == tiles[0].Length-1 || !tiles[x][y+1]) && !dontRoundBottom && !dontRoundRight)
                {
                    spriteNumber += 512;
                }

                Sprite sprite = null;
                switch (GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().colorScheme)
                {
                    case GameSystem.ColorScheme.Asteroid:
                        sprite = _spritesStandard[spriteNumber];
                        break;
                    case GameSystem.ColorScheme.FireIce:
                        sprite = _spritesFireIce[spriteNumber];
                        break;
                    case GameSystem.ColorScheme.Nature:
                        sprite = _spritesNature[spriteNumber];
                        break;
                }
                tile.GetComponent<SpriteRenderer>().sprite = sprite;
                //tile.GetComponent<SpriteRenderer>().sortingOrder = 1;
                tile.transform.localPosition = new Vector3(0.5f * x , -0.5f * y, 0);
                
            }
        }

        obstacle.transform.localScale = new Vector3(4, 4, 1);
        
        return obstacle;
    }
    
    
}

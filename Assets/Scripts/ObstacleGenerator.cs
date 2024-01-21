

using UnityEngine;

public class ObstacleGenerator
{
    private const int TileWidth = 8;
    private const int TileHeight = 8;

    private static Sprite[] sprites = Resources.LoadAll<Sprite>("tileset");
    
    public static GameObject GetObstacle(int width, int height)
    {
        GameObject obstacle = new GameObject("Obstacle");
        int xOffset = Random.Range(0, TileWidth - width +1);
        int yOffset = Random.Range(0, TileHeight - height +1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject tile = Object.Instantiate((GameObject)Resources.Load("Tile", typeof(GameObject)));
                int spriteNumber = 8 * (x + xOffset) + y + yOffset;
                if (x == 0 && y == 0)
                {
                    spriteNumber += 64;
                }
                if (x == 0 && y == height-1)
                {
                    spriteNumber += 128;
                }
                if (x == width-1 && y == 0)
                {
                    spriteNumber += 256;
                }
                if (x == width-1 && y == height-1)
                {
                    spriteNumber += 512;
                }
                Sprite sprite = sprites[spriteNumber];
                tile.GetComponent<SpriteRenderer>().sprite = sprite;
                //tile.GetComponent<SpriteRenderer>().sortingOrder = 1;
                tile.transform.position = new Vector3(0.32f * x , -0.32f * y, 0);
                tile.transform.SetParent(obstacle.transform);
                
            }
        }

        obstacle.transform.localScale = new Vector3(8, 8, 1);
        
        return obstacle;
    }
}

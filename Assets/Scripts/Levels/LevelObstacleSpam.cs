using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObstacleSpam : Level
{
    

    protected override int GetNextTileType()
    {
        int ran = Random.Range(0, 9);
        if (ran < 5) return 0;
        if (ran < 6) return 1;
        if (ran < 7) return 2;
        if (ran < 8) return 3;
        if (ran < 9) return 4;
        return 0;
    }
}

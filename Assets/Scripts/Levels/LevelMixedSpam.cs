

using UnityEngine;

public class LevelMixedSpam : Level
{
    protected override int GetNextTileType()
    {
        int ran = Random.Range(0, 6);
        if (ran < 2) return 0;
        if (ran < 3) return 1;
        if (ran < 4) return 2;
        if (ran < 5) return 3;
        if (ran < 6) return 4;
        return 0;
    }
}

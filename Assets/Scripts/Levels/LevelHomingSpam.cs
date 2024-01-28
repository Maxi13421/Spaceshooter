using UnityEngine;

public class LevelHomingSpam : Level
{
    protected override int GetNextTileType()
    {
        int ran = Random.Range(0, 9);
        if (ran < 2) return 0;
        if (ran < 3) return 1;
        if (ran < 4) return 2;
        if (ran < 5) return 3;
        if (ran < 9) return 4;
        return 0;
    }
}

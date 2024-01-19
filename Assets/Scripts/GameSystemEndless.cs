using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemEndless : GameSystem
{
    private GameObject _level;

    public int levelCount = 1;
    void Start()
    {
        
    }

    private void Awake()
    {
        _level = (GameObject)Resources.Load("Levels/TestLevel", typeof(GameObject));
        GameObject level = Instantiate(_level);
    }

    protected override void Update()
    {
        base.Update();
    }
}

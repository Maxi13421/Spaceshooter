using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemEndless : MonoBehaviour
{
    private GameObject _level;

    public int levelCount = 1;
    void Start()
    {
        
    }

    private void Awake()
    {
        _level = (GameObject)Resources.Load("TestLevel", typeof(GameObject));
        GameObject level = Instantiate(_level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

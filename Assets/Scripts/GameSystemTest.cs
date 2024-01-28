using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameSystemTest : GameSystem
{
    public GameObject level;
    private AudioSource _audioSource;

    public int levelCount = 1;
    

    protected void Awake()
    {
        Instantiate(this.level);
        
    }
}

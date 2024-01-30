using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float damageReduction;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().ZoomStatus != GameSystem.Zoom.Level)
        {
            gameObject.SetActive(false);
        }
    }
    

    
}

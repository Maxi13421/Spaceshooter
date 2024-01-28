using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelPiece : MonoBehaviour
{
    public bool[] connectionLeft = new bool[5];
    public bool[] connectionRight = new bool[5];
    public Obstacle.ColorScheme ColorScheme;

    public bool startTogether;
    protected bool Visible;
    protected void Awake()
    {
        ColorScheme = transform.parent.GetComponent<Level>().ColorScheme;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void FixedUpdate()
    {
        if (startTogether)
        {
            Visible = transform.position.x < Camera.main.orthographicSize * Camera.main.aspect + 2;
        }

        if (Visible)
        {
            for (int aaa = 0; aaa < transform.childCount; aaa++)
            {
                if (transform.GetChild(aaa).GetComponent<Entity>() != null)
                {
                    transform.GetChild(aaa).GetComponent<Entity>().visible = true;
                }
            }
        }
            
        
    }
}


using System;
using Unity.VisualScripting;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{

    public bool finished = false;

    

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(transform.position.x,Camera.main.orthographicSize,0),new Vector3(transform.position.x,-Camera.main.orthographicSize,0));
    }
}

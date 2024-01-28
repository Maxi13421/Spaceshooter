using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zchfvy.Plus;

public class CirclingObstacle : Obstacle
{
    public GameObject center;
    public float travelSpeed;
    public bool clockwise;
    protected int CurCheckpoint = 0;
    protected Vector3 StartPosition;
    protected Vector3 LevelStartPosition;
    protected void Start()
    {
        StartPosition = transform.position;
        LevelStartPosition = transform.parent.position;
    }

    // Update is called once per frame
    override protected void FixedUpdate()
    {
        base.FixedUpdate();
        if (visible)
        {
            Vector3 position = transform.position;
            Vector3 vector3 = Quaternion.AngleAxis((clockwise?-1:1)* (float)(360 * travelSpeed / Vector3.Distance(transform.position,center.transform.position) / 2/ Math.PI * Time.deltaTime), Vector3.forward) * (position-center.transform.position);
            transform.position = center.transform.position + vector3;
        }
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        GizmosPlus.Circle(center.transform.position, new Vector3(0,0,Vector3.Distance(transform.position,center.transform.position)));
        Gizmos.color = Color.white;
        for (int aaa = 0; aaa < 4; aaa++)
        {
            Gizmos.DrawWireCube(new Vector3(transform.position.x+xLength*1f-1f,transform.position.y-yLength*1f+1f,0),new Vector3(2f*xLength,2f*yLength,0));
        }
    }
}

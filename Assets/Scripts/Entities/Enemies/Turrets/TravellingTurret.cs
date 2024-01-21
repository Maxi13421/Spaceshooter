using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravellingTurret : Turret
{
    public Vector3[] checkpoints;
    public float travelSpeed;
    protected int CurCheckpoint = 0;
    protected Vector3 StartPosition;
    protected Vector3 LevelStartPosition;
    protected override void Start()
    {
        base.Start();
        StartPosition = transform.position;
        LevelStartPosition = transform.parent.position;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Vector3.Distance(transform.position-transform.parent.position,checkpoints[CurCheckpoint]+StartPosition-LevelStartPosition) <
            Time.fixedDeltaTime * travelSpeed+0.1)
        {
            CurCheckpoint = (CurCheckpoint + 1) % checkpoints.Length;
        }
        transform.Translate(travelSpeed*Time.fixedDeltaTime*(checkpoints[CurCheckpoint]-checkpoints[(CurCheckpoint-1+checkpoints.Length)%checkpoints.Length]).normalized,Space.World);
    }
}

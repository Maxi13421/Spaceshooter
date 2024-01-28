using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravellingObstacle : Obstacle
{
    public Vector3[] checkpoints;
    public float travelSpeed;
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
            if (Vector3.Distance(transform.position - transform.parent.position,
                    checkpoints[CurCheckpoint] + StartPosition - LevelStartPosition) <
                Time.fixedDeltaTime * travelSpeed + 0.1)
            {
                CurCheckpoint = (CurCheckpoint + 1) % checkpoints.Length;
            }

            transform.Translate(
                travelSpeed * Time.fixedDeltaTime * (checkpoints[CurCheckpoint] -
                                                     checkpoints[
                                                         (CurCheckpoint - 1 + checkpoints.Length) % checkpoints.Length])
                .normalized, Space.World);
        }
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int aaa = 0; aaa < checkpoints.Length; aaa++)
        {
            Gizmos.DrawLine(transform.position+checkpoints[aaa], transform.position+checkpoints[(aaa+1)%checkpoints.Length]);
        }
        Gizmos.color = Color.white;
        for (int aaa = 0; aaa < checkpoints.Length; aaa++)
        {
            Gizmos.DrawWireCube(new Vector3(transform.position.x+xLength*1f-1f,transform.position.y-yLength*1f+1f,0) + checkpoints[aaa],new Vector3(2f*xLength,2f*yLength,0));
        }
    }
}

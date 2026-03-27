using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraDetection : Detection
{
    private bool enemyInZone;
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override void DetectedPlayer(bool isDetect)
    {
        Debug.Log("DetectedPlayer Appelé");
        if (isDetect = true)
        {
            enemyInZone = true;
            Debug.Log("isDetect = " + isDetect);
        }
        else
        {
            enemyInZone = false;
            Debug.Log("isDetect = " + isDetect);
        }
    }

    private void FixedUpdate()
    {
        if (enemyInZone)
        {
            gameObject.transform.parent.LookAt(player.transform);
            TestIfWall();
        }
    }
}

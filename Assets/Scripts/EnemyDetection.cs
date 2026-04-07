using System.Collections;
using UnityEngine;

public class EnemyDetection : Detection
{
    [SerializeField] private GameObject detectionIndicator;
    private bool _enemyDetect;
    public bool cameraDetected = false;
    //public Vector3 lastPlayerPos;
    
    public IEnumerator DetectionDelay()
    {
        detectionIndicator.SetActive(false);
        canDetect = false;
        yield return new WaitForSeconds(5);
        canDetect = true;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            DetectedPlayer(TestIfWall());
        }
    }

    public override void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            DetectedPlayer(TestIfWall());
        }
    }

    public override void DetectedPlayer(bool isDetect)
    {
        if (!isDetect)
        {
            _enemyDetect = true;
        }
        else
        {
            _enemyDetect = false;
        }
    }
    
    public void GoToLastPlayerPos(Vector3 lastPlayerPos)
    {
        _pathfindingsScript.PursuitPlayer(lastPlayerPos);
    }

    public override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            DetectedPlayer(true);
        }
    }

    private void FixedUpdate()
    {
        if (_enemyDetect && canDetect)
        {
            playerPosition = GetPlayerPos();
            _pathfindingsScript.PursuitPlayer(GetPlayerPos());
            detectionIndicator.SetActive(true);
        }
    }
}

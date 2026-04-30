using System.Collections;
using UnityEngine;

public class EnemyDetection : Detection
{
    [SerializeField] private GameObject detectionIndicator;
    
    public IEnumerator DetectionDelay()
    {
        detectionIndicator.SetActive(false);
        canDetect = false;
        yield return new WaitForSeconds(5);
        canDetect = true;
    }
    

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && _playerManager.IsTargetable())
        {
            StopAllCoroutines();
            DetectedPlayer(TestIfWall());
        }
    }

    public override void DetectedPlayer(bool notDetect)
    {
        if (!notDetect && _playerManager.IsTargetable())
        {
            _enemyDetect = true;
            _pathfindingsScript._detectedPlayer = true;
        }
        else
        {
            _enemyDetect = false;
            _pathfindingsScript._detectedPlayer = false;
        }
    }
    

    public override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(TimeBeforeEscapePlayer());
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
        else
        {
            detectionIndicator.SetActive(false);
        }
    }
}

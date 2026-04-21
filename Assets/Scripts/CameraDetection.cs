using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraDetection : Detection
{
    private bool enemyInZone;
    private EnemyDetection _enemyDetection;
    private InfluenceZone influenceZone;
    private EnemyDetection test2;
    private StateCamera _stateCamera;
    [SerializeField] private float coyoteTimeExitCamera = 0.3f;

    private IEnumerator CoyoteTimeExitCamera()
    {
        yield return new WaitForSeconds(coyoteTimeExitCamera);
        enemyInZone = false;
        StartCoroutine(_stateCamera.WaitChangeState());
    }
    
    public override void Init()
    {
        _stateCamera = GetComponentInParent<StateCamera>();
        InfluenceZone influenceZone = GetComponentInChildren<InfluenceZone>();
        //EnemyDetection test2 = test.GetComponentInChildren<EnemyDetection>();
    }
    
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // à mettre dans enemy detection +
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
            StopAllCoroutines();
            enemyInZone = true;
            GameObject test = CallEnemy();
            if (test is not null)
            {
                test2.cameraDetected = true;
            }
        }
        else
        {
            Debug.Log("on le voit plus");
            StartCoroutine(CoyoteTimeExitCamera());
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            DetectedPlayer(true);
        }
    }

    
    private GameObject CallEnemy()
    {
        float distanceMin = int.MaxValue;
        GameObject nearestEnemy = null;
        if (influenceZone.enemyInInfluenceZone.Count != 0)
        {
            foreach (GameObject enemy in influenceZone.enemyInInfluenceZone)
            {
                float distance = Vector3.Distance(playerPosition, enemy.transform.position);
                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    nearestEnemy = enemy;
                }
            }
        }
        return nearestEnemy;
    }
    
    private void FixedUpdate()
    {
        Debug.Log("EnemyInZone " + enemyInZone);
        if (enemyInZone)
        {
            playerPosition = GetPlayerPos();
            //gameObject.transform.parent.LookAt(player.transform);
            DetectedPlayer(TestIfWall());
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class CameraDetection : Detection
{
    private bool enemyInZone;
    private EnemyDetection _enemyDetection;
    private InfluenceZone influenceZone;
    private EnemyDetection test2;

    public override void Init()
    {
        InfluenceZone influenceZone = GetComponentInChildren<InfluenceZone>();
        EnemyDetection test2 = test.GetComponentInChildren<EnemyDetection>();
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
            enemyInZone = true;
            GameObject test = CallEnemy();
            Debug.Log("test c'est un " + test);
            if (test != null)
            {
                test2.cameraDetected = true;
            }
            
        }
        else
        {
            enemyInZone = false;
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
        float distanceMin = 100000000;
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
        if (enemyInZone)
        {
            playerPosition = GetPlayerPos();
            gameObject.transform.parent.LookAt(player.transform);
            DetectedPlayer(TestIfWall());
        }
    }
}

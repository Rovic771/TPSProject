using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraDetection : Detection
{
    private bool enemyInZone;
    private EnemyDetection _enemyDetection;
    private InfluenceZone influenceZone;
    private StateCamera _stateCamera;
    private MeshRenderer areaDetectionMaterial;
    [SerializeField] private Material detectedPlayerMat;
    [SerializeField] private Material notDetectedPlayerMat;
    [SerializeField] private float coyoteTimeExitCamera = 0.3f;

    private IEnumerator CoyoteTimeExitCamera()
    {
        yield return new WaitForSeconds(coyoteTimeExitCamera);
        enemyInZone = false;
        areaDetectionMaterial.material = notDetectedPlayerMat;
        StartCoroutine(_stateCamera.WaitChangeState());
    }
    
    public override void Init()
    {
        _stateCamera = GetComponentInParent<StateCamera>();
        influenceZone = GetComponentInChildren<InfluenceZone>();
        areaDetectionMaterial = GetComponent<MeshRenderer>();
    }
    
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            DetectedPlayer(TestIfWall());
        }
    }
    
    public override void DetectedPlayer(bool notDetect)
    {
        if (!notDetect && _playerManager.IsTargetable())
        {
            playerPosition = GetPlayerPos();
            areaDetectionMaterial.material = detectedPlayerMat;
            StopAllCoroutines();
            enemyInZone = true;
            GameObject nearestEnemy = CallEnemy();
            
            if (nearestEnemy is not null)
            {
                nearestEnemy.GetComponent<Pathfindings>().GoToLastPlayerPos(player.transform.position);
            }
        }
        else
        {
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
        if (enemyInZone)
        {
            playerPosition = GetPlayerPos();
            gameObject.transform.parent.LookAt(player.transform);
            DetectedPlayer(TestIfWall());
        }
    }
}

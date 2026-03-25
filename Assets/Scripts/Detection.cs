using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject detectionIndicator;
    [SerializeField] Color rayColor = Color.green;
    [SerializeField] private Transform rayCastOrigin;
    [SerializeField] private LayerMask whatToHit;
    private Pathfindings _pathfindingsScript;
    public bool canDetect = true;
    private Vector3 direction;
    
    
    public IEnumerator DetectionDelay()
    {
        detectionIndicator.SetActive(false);
        canDetect = false;
        yield return new WaitForSeconds(5);
        canDetect = true;
    }
    
    private void Start()
    {
        _pathfindingsScript = GetComponentInParent<Pathfindings>();
    }

    private void OnTriggerEnter(Collider other)
    {
        {
            if (canDetect)
            {
                TestIfWall();
            }
        }
    }
    
    private Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }

    private void TestIfWall()
    {
        direction = (player.transform.position - rayCastOrigin.position);
        Vector3 directionFlat = new Vector3(direction.x, direction.y +0.5f, direction.z);
        Ray raycast = new Ray(rayCastOrigin.position, directionFlat);
        RaycastHit hit;
        Debug.DrawRay(rayCastOrigin.position, directionFlat, rayColor, 5 );
        if (Physics.Raycast(raycast, out hit, whatToHit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _pathfindingsScript.PursuitPlayer(GetPlayerPos());
                detectionIndicator.SetActive(true);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class Detection : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] Color rayColor = Color.green;
    [SerializeField] private Transform rayCastOrigin;
    [SerializeField] private LayerMask whatToHit;
    private Pathfindings _pathfindingsScript;
    public bool canDetect = true;
    private Vector3 direction;
    
    private void Start()
    {
        _pathfindingsScript = GetComponentInParent<Pathfindings>();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // à mettre dans enemy detection canDetect && 
        {
            TestIfWall();
        }
    }

    public abstract void DetectedPlayer(bool _isDetect);

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Joueur Sorti");
            DetectedPlayer(false);
        }
    }

    private Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }

    protected bool TestIfWall()
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
                DetectedPlayer(true);
                return false;
                //_pathfindingsScript.PursuitPlayer(GetPlayerPos());
                //detectionIndicator.SetActive(true);
            }
        }
        DetectedPlayer(false);
        return true;
    }
}

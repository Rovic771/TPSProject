using System;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceZone : MonoBehaviour
{
    public List<GameObject> enemyInInfluenceZone =  new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemyInInfluenceZone.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemyInInfluenceZone.Remove(other.gameObject);
        }
    }
}

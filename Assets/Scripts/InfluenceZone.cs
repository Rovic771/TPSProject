using System;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceZone : MonoBehaviour
{
    public List<GameObject> enemyInInfluenceZone =  new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("objet entré dans la zone " + other.gameObject);
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Enemy entré");
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

using System;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceZone : MonoBehaviour
{
    public List<GameObject> enemyInInfluenceZone =  new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("objet entré dans la zone " + other.gameObject);
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyInInfluenceZone.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemyInInfluenceZone.Remove(other.gameObject);
        }
    }
}

using System.Collections;
using UnityEngine;

public class EnemyDetection : Detection
{
    [SerializeField] private GameObject detectionIndicator;
    
    public IEnumerator DetectionDelay() // classe Dectection Enemy
    {
        detectionIndicator.SetActive(false);
        canDetect = false;
        yield return new WaitForSeconds(5);
        canDetect = true;
    }

    public override void DetectedPlayer(bool isDetect)
    {
        throw new System.NotImplementedException();
    }
}

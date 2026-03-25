using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject dieIndicator;
    
    public IEnumerator DieDelay()
    {
        dieIndicator.SetActive(true);
        yield return new WaitForSeconds(5);
        dieIndicator.SetActive(false);
    }
}

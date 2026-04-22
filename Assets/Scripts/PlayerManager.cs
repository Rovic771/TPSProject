using System;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject dieIndicator;

    public enum PlayerState
    {
        alive,
        dead,
    }

    public PlayerState currentState;
    
    public IEnumerator DieDelay()
    {
        dieIndicator.SetActive(true);
        currentState = PlayerState.dead;
        yield return new WaitForSeconds(5);
        dieIndicator.SetActive(false);
        currentState = PlayerState.alive;
    }

    public bool IsTargetable()
    {
        return currentState == PlayerState.alive;
    }
    
    private void Update()
    {
        switch (currentState)
        {
            case PlayerState.alive:
                Debug.Log("Alive");
                break;
            case PlayerState.dead:
                Debug.Log("Dead");
                break;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject dieIndicator;
    [SerializeField] private UIManager UIManager;
    [SerializeField] private GameObject exitDoor;
    private PlayerController player;
    private UIManager uiManager;
    List<GameObject> objectsCanCollect = new List<GameObject>();
    public bool haveKey = false;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Recoltable"))
        {
            UIManager.UpdateAffichage(other.gameObject, true);
            player.canCollect = true;
            objectsCanCollect.Add(other.gameObject);
        }

        if (other.gameObject.CompareTag("FindRoom"))
        {
            GameManager.instance.advancement++;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("End"))
        {
            UIManager.WinMenu();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Recoltable"))
        {
            UIManager.UpdateAffichage(other.gameObject, false);
            player.canCollect = false;
            objectsCanCollect.Remove(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Exit") && haveKey)
        {
            UIManager.exitText.gameObject.SetActive(true);
            player.canOpen = true;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            UIManager.DeathMenu();
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Exit"))
        {
            UIManager.exitText.gameObject.SetActive(false);
            player.canOpen = false;
        }
    }

    public void Collect()
    {
        if (objectsCanCollect.Count > 0)
        {
            GameObject objectToDestroy = objectsCanCollect[0];
            Debug.Log("Collecting " + objectToDestroy.name);
            objectsCanCollect.Remove(objectToDestroy);
            if (objectsCanCollect.Count == 0)
            {
                player.canCollect = false;
                UIManager.UpdateAffichage(objectToDestroy, false);
                haveKey = true;
            }
            Destroy(objectToDestroy);
        }
    }

    public void Open()
    {
        exitDoor.SetActive(false);
    }

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

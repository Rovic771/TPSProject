using System;
using TMPro;
using UnityEngine;

public class recoltable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI grabText;
    private PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            grabText.text = "E pour ramasser " + gameObject.name;
            grabText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))  grabText.gameObject.SetActive(false);
    }
    
    // en vrai c mieux de mettre tout ça dans le player car là c juste chiant
}

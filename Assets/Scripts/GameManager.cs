using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private UIManager uiManager;
    public int advancement = 1;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    private void FixedUpdate()
    {
        switch (advancement)
        {
            case 2:
                uiManager.UpdateObjectifs("Récupérez la clé");
                break;
            case 3:
                uiManager.UpdateObjectifs("Trouvez la porte jaune");
                break;
            case 4:
                uiManager.UpdateObjectifs("Echappez vous");
                break;
        }
    }
}

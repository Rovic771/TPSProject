using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI grabText;
    [SerializeField] public TextMeshProUGUI exitText;
    [SerializeField] private TextMeshProUGUI objectifs;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject basicUi;

    public void UpdateAffichage(GameObject objet, bool displayState)
    {
        grabText.text = "E pour ramasser " + objet.name;
        grabText.gameObject.SetActive(displayState);
    }

    public void UpdateObjectifs(string obj)
    {
        objectifs.text = obj;
    }

    public void WinMenu()
    {
        basicUi.SetActive(false);
        winMenu.SetActive(true);
    }

    public void DeathMenu()
    {
        basicUi.SetActive(false);
        deathMenu.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

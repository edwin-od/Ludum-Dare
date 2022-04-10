using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenuPanel;
    public GameObject optionsPanel;


    private void Start()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Hugo Scene");
    }

    public void OptionsButton()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void BackOptionsButton()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

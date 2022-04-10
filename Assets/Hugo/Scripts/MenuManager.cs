using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject chooseLevels;

    public GameObject imageOptions;
    public GameObject imagePlay;

    public Slider[] sliders;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindObjectOfType<DontDestroyMusic>() != null)
            GameObject.FindObjectOfType<DontDestroyMusic>().StopMusic();

        if(SceneManager.GetActiveScene().name == "Menu Scene" && SceneManager.GetActiveScene().name == "Main Menu")
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
            chooseLevels.SetActive(false);
        }
        
    }

    public void MyLoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true); 
        imageOptions.SetActive(false);
    }

    public void GoToLevels()
    {
        mainMenu.SetActive(false);
        chooseLevels.SetActive(true);
        imagePlay.SetActive(false);
    }

    public void GoToMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        chooseLevels.SetActive(false);
    }

}

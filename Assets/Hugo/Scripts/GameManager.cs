using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject _panelPause;
    public GameObject pause;
    public GameObject options;

    public GameObject pauseButton;
    public GameObject resumeButton;

    public bool _isPause;

    public GameObject[] unselectedAmmoUI;
    public GameObject[] selectedAmmoUI;

    public Slider[] sliders;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        _panelPause.SetActive(false);

        pause.SetActive(true);
        options.SetActive(false);

        pauseButton.SetActive(true);
        resumeButton.SetActive(false);

        if (!GameObject.FindObjectOfType<DontDestroyMusic>().GetComponent<AudioSource>().isPlaying)
            GameObject.FindObjectOfType<DontDestroyMusic>().PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause(!_isPause);

        if (Input.GetKeyDown(KeyCode.R))
            ReloadScene();
    }
    public void MyLoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void Pause(bool pause)
    {
        _isPause = pause;

        if (pause)
        {
            Time.timeScale = 0;
            pauseButton.SetActive(false);
            resumeButton.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseButton.SetActive(true);
            resumeButton.SetActive(false);
        }

        _panelPause.SetActive(_isPause);
    }

    public void ResumeButton()
    {
        Pause(false);
    }

    public void ReloadScene()
    {
        MyLoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton()
    {
        MyLoadScene("Main Menu");
    }

    public void SetActiveFalse(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void GoToPause()
    {
        pause.SetActive(true);
        options.SetActive(false);
    }

    public void GoToOptions()
    {
        pause.SetActive(false);
        options.SetActive(true);
    }
}

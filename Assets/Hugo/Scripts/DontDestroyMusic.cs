using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyMusic : MonoBehaviour
{

    private static DontDestroyMusic musicInstance;

    void Awake()
    {

        DontDestroyOnLoad(this);

        if(musicInstance == null)
            musicInstance = this;
        else
            Destroy(gameObject);

        
    }

    public void PlayMusic()
    {
        musicInstance.GetComponent<AudioSource>().Play();
    }

    public void StopMusic()
    {
        musicInstance.GetComponent<AudioSource>().Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameManager gameManager;
    public MenuManager menuManager;

    void Start()
    {
        if(gameManager != null)
        {
            float volumeValue;
            bool result1 = audioMixer.GetFloat("volume", out volumeValue);
            if (result1)
                gameManager.sliders[0].value = volumeValue;

            float musicValue;
            bool result2 = audioMixer.GetFloat("music", out musicValue); 
            if (result2)
                gameManager.sliders[1].value = musicValue;

            float effectsValue;
            bool result3 = audioMixer.GetFloat("effects", out effectsValue);
            if (result3)
                gameManager.sliders[2].value = effectsValue;
        }

        if (menuManager != null)
        {
            float volumeValue;
            bool result1 = audioMixer.GetFloat("volume", out volumeValue);
            if (result1)
                menuManager.sliders[0].value = volumeValue;

            float musicValue;
            bool result2 = audioMixer.GetFloat("music", out musicValue);
            if (result2)
                menuManager.sliders[1].value = musicValue;

            float effectsValue;
            bool result3 = audioMixer.GetFloat("effects", out effectsValue);
            if (result3)
                menuManager.sliders[2].value = effectsValue;
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetMusic(float music)
    {
        audioMixer.SetFloat("music", music);
    }

    public void SetEffects(float effects)
    {
        audioMixer.SetFloat("effects", effects);
    }

    public void PlayEffect(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.PlayOneShot(audioClip);
    }
    
    public void PlayEffectOnLoop(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void StopEffectOnLoop(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.Stop();
    }
}

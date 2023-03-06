using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //rende possibile referenziare AudioManager in maniera piu accessibile
    public static AudioManager instance;
    private void Awake()
    {
        instance = this;   
    }

    //variabili
    public GameObject soundObject;
    public GameObject currentMusicObject;

    [Header("SFX")]
    public AudioClip sfx_lione;

    [Header("Musics")]
    public AudioClip music_ziopeppe;
    

    #region sfx
    public void PlaySFX(string name)
    {
        switch(name)
        {
            //case per ogni audio del gioco
            case "Ricchione":
                SoundObjectCreation(sfx_lione);
                break;
            default:
                break;
        }
    }

    void SoundObjectCreation(AudioClip clip)
    {
        GameObject newObject = Instantiate(soundObject,transform);
        newObject.GetComponent<AudioSource>().clip = clip;
        newObject.GetComponent<AudioSource>().Play();
    }
    #endregion

    #region music

    public void PlayMusic(string name)
    { 
        switch(name)
        {
            case "ZioPeppe":
                MusicObjectCreation(music_ziopeppe);
                break; 
            default:
                break;  
        }
    }

    void MusicObjectCreation(AudioClip clip)
    {
        if (currentMusicObject)
            Destroy(currentMusicObject);
        currentMusicObject = Instantiate(soundObject, transform);
        currentMusicObject.GetComponent<AudioSource>().clip = clip;
        currentMusicObject.GetComponent<AudioSource>().loop = true;
        currentMusicObject.GetComponent<AudioSource>().Play();
    }
    #endregion
}

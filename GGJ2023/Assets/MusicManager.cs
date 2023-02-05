using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip title;
    [SerializeField] private AudioClip main;
    [SerializeField] private AudioClip gameOver;

    public void PlayTitle()
    {
        musicSource.clip = title;
        musicSource.volume = 1;
        musicSource.Play();
    }
    public void PlayMain()
    {
        musicSource.clip = main;
        musicSource.volume = 1;
        musicSource.Play();
    }
    public void PlayGameOver()
    {
        musicSource.clip = gameOver;
        musicSource.volume = 1;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    
    
    
    
}

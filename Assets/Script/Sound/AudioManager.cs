using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] Sound[] musicSounds;
    [SerializeField] AudioSource musicSource;


    private void Awake()
    {
        if(Instance == null)
        { 
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Menu");
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.Name == name);

        if (s != null) 
        {
            musicSource.clip = s.Clip;
            musicSource.Play();
        }
    }
}

[Serializable]
public class Sound
{
    [SerializeField] string name;
    [SerializeField] AudioClip clip;

    public string Name => name;
    public AudioClip Clip => clip;
}

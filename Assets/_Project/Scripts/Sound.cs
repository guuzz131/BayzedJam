using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sound : MonoBehaviour
{
    public static Sound Instance;
    
    [SerializeField] private AudioSource source;

    [SerializeField] private AudioClip[] audioClips;

    private void Awake()
    {
        Instance = this;
        
    }

    public void Play(int index)
    {
        source.PlayOneShot(audioClips[index]);
    }
}

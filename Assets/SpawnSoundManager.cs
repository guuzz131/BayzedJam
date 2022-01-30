using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnSoundManager : MonoBehaviour
{
    public GameObject soundManager;
    void Start()
    {
        GameObject newSoundManager = Instantiate(soundManager);
        DontDestroyOnLoad(newSoundManager);
        SceneManager.LoadScene("Menu");
    }

    
}

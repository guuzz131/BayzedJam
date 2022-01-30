using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Animation animation;
    [SerializeField] private AnimationClip fadeOut;
    [SerializeField] private GameObject image;

    private void Awake()
    {
        image.SetActive(true);
        StartCoroutine(DisableObject());
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        StartCoroutine(ToGame());
        image.SetActive(true);
        animation.clip = fadeOut;
        animation.Play();
    }

    public void BackToMenu()
    {
        StartCoroutine(ToMenu());
        image.SetActive(true);
        animation.clip = fadeOut;
        animation.Play();
    }

    private IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(1f);
        image.SetActive(false);
    }

    private IEnumerator ToMenu()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Menu");
    }
    
    private IEnumerator ToGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game");
    }
}

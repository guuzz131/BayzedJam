using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    [SerializeField] Text highscoreText;

    private void Update()
    {
        highscoreText.text = "" + PlayerPrefs.GetInt("Highscore");
    }
}

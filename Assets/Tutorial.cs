using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Text text;
    [TextArea(3, 10)] [SerializeField] private string[] messages;
    private int index;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Highscore") > 0)
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
        else
        {
            Time.timeScale = 0f;
            OnInteract();
        }
    }
    
    public void OnInteract()
    {
        //Sound.Instance.Play(5);
        if (index >= messages.Length - 1)
        {
            Time.timeScale = 1f;
            Destroy(gameObject);
        }
        text.text = messages[index];
        index++;
    }
}

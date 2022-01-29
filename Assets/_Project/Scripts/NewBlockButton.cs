using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NewBlockButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttons0 = new List<GameObject>();
    [SerializeField] private List<GameObject> buttons1 = new List<GameObject>();

    [SerializeField] private AnimationClip clipIn;
    [SerializeField] private AnimationClip clipOut;


    private void Awake()
    {
        ShowNewBlockOptions(false, null);
        ShowNewBlockOptions(true, null);
    }

    private void ShowNewBlockOptions(bool left, GameObject buttonToTurnOff)
    {
        if (buttonToTurnOff != null) buttonToTurnOff.SetActive(false);
        if (left)
        {
            int pickedButton0 = Random.Range(0, buttons0.Count);
            buttons0[pickedButton0].SetActive(true);
        }

        if (!left)
        {
            int pickedButton1 = Random.Range(0, buttons1.Count);
            buttons1[pickedButton1].SetActive(true);
        }
        
    }

    public void OnLeftInteract(GameObject button)
    {
        ShowNewBlockOptions(true, button);
        GetComponent<Animation>().clip = clipOut;
        GetComponent<Animation>().Play();
    }

    public void OnRightInteract(GameObject button)
    {
        ShowNewBlockOptions(false, button);
        GetComponent<Animation>().clip = clipOut;
        GetComponent<Animation>().Play();
    }

    public void ExtendMenu()
    {
        GetComponent<Animation>().clip = clipIn;
        GetComponent<Animation>().Play();
    }

}

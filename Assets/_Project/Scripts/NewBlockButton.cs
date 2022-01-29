using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NewBlockButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttons0 = new List<GameObject>();
    [SerializeField] private List<GameObject> buttons1 = new List<GameObject>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShowNewBlockOptions();
        }
    }

    private void ShowNewBlockOptions()
    {
        int pickedButton0 = Random.Range(0, buttons0.Count);
        buttons0[pickedButton0].SetActive(true);
        
        int pickedButton1 = Random.Range(0, buttons1.Count);
        buttons1[pickedButton1].SetActive(true);
    }

    public void Disable(GameObject button) => button.SetActive(false);
}

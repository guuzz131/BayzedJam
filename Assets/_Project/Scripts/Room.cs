using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    [SerializeField] private float yWeightMultiplier;
    [SerializeField] private TextMesh weightText;
    public float currentWeight;

    private void Awake()
    {
        FindObjectOfType<Hotel>().AddRoom(this);
    } 

    private float GetWeight()
    {
        float xValue = transform.position.x;
        float yValue = (transform.position.y * yWeightMultiplier);
        float currentWeight = xValue * yValue;
        return currentWeight;
    }

    private void Update()
    {
        currentWeight = GetWeight();
        weightText.text = currentWeight.ToString();
    }
}
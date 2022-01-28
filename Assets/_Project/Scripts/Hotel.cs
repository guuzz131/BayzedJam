using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotel : MonoBehaviour
{
    public List<Block> blocks = new List<Block>();
    public List<Room> rooms = new List<Room>();

    [SerializeField] private float breakingPointAngle = 10f;

    private void Update()
    {
        Rotate(EvaluateAngle());
    }

    private float EvaluateAngle()
    {
        float angle = 0f;
        float currentWeight = 0f;
        
        foreach (var room in rooms)
        {
            currentWeight += room.GetWeight();
        }

        return angle;
    }

    private void Rotate(float angle)
    {
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        if (Mathf.Abs(angle) > breakingPointAngle) print("the hotel broke!");
    }
}

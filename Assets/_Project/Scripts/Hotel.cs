using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class Hotel : MonoBehaviour
{
    public static Hotel Instance;
    
    public List<Room> rooms = new List<Room>();

    [SerializeField] private float breakingPointAngle = 10f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Rotate(EvaluateAngle());
    }

    private float EvaluateAngle()
    {
        float currentWeight = 0f;
        
        foreach (var room in rooms)
        {
            currentWeight += room.currentWeight;
        }
        
        return currentWeight;
    }

    private void Rotate(float angle)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, -angle);
        if (Mathf.Abs(angle) > breakingPointAngle) print("the hotel broke!");
    }

    public void AddRoom(Room room)
    {
        rooms.Add(room);
    }
}
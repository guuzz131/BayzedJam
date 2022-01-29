using System.Collections.Generic;
using UnityEngine;

public class Hotel : MonoBehaviour
{
    public static Hotel Instance;
    
    public List<Room> rooms = new List<Room>();

    [SerializeField] private float breakingPointAngle = 10f;
    [SerializeField] private float rotationSpeed = .01f;
    [SerializeField] private float wankelSpeed;
    [SerializeField] private float wankelAmount;

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
            currentWeight -= room.currentWeight;
        }
        
        return currentWeight;
    }

    private void Rotate(float angle)
    {
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(0f, 0f, angle + Mathf.Sin(Time.time * wankelSpeed) * wankelAmount),
            rotationSpeed * Time.deltaTime);
        if (Mathf.Abs(transform.rotation.z) > breakingPointAngle) print("the hotel broke!");
    }

    public void AddRoom(Room room)
    {
        rooms.Add(room);
    }
}
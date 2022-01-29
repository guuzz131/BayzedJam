using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private float xWeightMultiplier;
    [SerializeField] private float yWeightMultiplier;
    [SerializeField] private TextMesh weightText;
    public float currentWeight;

    private void Awake()
    {
        FindObjectOfType<Hotel>().AddRoom(this);
        FindObjectOfType<PeopleController>().CheckRoomSide(gameObject);
    } 

    private float GetWeight()
    {
        float xValue = (transform.localPosition.x + transform.parent.localPosition.x) * xWeightMultiplier;
        float yValue = transform.localPosition.y * yWeightMultiplier;
        float currentWeight = xValue * yValue;
        return currentWeight;
    }

    private void Update()
    {
        currentWeight = GetWeight();
        weightText.text = currentWeight.ToString("0.00");
    }
}
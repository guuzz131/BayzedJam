using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject[] type;
    [SerializeField] private GameObject[] previewType;

    public void OnInteract(int number)
    {
        FindObjectOfType<PlaceRoom>().SelectType(type[number]);
        FindObjectOfType<PlaceRoom>().SelectPreviewType(previewType[number]);
    }
}

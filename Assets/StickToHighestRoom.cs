using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class StickToHighestRoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private float zoomSpeed;

    private void Update()
    {
        float highestRoom = 0f;
        foreach (var room in Hotel.Instance.rooms)
        {
            if (room.transform.position.y > highestRoom)
            {
                highestRoom = room.transform.position.y + 8f;
            }
        }

        
        if (cam.m_Lens.OrthographicSize < highestRoom) cam.m_Lens.OrthographicSize += Time.deltaTime * zoomSpeed;
    }
}

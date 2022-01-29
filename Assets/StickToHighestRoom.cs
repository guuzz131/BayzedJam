using System;
using System.Collections;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class StickToHighestRoom : MonoBehaviour
{
    [SerializeField] private Transform bounds;
    [SerializeField] private CinemachineVirtualCamera cam;

    float yScale;
    float oldHighRoom;
    private bool isMoving;
    private float camSize;

    private void Update()
    {
        float highestRoom = 0f;
        foreach (var room in Hotel.Instance.rooms)
        {
            if (room.transform.position.y > highestRoom)
            {
                highestRoom = room.transform.position.y + 5f;
            }
        }

        const float tolerance = .1f;
        if (Math.Abs(highestRoom - oldHighRoom) > tolerance)
        {
            if (!isMoving)
            {
                yScale = Map(highestRoom, .5f, 34, .5f, 1);
                isMoving = true;
                StartCoroutine(TimeToMove());
            }
        }

        oldHighRoom = highestRoom;
        cam.m_Lens.OrthographicSize = Map(bounds.localScale.y, .5f, 1, 9, 19.4f);
    }

    private float Map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    private IEnumerator TimeToMove()
    {
        bounds.DOScaleY(yScale, 1f);
        print(":D");
        yield return new WaitForSeconds(1f);
        isMoving = false;
    }
}

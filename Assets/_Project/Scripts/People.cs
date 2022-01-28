using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class People : MonoBehaviour
{
    [SerializeField] float speed;
    public GameObject currentRoom;

    public void MoveToNewPosition(Vector3 newRoomPos, GameObject room)
    {
        currentRoom = room;
        newRoomPos = new Vector3(newRoomPos.x, newRoomPos.y, transform.position.z);
        transform.DOMove(newRoomPos, speed);
    }
}

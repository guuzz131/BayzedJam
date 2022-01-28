using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRoom : MonoBehaviour
{
    [SerializeField] private GameObject room;
    [SerializeField] private GameObject roomPreview;

    private GameObject currentPreview;

    private void Update()
    {
        /*
        if (Input.GetMouseButton(0))
        {
            if (ValidPosition())
            {
                if (currentPreview == null) currentPreview = Instantiate(roomPreview, RoomPosition().point, Quaternion.identity);
                ShowPreview();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(currentPreview != null) PlaceNewRoom();
            Destroy(currentPreview);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Destroy(currentPreview);
        }
        */
    }

    private void ShowPreview()
    {
        currentPreview.transform.position = RoomPosition().point;
    }

    private void PlaceNewRoom()
    {
        Instantiate(room, RoomPosition().point, Quaternion.identity, transform);
    }

    private Vector2 GetMousePos()
    {
        return new Vector2(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    }

    private bool ValidPosition()
    {
        return RoomPosition().collider != null;
    }

    private RaycastHit2D RoomPosition()
    {
        Vector2 roomPos = new Vector2(GetMousePos().x, GetMousePos().y + 100f);
        return Physics2D.Raycast(roomPos, Vector2.down, Mathf.Infinity);
    }
}

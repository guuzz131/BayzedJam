using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRoom : MonoBehaviour
{
    [SerializeField] private GameObject roomPreview;

    private GameObject currentPreview;
    private GameObject selectedRoomType;

    private bool showPreview;
    private Vector2 lastValidPosition;

    private void Update()
    {
        if (showPreview)
        {
            ShowPreview();

            if (ValidPosition())
            {
                lastValidPosition = RoomPosition().point;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            showPreview = false;
            Destroy(currentPreview);
        }

        if (showPreview && Input.GetMouseButtonDown(0))
        {
            PlaceNewRoom();
        }
    }

    public void SelectType(GameObject newRoom)
    {
        selectedRoomType = newRoom;
        showPreview = true;
    }

    private void ShowPreview()
    {
        if (currentPreview == null) currentPreview = Instantiate(roomPreview, lastValidPosition, Quaternion.identity);
        if (ValidPosition()) currentPreview.transform.position = lastValidPosition;
    }

    private void PlaceNewRoom()
    {
        Instantiate(selectedRoomType, lastValidPosition, Quaternion.identity, transform);
        showPreview = false;
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

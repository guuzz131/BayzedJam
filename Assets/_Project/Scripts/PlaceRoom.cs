using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRoom : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private NewBlockButton blockButton;
    private GameObject currentBlock;
    private GameObject currentPreview;
    private GameObject selectedRoomType;

    private bool showPreview;
    private Vector2 lastValidPosition;

    private void Update()
    {
        if (showPreview)
        {
            ShowPreview();
        }

        if (showPreview && Input.GetMouseButtonDown(0) && HittingAnything())
        {
            PlaceNewRoom();
            Destroy(currentPreview);
        }
    }

    public void SelectType(GameObject newRoom)
    {
        selectedRoomType = newRoom;
        showPreview = true;
    }

    public void SelectPreviewType(GameObject newPreviewRoom)
    {
        currentBlock = newPreviewRoom;
    }

    private void ShowPreview()
    {
        if (currentPreview == null) currentPreview = Instantiate(currentBlock, RoomPosition(), Hotel.Instance.transform.rotation);
        if (HittingAnything()) { currentPreview.transform.position = RoomPosition(); }
        else currentPreview.transform.position = GetMousePos();

        currentPreview.transform.rotation = Hotel.Instance.transform.rotation;
    }

    private void PlaceNewRoom()
    {
        Instantiate(selectedRoomType, RoomPosition(), Hotel.Instance.transform.rotation, transform);
        showPreview = false;
        blockButton.ExtendMenu();
    }

    private Vector2 GetMousePos()
    {
        return new Vector2(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    }

    private bool HittingAnything()
    {
        if (currentPreview == null) return false;
        for (int i = 0; i < currentPreview.GetComponent<Block>().rooms.Count; i++)
        {
            Vector2 offset = (Vector2)currentPreview.GetComponent<Block>().rooms[i].transform.position - (Vector2)currentPreview.GetComponent<Block>().rooms[i].transform.parent.position;
            Vector2 pointInTheSky = new Vector2(GetMousePos().x, GetMousePos().y) + offset + (Vector2)Hotel.Instance.transform.up * 100f;

            foreach (var point in currentPreview.GetComponent<Block>().rooms[i].GetComponent<PreviewBlock>().sides)
            {
                Vector2 direction = ((Vector2)point.position - pointInTheSky).normalized;
                RaycastHit2D hit = Physics2D.Raycast(pointInTheSky, direction, Mathf.Infinity, mask);
                Debug.DrawRay(pointInTheSky, direction * 120f, Color.red);
                if (hit.collider != null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private Vector2 RoomPosition()
    {
        float shortestRay = Mathf.Infinity;
        Vector2 position = Vector2.zero;
        if (currentPreview != null)
        {
            for (int i = 0; i < currentPreview.GetComponent<Block>().rooms.Count; i++)
            {
                Vector2 offset = (Vector2)currentPreview.GetComponent<Block>().rooms[i].transform.position - (Vector2)currentPreview.GetComponent<Block>().rooms[i].transform.parent.position;
                Vector2 pointInTheSky = new Vector2(GetMousePos().x, GetMousePos().y) + offset + (Vector2)Hotel.Instance.transform.up * 100f;
                foreach (var point in currentPreview.GetComponent<Block>().rooms[i].GetComponent<PreviewBlock>().sides)
                {
                    Vector2 direction = ((Vector2)point.position - pointInTheSky).normalized;
                    RaycastHit2D hit = Physics2D.Raycast(pointInTheSky, direction, Mathf.Infinity, mask);
                    Debug.DrawRay(pointInTheSky, direction * 120f, Color.red);
                    if (hit.collider != null)
                    {
                        float rayToCheck = Vector2.Distance(pointInTheSky, hit.point);
                        if (rayToCheck < shortestRay)
                        {
                            shortestRay = rayToCheck;
                            position.y = hit.point.y - offset.y + .5f;
                        }
                    }
                }
            }
        }
        else
        {
            return GetMousePos();
        }

        return new Vector2(GetMousePos().x, position.y);
    }
}
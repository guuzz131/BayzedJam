using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpriteController : MonoBehaviour
{
    [SerializeField] GameObject[] heavenRooms;
    [SerializeField] GameObject[] hellRooms;
    [SerializeField] bool isMenu;
    [SerializeField] bool isAngel;

    private void OnEnable()
    {
        //spriteRenderer.sprite = neutralRoomSprite;
        if (isMenu) ChangeSide(isAngel);
    }

    public void ChangeSide(bool isAngel)
    {
        if (isAngel)
        {
            //Debug.Log("If you see this i dont know whats wrong");
            int randomRoom = Random.Range(0, heavenRooms.Length);
            GameObject newVisualRoom = Instantiate(heavenRooms[randomRoom], transform.position, transform.rotation, transform);
        }
        else
        {
            //Debug.Log("If you see this i dont know whats wrong 2");
            int randomRoom = Random.Range(0, hellRooms.Length);
            GameObject newVisualRoom = Instantiate(hellRooms[randomRoom], transform.position, transform.rotation, transform);
        }
    }
}

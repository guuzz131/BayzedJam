using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpriteController : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite neutralRoomSprite;
    [SerializeField] Sprite[] heavenRoomSprites;
    [SerializeField] Sprite[] hellRoomSprites;

    private void OnEnable()
    {
        spriteRenderer.sprite = neutralRoomSprite;
    }

    public void ChangeSide(bool isAngel)
    {
        if (isAngel)
        {
            int randomRoom = Random.Range(0, heavenRoomSprites.Length);
            spriteRenderer.sprite = heavenRoomSprites[randomRoom];
        }
        else
        {
            int randomRoom = Random.Range(0, hellRoomSprites.Length);
            spriteRenderer.sprite = hellRoomSprites[randomRoom];
        }
    }
}

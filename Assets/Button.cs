using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject[] type;
    [SerializeField] private GameObject[] previewType;
    [SerializeField] private NewBlockButton newBlockButton;

    bool canPay;

    public void OnInteract(int number)
    {
        if (canPay)
        {
            FindObjectOfType<PlaceRoom>().SelectType(type[number]);
            FindObjectOfType<PlaceRoom>().SelectPreviewType(previewType[number]);
            Sound.Instance.Play(0);
        }
        
    }

    public void Price(int cost)
    {
        canPay = false;
        if(MoneyHandler.Instance.money >= cost)
        {
            MoneyHandler.Instance.money -= cost;
            canPay = true;
        }
    }

    public void NewBlockButtonLeft(GameObject button)
    {
        if (!canPay) return;
        newBlockButton.OnLeftInteract(button);
    }

    public void NewBlockButtonRight(GameObject button)
    {
        if (!canPay) return;
        newBlockButton.OnRightInteract(button);
    }
}

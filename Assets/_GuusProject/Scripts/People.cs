using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class People : MonoBehaviour
{
    [SerializeField] float speed;
    public GameObject currentRoom;
    public bool IsAngel;
    public bool firstFlight;
    bool isInRoom;
    GameObject pivot;

    private void Awake()
    {
        StartCoroutine(FirstFlightWait());
        pivot = GameObject.Find("HotelPivot");
    }

    private void Update()
    {
        if (isInRoom && currentRoom != null && transform.parent == pivot.transform)
        {
            transform.position = currentRoom.transform.position;
        }
    }
    public void MoveToNewPosition(Vector3 newRoomPos, GameObject room)
    {
        isInRoom = false;
        currentRoom = room;
        //newRoomPos = new Vector3(room.transform.position.x, room.transform.position.y, transform.position.z);
        if (room == null)
        {
            transform.DOMove(newRoomPos, speed);
        }
        else
        {
            transform.DOMove(room.transform.position, speed);
        }
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(Wait());

        if (transform.parent != null && !FindObjectOfType<PeopleController>().peopleInHouse.Contains(this))
        {
            FindObjectOfType<PeopleController>().peopleInHouse.Add(this);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(speed);
        transform.GetChild(0).gameObject.SetActive(false);
        isInRoom = true;
        if(gameObject.layer != 10) MoneyHandler.Instance.money += Random.Range(9, 17);
        gameObject.layer = 10;
    }

    private IEnumerator FirstFlightWait()
    {
        yield return new WaitForSeconds(speed + .7f);
        firstFlight = true;
    }

    public float GetCurrentWeight()
    {
        if (!firstFlight) return 0f;
        float xValue = transform.localPosition.x;
        float yValue = transform.localPosition.y;
        float currentWeight = xValue * yValue;
        return currentWeight;
    }
}

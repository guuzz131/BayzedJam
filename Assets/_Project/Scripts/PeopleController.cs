using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleController : MonoBehaviour
{
    [SerializeField] List<People> people;

    [SerializeField] List<People> selectedPeople;

    [SerializeField] List<GameObject> rooms;
    List<GameObject> availibleRooms;

    [SerializeField] Transform mouseSelectionField;

    private Vector2 moveToPos;
    private Vector2 newRoomPos;
    private GameObject newRoom;


    private void Start()
    {
        availibleRooms = rooms;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseSelectionField.gameObject.SetActive(true);
            mouseSelectionField.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            mouseSelectionField.position = new Vector3(mouseSelectionField.position.x, mouseSelectionField.position.y, -10);
            mouseSelectionField.localScale = new Vector3(
                                                    Camera.main.ScreenToWorldPoint(Input.mousePosition).x - mouseSelectionField.position.x,
                                                    (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - mouseSelectionField.position.y) * -1, 1);

        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseSelectionField.gameObject.SetActive(false);
            selectedPeople = mouseSelectionField.GetChild(0).GetComponent<SelectPeople>().selectedPeople;
        }

        if (Input.GetMouseButtonDown(0) && selectedPeople.Count > 0)
        {
            moveToPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            foreach (var person in selectedPeople)
            {
                if (person.currentRoom != null)
                {
                    availibleRooms.Add(person.currentRoom);
                }
                GetClosestAvailableRoom(moveToPos);
                person.MoveToNewPosition(newRoomPos, newRoom);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            foreach(var people in selectedPeople)
            {
                people.GetComponent<SpriteRenderer>().color = Color.white;
            }
            selectedPeople.Clear();
        }
    }

    private void GetClosestAvailableRoom(Vector2 closeTo)
    {
        float distance = Mathf.Infinity;
        int closestIndex = 0;
        for (int i = 0; i < availibleRooms.Count; i++)
        {
            float newDistance = Vector2.Distance(closeTo, availibleRooms[i].transform.position);
            if (newDistance < distance)
            {
                distance = newDistance;
                closestIndex = i;
            }
        }

        newRoomPos = availibleRooms[closestIndex].transform.position;
        newRoom = availibleRooms[closestIndex];
        availibleRooms.RemoveAt(closestIndex);
        
    }
}

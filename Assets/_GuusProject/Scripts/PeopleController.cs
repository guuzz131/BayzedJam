using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleController : MonoBehaviour
{
    [SerializeField] GameObject[] angelAndDevil;
    

    [SerializeField] List<People> people = new List<People>();

    [SerializeField] List<People> selectedPeople = new List<People>();
    
    [Header("Devils")]
    [SerializeField] List<GameObject> hellRooms = new List<GameObject>();
    List<GameObject> hellAvailibleRooms = new List<GameObject>();
    [SerializeField] Transform[] hellQueuePos;
    [SerializeField] List<People> devils = new List<People>();
    [SerializeField] List<People> waitingDevils = new List<People>();
    [SerializeField] Transform devilsSpawnPos;

    [Header("Angels")]
    [SerializeField] List<GameObject> heavenRooms = new List<GameObject>();
    List<GameObject> heavenAvailibleRooms = new List<GameObject>();
    [SerializeField] Transform[] heavenQueuePos;
    [SerializeField] List<People> angels = new List<People>();
    [SerializeField] List<People> waitingAngels = new List<People>();
    [SerializeField] Transform angelSpawnPos;

    [Header("Other Stuff")]
    [SerializeField] Transform mouseSelectionField;
    
    [SerializeField] Transform hotelPivot;

    //[SerializeField] AnimationCurve angelSpawnCurve;
    [SerializeField] float spawnSpeed;
    [SerializeField] float difficultyIncrease;
    [SerializeField] float spawnSpeedMax;

    private Vector2 moveToPos;
    private Vector2 newRoomPos;
    private GameObject newRoom;


    private void Start()
    {
        StartCoroutine(SpawnPeople());
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
                MovePersonToNewRoom(person);
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

    IEnumerator SpawnPeople()
    {
        yield return new WaitForSeconds(spawnSpeed);

        int randomSelect = Random.Range(0, 2);
        GameObject newPerson = angelAndDevil[randomSelect]; //Random Angel or Devil

        spawnSpeed = spawnSpeed * difficultyIncrease;
        if (spawnSpeed < spawnSpeedMax) spawnSpeed = spawnSpeedMax; //SpawnSpeed adjustment

        GameObject SpawnedPerson = Instantiate(newPerson, angelSpawnPos.position, Quaternion.identity); //Spawn new person
        People person = SpawnedPerson.GetComponent<People>();
        if(randomSelect == 0)
        {
            angels.Add(person);
            if (heavenAvailibleRooms.Count > 0)
            {
                SpawnedPerson.transform.SetParent(hotelPivot, true);
                MovePersonToNewRoom(person);
            }
            else
            {

                if (heavenQueuePos.Length > waitingAngels.Count)
                {
                    person.MoveToNewPosition(heavenQueuePos[waitingAngels.Count].position, null);
                }
                waitingAngels.Add(person);
            }
        }
        else
        {
            angels.Add(person);
            SpawnedPerson.transform.position = devilsSpawnPos.position;
            if (hellAvailibleRooms.Count > 0)
            {
                SpawnedPerson.transform.SetParent(hotelPivot, true);
                MovePersonToNewRoom(person);
            }
            else
            {
                if (hellQueuePos.Length > waitingDevils.Count)
                {
                    person.MoveToNewPosition(hellQueuePos[waitingDevils.Count].position, null);
                }
                waitingDevils.Add(person);
            }
        }
        
        people.Add(person);
        StartCoroutine(SpawnPeople());

    }


    private void MovePersonToNewRoom(People person)
    {
        if (person.currentRoom != null && person.IsAngel)
        {
            heavenAvailibleRooms.Add(person.currentRoom);
        } else if (person.currentRoom != null)
        {
            hellAvailibleRooms.Add(person.currentRoom);
        }
        GetClosestAvailableRoom(moveToPos, person.IsAngel);
        person.MoveToNewPosition(newRoomPos, newRoom);
    }


    private void GetClosestAvailableRoom(Vector2 closeTo, bool isAngel)
    {
        float distance = Mathf.Infinity;
        int closestIndex = 0;

        if (isAngel)
        {
            for (int i = 0; i < heavenAvailibleRooms.Count; i++)
            {
                float newDistance = Vector2.Distance(closeTo, heavenAvailibleRooms[i].transform.position);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    closestIndex = i;
                }
            }

            newRoomPos = heavenAvailibleRooms[closestIndex].transform.position;
            newRoom = heavenAvailibleRooms[closestIndex];
            heavenAvailibleRooms.RemoveAt(closestIndex);
        }
        else
        {
            for (int i = 0; i < hellAvailibleRooms.Count; i++)
            {
                float newDistance = Vector2.Distance(closeTo, hellAvailibleRooms[i].transform.position);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    closestIndex = i;
                }
            }

            newRoomPos = hellAvailibleRooms[closestIndex].transform.position;
            newRoom = hellAvailibleRooms[closestIndex];
            hellAvailibleRooms.RemoveAt(closestIndex);
        }
    }


    public void CheckRoomSide(GameObject room)
    {
        
        Vector2 relativeRoomPos = Vector2.zero;
        if (room.transform.parent != null)
        {
            relativeRoomPos = room.transform.localPosition + room.transform.parent.localPosition;
        }

        if(relativeRoomPos.x < 0)
        {
            heavenRooms.Add(room);
            heavenAvailibleRooms.Add(room);
            room.GetComponent<RoomSpriteController>().ChangeSide(true);
            Debug.Log("Angel: " + relativeRoomPos.x);
        }
        else
        {
            hellRooms.Add(room);
            hellAvailibleRooms.Add(room);
            room.GetComponent<RoomSpriteController>().ChangeSide(false);
            Debug.Log("Devil: " + relativeRoomPos.x);
        }
        
        //heavenAvailibleRooms = heavenRooms;
        //hellAvailibleRooms = hellRooms;
    }
}

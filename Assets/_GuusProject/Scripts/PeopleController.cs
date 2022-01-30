using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleController : MonoBehaviour
{
    [SerializeField] GameObject[] angelAndDevil;
    

    public List<People> people = new List<People>();
    public List<People> peopleInHouse = new List<People>();

    [SerializeField] List<People> selectedPeople = new List<People>();
    
    [Header("Devils")]
    [SerializeField] List<GameObject> hellRooms = new List<GameObject>();
    List<GameObject> hellAvailibleRooms = new List<GameObject>();
    [SerializeField] Transform[] hellQueuePos;
    public List<People> devils = new List<People>();
    [SerializeField] List<People> waitingDevils = new List<People>();
    [SerializeField] Transform devilsSpawnPos;

    [Header("Angels")]
    [SerializeField] List<GameObject> heavenRooms = new List<GameObject>();
    List<GameObject> heavenAvailibleRooms = new List<GameObject>();
    [SerializeField] Transform[] heavenQueuePos;
    public List<People> angels = new List<People>();
    [SerializeField] List<People> waitingAngels = new List<People>();
    [SerializeField] Transform angelSpawnPos;

    [Header("Other Stuff")]
    [SerializeField] Transform mouseSelectionField;
    
    [SerializeField] Transform hotelPivot;

    //[SerializeField] AnimationCurve angelSpawnCurve;
    [SerializeField] float spawnSpeed;
    [SerializeField] float difficultyIncrease;
    [SerializeField] float spawnSpeedMax;


    [SerializeField] GameObject AlertDevils;
    [SerializeField] GameObject AlertAngels;


    private Vector2 moveToPos;
    private Vector2 newRoomPos;
    private GameObject newRoom;

    int totalRoomCountAngel;
    int totalRoomCountDevil;

    private void Start()
    {
        StartCoroutine(SpawnPeople());
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && selectedPeople.Count > 0)
        {
            moveToPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            foreach (var person in selectedPeople)
            {
                MovePersonToNewRoom(person);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            foreach (var people in selectedPeople)
            {
                people.GetComponent<SpriteRenderer>().color = Color.white;
                people.transform.GetChild(1).gameObject.SetActive(false);
            }
            selectedPeople.Clear();
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

        if (Input.GetMouseButtonDown(1))
        {
            foreach(var people in selectedPeople)
            {
                people.GetComponent<SpriteRenderer>().color = Color.white;
                people.transform.GetChild(1).gameObject.SetActive(false);
            }
            selectedPeople.Clear();
        }


        if(totalRoomCountAngel != heavenRooms.Count)
        {
            for(int i = 0; i < heavenRooms.Count - totalRoomCountAngel; i++)
            {
                if(waitingAngels.Count > 0)
                {
                    waitingAngels[0].transform.SetParent(hotelPivot, true);
                    MovePersonToNewRoom(waitingAngels[0]);
                    waitingAngels.RemoveAt(0);
                }
            }
            totalRoomCountAngel = heavenRooms.Count;
        }
        if(totalRoomCountDevil != hellRooms.Count)
        {
            for (int i = 0; i < hellRooms.Count - totalRoomCountDevil; i++)
            {
                if (waitingDevils.Count > 0)
                {
                    waitingDevils[0].transform.SetParent(hotelPivot, true);
                    MovePersonToNewRoom(waitingDevils[0]);
                    waitingDevils.RemoveAt(0);
                }
            }
            totalRoomCountDevil = hellRooms.Count;
        }
    }

    IEnumerator SpawnPeople()
    {
        yield return new WaitForSeconds(spawnSpeed);
        if (!Hotel.Instance.dead)
        {
            int randomSelect = Random.Range(0, 2);
            GameObject newPerson = angelAndDevil[randomSelect]; //Random Angel or Devil

            spawnSpeed = spawnSpeed * difficultyIncrease;
            if (spawnSpeed < spawnSpeedMax) spawnSpeed = spawnSpeedMax; //SpawnSpeed adjustment

            GameObject SpawnedPerson = Instantiate(newPerson, angelSpawnPos.position, Quaternion.identity); //Spawn new person
            People person = SpawnedPerson.GetComponent<People>();
            if (randomSelect == 0)
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

                        if (heavenQueuePos.Length - 1 == waitingAngels.Count)
                        {
                            AlertAngels.SetActive(true);
                            AlertAngels.transform.localScale = new Vector3(1.2f, 1.2f, 1);
                        }
                        if (heavenQueuePos.Length - 2 == waitingAngels.Count)
                        {
                            AlertAngels.SetActive(true);
                            AlertAngels.transform.localScale = new Vector3(.7f, .7f, 1);
                        }
                        person.MoveToNewPosition(heavenQueuePos[waitingAngels.Count].position, null);
                    }
                    if (heavenQueuePos.Length == waitingAngels.Count)
                    {
                        Hotel.Instance.BreakHotel();
                    }
                    waitingAngels.Add(person);
                }
            }
            else
            {
                devils.Add(person);
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
                        
                        if (hellQueuePos.Length - 1 == waitingDevils.Count)
                        {
                            AlertDevils.SetActive(true);
                            AlertDevils.transform.localScale = new Vector3(1.2f, 1.2f, 1);
                        }
                        if (hellQueuePos.Length - 2 == waitingDevils.Count)
                        {
                            AlertDevils.SetActive(true);
                            AlertDevils.transform.localScale = new Vector3(.7f, .7f, 1);
                        }
                        person.MoveToNewPosition(hellQueuePos[waitingDevils.Count].position, null);
                    }
                    if (hellQueuePos.Length == waitingDevils.Count)
                    {
                        Hotel.Instance.BreakHotel();
                    }
                    waitingDevils.Add(person);
                }
            }

            people.Add(person);
            StartCoroutine(SpawnPeople());
        }
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
            //Debug.Log("Angel: " + relativeRoomPos.x);
        }
        else
        {
            hellRooms.Add(room);
            hellAvailibleRooms.Add(room);
            room.GetComponent<RoomSpriteController>().ChangeSide(false);
            //Debug.Log("Devil: " + relativeRoomPos.x);
        }
        
        //heavenAvailibleRooms = heavenRooms;
        //hellAvailibleRooms = hellRooms;
    }
}

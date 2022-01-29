using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotel : MonoBehaviour
{
    public static Hotel Instance;
    
    public List<Room> rooms = new List<Room>();

    [SerializeField] private float breakingPointAngle = 10f;
    [SerializeField] private float rotationSpeed = .01f;
    [SerializeField] private float wankelSpeed;
    [SerializeField] private float wankelAmount;
    [SerializeField] private Transform brokenHotelParent;

    [SerializeField] private AnimationClip crumbleRightAni, crumbleLeftAni;

    [SerializeField] private PeopleController peopleController;

    public bool dead;

    private void Awake()
    {
        Instance = this;
        bool once = true;
    }

    private void Update()
    {
        Rotate(EvaluateAngle());
    }

    private float EvaluateAngle()
    {
        float currentWeight = 0f;
        
        foreach (var room in rooms)
        {
            currentWeight -= room.currentWeight;
        }
        
        return currentWeight;
    }

    private void Rotate(float angle)
    {
        //Debug.Log("Abs Euler Transform " + (Mathf.Abs(transform.rotation.eulerAngles.z) + 90 + 360) % 360 + " angle: " + angle);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(0f, 0f, angle + Mathf.Sin(Time.time * wankelSpeed) * wankelAmount),
            rotationSpeed * Mathf.Abs(angle) * Time.deltaTime);
        if (((Mathf.Abs(transform.rotation.eulerAngles.z) + 90 + 360) % 360 > breakingPointAngle + 90 || (Mathf.Abs(transform.rotation.eulerAngles.z) + 90 + 360) % 360 < 90 - breakingPointAngle) && !dead)
        {
            BreakHotel();
            print("the hotel broke!");
            dead = true;
        }
    }

    public void AddRoom(Room room)
    {
        rooms.Add(room);
    }

    void BreakHotel()
    {
        if(transform.rotation.z < 0)
        {
            GetComponent<Animation>().clip = crumbleRightAni;
        }
        else
        {
            GetComponent<Animation>().clip = crumbleLeftAni;
        }
        GetComponent<Animation>().Play();
        foreach (var room in rooms)
        {
            room.transform.SetParent(brokenHotelParent, true);
            room.gameObject.AddComponent<Rigidbody2D>();
            Vector2 randomDir = new Vector2(Random.Range(-1f,1f), Random.Range(-1f, 1f));
            float force = Random.Range(0, 1f);
            room.gameObject.GetComponent<Rigidbody2D>().AddForce(randomDir * force, ForceMode2D.Impulse);
        }

        foreach (var person in peopleController.people)
        {
            person.transform.SetParent(brokenHotelParent, true);
        }
        rooms.Clear();
        StartCoroutine(WaitForKill());
    }

    IEnumerator WaitForKill()
    {
        yield return new WaitForSeconds(1f);
        if(transform.rotation.y < 0)
        {
            foreach(var angel in peopleController.angels)
            {
                angel.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (var devil in peopleController.devils)
            {
                devil.gameObject.SetActive(false);
            }
        }
    }
}
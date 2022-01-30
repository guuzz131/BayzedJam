using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeopleScore : MonoBehaviour
{
    [SerializeField] PeopleController peopleController;

    public int totalPeopleInHouse;

    void Update()
    {
        totalPeopleInHouse = peopleController.peopleInHouse.Count;
        GetComponent<Text>().text = "" + totalPeopleInHouse;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeopleScore : MonoBehaviour
{
    [SerializeField] PeopleController peopleController;

    public int totalPeopleInHouse;


    static int Highscore;

    private void Start()
    {
        PlayerPrefs.SetInt("Highscore", 0);
        Highscore = PlayerPrefs.GetInt("Highscore");

    }

    void Update()
    {
        totalPeopleInHouse = peopleController.peopleInHouse.Count;
        GetComponent<Text>().text = "" + totalPeopleInHouse;
    }

    public void SaveHighscore()
    {
        if(totalPeopleInHouse > Highscore)
        {
            Highscore = totalPeopleInHouse;
            PlayerPrefs.SetInt("Highscore", Highscore);
            Debug.Log("Set Highscore: " + PlayerPrefs.GetInt("Highscore"));
        }
    }

}

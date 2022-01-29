using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyHandler : MonoBehaviour
{
    public static MoneyHandler Instance;

    public int money;

    public Text moneyText;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        moneyText.text = "" + money;
    }
}

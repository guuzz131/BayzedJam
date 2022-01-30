using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPeople : MonoBehaviour
{
    public List<People> selectedPeople;
    [SerializeField] LayerMask peopleLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Added " + collision);
        if(collision.gameObject.layer == 10)
        {
            selectedPeople.Add(collision.gameObject.GetComponent<People>());
            collision.GetComponent<SpriteRenderer>().color = Color.yellow;
            collision.transform.GetChild(1).gameObject.SetActive(true);
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Removed " + collision);
            if (collision.gameObject.layer == 10)
            {
                selectedPeople.Remove(collision.gameObject.GetComponent<People>());
                collision.GetComponent<SpriteRenderer>().color = Color.white;
                collision.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        
    }
}

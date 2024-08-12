using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //If the player is nearby the phone and can pick it up,
    //Allow them to pick it up
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CutsceneMovement>().canPickup = true;
            collision.gameObject.GetComponent<CutsceneMovement>().curPickup = this.gameObject;
        }
    }

    //If the player moves past the phone, they can't pick it up.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CutsceneMovement>().canPickup = false;
            collision.gameObject.GetComponent<CutsceneMovement>().curPickup = null;
        }
    }
}

// This script will be to switch from the train station scene to the cutscenes at the start of the game
// I'll add the animations of the phone buzzing n stuff later

// How this scene is supposed to go:
// Animation: Phone rings and user picks up.
// Switches to CS_1 aka the first cutscene 

// How it'll go here because I cba to animate:
// User gets 3-5 seconds to move around, text pops up saying phone is ringing and then switch to cutscenes

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Transactions; // For IEnumerator and coroutines

public class SwitchToGame : MonoBehaviour
{

    private bool starter = false; // is set to true after the 3 seconds pass
    public GameObject canvasControl;
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        starter = false;
        
        // Hide the Canvas initially 
        canvasControl.SetActive(false);
        
        // wait and then show
        if (starter == false)
        {
            StopAllCoroutines();
            StartCoroutine(WaitThreeSeconds());
        }
    }

    void Update()
    {
        //If the player presses E while the Objective UI is active (visible)
        //Load the first cutscene
        if (Input.GetKeyDown(KeyCode.E) && (canvasControl.activeSelf == true) && player.GetComponent<CutsceneMovement>().pickedUp == true)
        {
            StopAllCoroutines();
            StartCoroutine(WaitOneSecond());
            SceneManager.LoadScene("CS_1");
        }
    }
    
    protected IEnumerator WaitThreeSeconds()
    {
        yield return new WaitForSeconds(3);
        canvasControl.SetActive(true);
        starter = true;
    }
    
    protected IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1);
    }

}

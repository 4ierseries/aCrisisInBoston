// This script will be to switch from the train station scene to the cutscenes at the start of the game
// I'll add the animations of the phone buzzing n stuff later

// How this scene is supposed to go:
// Animation: Phone rings and user picks up.
// Switches to CS_1 aka the first cutscene 

// How it'll go here because I cba to animate:
// User gets 3-5 seconds to move around, text pops up saying phone is ringing and then switch to cutscenes

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For UI elements
using System.Collections; // For IEnumerator and coroutines

namespace Switch
{
public class SwitchToGame : MonoBehaviour
{
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "TRAIN_STATION")
        {
            StartCoroutine(WaitToSwitch());
            gameObject.SetActive(false);
        }
    }

    private IEnumerator WaitToSwitch()
    {
        // Wait for a few seconds
        yield return new WaitForSeconds(1);
    }
}
}
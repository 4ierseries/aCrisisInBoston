// this script is for switching to the game from the title screen
// add a fully responsive menu soon
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private string _SceneName = "TRAIN_STATION"; // Insert Name of Scene to load
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //This method gets called on the PlayButton when the player clicks on it.
    public void StartGame()
    {
        LoadScene(_SceneName);
    }


    public void Quit()
    {
        //If you're in the Unity Editor and click on Quit,
        //Exit out of Play Mode.
        #if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

        //If you're in a Build and click on Quit,
        //Exit out of the Game (Program).
        #endif
                Application.Quit();
    }
}

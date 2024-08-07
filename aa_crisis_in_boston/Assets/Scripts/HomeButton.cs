using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // For IEnumerator and coroutines

public class HomeButton : MonoBehaviour
{

    public GameObject canvas;
    
    public void GoHome()
    {
        canvas.SetActive(false);
        Debug.Log("Send to home");
    }
}

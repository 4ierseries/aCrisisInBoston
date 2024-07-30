using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewDialogue : MonoBehaviour
{
    private TMP_Text textHolder;
    [SerializeField] private string input;

    private bool dialogueStarted = false;
    private bool dialogueCompleted = false;

    private void Start()
    {
        textHolder = GetComponent<TMP_Text>();
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Return)
        {
            HandleReturnKeyPress();
        }
    }

    private void HandleReturnKeyPress()
    {
        if (!dialogueStarted)
        {
            dialogueStarted = true;
            StartCoroutine(WriteText(input, textHolder, OnDialogueComplete));
        }
        else if (dialogueCompleted)
        {
            Debug.Log("Switching to the next cutscene.");
            SceneManager.LoadScene("CS_2");
        }
    }

    private IEnumerator WriteText(string input, TMP_Text textHolder, System.Action onComplete)
    {
        textHolder.text = ""; // Clear the text before starting
        for (int i = 0; i < input.Length; i++)
        {
            textHolder.text += input[i];
            yield return new WaitForSeconds(0.1f);
        }
        onComplete?.Invoke();
    }

    private void OnDialogueComplete()
    {
        Debug.Log("Dialogue has ended.");
        dialogueCompleted = true;
    }
}

    /*
    void Update()
    {
        bool start = true;
        if (Input.GetKeyDown(KeyCode.Return) && start == true)
        {
          StartCoroutine(WriteText(input, textHolder));
          start = false;
        }

        if (start == false)
        {
            Debug.Log("does this work");
        }
    }
    */

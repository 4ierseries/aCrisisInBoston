// this script handles all the dialogue scenes
// because of how lines 39-53 work this needs to be manually updated each time a new cutscene is added
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private TMP_Text textHolder;
    [SerializeField] private string input;

    private bool dialogueStarted = false;
    private bool dialogueCompleted = false;

    private void Start()
    {
        textHolder = GetComponent<TMP_Text>();

        if (SceneManager.GetActiveScene().name == "GameCompleteScene")
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        if (!dialogueStarted)
        {
            dialogueStarted = true;
            StartCoroutine(WriteText(input, textHolder, OnDialogueComplete));
        }
    }

    private IEnumerator WriteText(string input, TMP_Text textHolder, System.Action onComplete)
    {
        textHolder.text = ""; // Clear the text before starting
        for (int i = 0; i < input.Length; i++)
        {
            textHolder.text += input[i];
            yield return new WaitForSeconds(0.1f); // Adjust the speed of the text
        }
        onComplete?.Invoke();
    }

    private void OnDialogueComplete()
    {
        dialogueCompleted = true;
        // Optionally, you could trigger something else after the dialogue completes.
    }
}

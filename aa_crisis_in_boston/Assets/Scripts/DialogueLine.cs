using System.Collections;
using TMPro;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private TMP_Text textHolder;
        [SerializeField] private string input;

        private void Update()
        {
            textHolder = GetComponent<TMP_Text>();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(WriteText(input, textHolder));
            }
        }

    }

}
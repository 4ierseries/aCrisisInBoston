using System.Collections;
using TMPro;
using UnityEngine;


namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private TMP_Text textHolder;
        [SerializeField] private string input;

        private void Awake()
        {
            textHolder = GetComponent<TMP_Text>();

            StartCoroutine(WriteText(input, textHolder));
        }

    }

}

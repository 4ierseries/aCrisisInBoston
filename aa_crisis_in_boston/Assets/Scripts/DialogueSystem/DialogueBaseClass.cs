using System.Collections;
using TMPro;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        protected IEnumerator WriteText(string input, TMP_Text textHolder)
        {

            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return new WaitForSeconds(0.1f);
            }
            
        }

    }
    
}

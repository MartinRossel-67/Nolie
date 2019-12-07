using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    [SerializeField] private Text speakerField;
    private string curentSpeaker;
    [SerializeField] private Text textField;

    [SerializeField] private float typeSpeed;


    public void UpdateTextField(string _speaker, string _sentence)
    {
        if (_speaker != curentSpeaker)
        {
            speakerField.text = _speaker;
            curentSpeaker = _speaker;
        }
        StartCoroutine(TypeSentence(_sentence));
    }


    IEnumerator TypeSentence (string sentence)
    {
        textField.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            yield return new WaitForSeconds(1 / typeSpeed);
            textField.text += letter;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    [SerializeField] private GameObject curentSpeaker;
    [SerializeField] private Text speakerField;
    [SerializeField] private Text textField;

    [SerializeField] private float typeSpeed;
    public bool isTyping;

    [SerializeField] private Color notSpeakingColor;


    #region Text Manager
    public void UpdateTextField(string _speaker, string _sentence)
    {
        if (_speaker != curentSpeaker.name)
        {
            curentSpeaker.GetComponentInParent<Image>().color = notSpeakingColor;

            foreach (GameObject character in characters)
            {
                if (character.name == _speaker)
                {
                    character.GetComponentInParent<Image>().color = new Color(1,1,1);
                    curentSpeaker = character;
                    break;
                }
            }
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(_sentence));
    }


    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        textField.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            yield return new WaitForSeconds(1 / typeSpeed);
            textField.text += letter;
        }
        isTyping = false;
    }


    public void DisplaySentence(string _sentence)
    {
        StopAllCoroutines();
        textField.text = _sentence;
        isTyping = false;
    }
    #endregion
}

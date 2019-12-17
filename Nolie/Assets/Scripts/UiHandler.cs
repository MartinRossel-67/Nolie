using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    [SerializeField] private GameObject curentSpeaker;

    [Space(10)]
    [SerializeField] private Text dialogueTextField;
    private Canvas dialogueCanvas;
    [SerializeField] private Text option1TextField, option2TextField;
    private Canvas optionsCanvas;

    [SerializeField][Space(10)] private float typeSpeed;
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
        StartCoroutine(TypeSentence(_sentence, dialogueTextField));
    }


    IEnumerator TypeSentence(string sentence, Text _textField)
    {
        isTyping = true;
        _textField.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            yield return new WaitForSeconds(1 / typeSpeed);
            _textField.text += letter;
        }
        isTyping = false;
    }


    public void DisplaySentence(string _sentence)
    {
        StopAllCoroutines();
        dialogueTextField.text = _sentence;
        isTyping = false;
    }
    #endregion


    public void ActiveOptions(string _sentence1, string _sentence2)
    {
        if (dialogueCanvas == null)
        {
            dialogueCanvas = dialogueTextField.GetComponentInParent<Canvas>();
            optionsCanvas = option1TextField.GetComponentInParent<Canvas>();
        }
        
        dialogueCanvas.enabled = false;        
        optionsCanvas.enabled = true;

        StartCoroutine(TypeSentence(_sentence1, option1TextField));  
        StartCoroutine(TypeSentence(_sentence2, option2TextField));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    [Header("Dialogues")]
    [SerializeField] private GameObject[] characters;
    [SerializeField] private GameObject curentSpeaker;

    [Space(10)]
    [SerializeField] private Text dialogueTextField;
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private Text option1TextField, option2TextField;
    [SerializeField] private Canvas optionsCanvas;

    [SerializeField][Space(10)] private float typeSpeed;
    [SerializeField] public bool isTyping;

    [SerializeField] private Color notSpeakingColor;


    [Header("Help")]
    [SerializeField] private GameObject swipHelper;
    [SerializeField] private float swipHelperDuration = 1;
    [SerializeField] private GameObject clicHelper;
    [SerializeField] private float clicHelperDuration = 1;



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


    public void EnableOptions(string _sentence1, string _sentence2)
    {        
        dialogueCanvas.enabled = false;        
        optionsCanvas.enabled = true;

        StartCoroutine(TypeSentence(_sentence1, option1TextField));  
        StartCoroutine(TypeSentence(_sentence2, option2TextField));
    }

        public void DisableOptions()
    {        
        dialogueCanvas.enabled = true;        
        optionsCanvas.enabled = false;
    }


    
    public IEnumerator SwipHelp()
    {        
        swipHelper.SetActive(true);
        yield return new WaitForSeconds(swipHelperDuration - 0.01f);
        swipHelper.SetActive(false);

        gameController.EndHelp();
    }


    public IEnumerator ClicHelp()
    {        
        clicHelper.SetActive(true);
        yield return new WaitForSeconds(clicHelperDuration - 0.01f);
        clicHelper.SetActive(false);

        gameController.EndHelp();
    }
}

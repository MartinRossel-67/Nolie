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
    [SerializeField] public GameObject narrativeCanvas;

    [SerializeField] private Text speakerTextField;
    [SerializeField] private Text dialogueTextField;
    [SerializeField] private Canvas dialogueCanvas;

    [SerializeField] private Text option1TextField, option2TextField;
    [SerializeField] private Canvas optionsCanvas;

    [SerializeField] [Space(10)] private float typeSpeed;
    [SerializeField] public bool isTyping;

    [SerializeField] private Color notSpeakingColor;


    [Header("Help")]
    [SerializeField] private GameObject swipHelper;
    [SerializeField] private float swipHelperDuration = 1;
    [SerializeField] private GameObject clicHelper;
    [SerializeField] private float clicHelperDuration = 1;


    private float waitingTime;
    [HideInInspector] public bool isWaiting;



    #region Text Manager
    public void UpdateTextField(string _speaker, string _sentence, float waitingTime)
    {
        if (_speaker != curentSpeaker.name)
        {
            speakerTextField.text = _speaker;
            curentSpeaker.GetComponentInParent<Image>().color = notSpeakingColor;

            foreach (GameObject character in characters)
            {
                if (character.name == _speaker)
                {
                    curentSpeaker = character;
                    character.GetComponentInParent<Image>().color = new Color(1, 1, 1);
                    break;
                }
            }
        }

        StopAllCoroutines();
        StopHelp();
        StartCoroutine(TypeSentence(_sentence, dialogueTextField, waitingTime));
    }


    IEnumerator TypeSentence(string sentence, Text _textField, float waitingTime)
    {
        if (waitingTime > 0)
        {
            isWaiting = true;
            narrativeCanvas.SetActive(false);
            yield return new WaitForSeconds(waitingTime);
            narrativeCanvas.SetActive(true);
            isWaiting = false;
        }


        if (sentence.ToCharArray().Length != 0)
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
        else
        {
            gameController.NextSentence();
        }
    }


    public void DisplaySentence(string _sentence)
    {
        StopAllCoroutines();
        StopHelp();

        dialogueTextField.text = _sentence;
        isTyping = false;

        gameController.EndHelp();
    }
    #endregion


    public void EnableOptions(string _sentence1, string _sentence2)
    {
        dialogueCanvas.enabled = false;
        optionsCanvas.enabled = true;

        StartCoroutine(TypeSentence(_sentence1, option1TextField, 0));
        StartCoroutine(TypeSentence(_sentence2, option2TextField, 0));
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


    public void StopHelp()
    {
        swipHelper.SetActive(false);
        clicHelper.SetActive(false);

        gameController.EndHelp();
    }
}

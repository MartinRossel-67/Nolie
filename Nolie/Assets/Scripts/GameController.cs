using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private UiHandler uiHandler;
    private SceneHandler sceneHandler;
    private Animator animHandler;
    private AudioSource audioSource;


    [SerializeField] private GameObject kigurumis;


    [SerializeField] private NodeDatas firstNode;

    private Node curentNode = new Node();
    private NodeDatas nextNodeDatas;
    private int curentDialogue, curentSentence = -1;

    [SerializeField] private float timeBeforeHelp = 1f;
    private float timeSinceLastInput;
    private bool isHelping;

    private static bool gameStarted;


    void Awake()
    {
        uiHandler = GetComponent<UiHandler>();
        sceneHandler = GetComponent<SceneHandler>();
        audioSource = GetComponent<AudioSource>();

        curentNode.SettingUp(firstNode);
    }


    void Update()
    {
        timeSinceLastInput += Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            if (!uiHandler.isWaiting && gameStarted)
            {
                if (uiHandler.isTyping)
                {
                    uiHandler.DisplaySentence(curentNode.dialogues[curentDialogue].Sentences[curentSentence]);
                }
                else
                {
                    NextSentence();
                }
                timeSinceLastInput = 0;
            }
            else
            {
                if (!gameStarted)
                {
                    StartGame();
                }
            }
        }

        if (timeSinceLastInput > timeBeforeHelp && !isHelping)
        {
            isHelping = true;
            if (kigurumis.GetComponent<Canvas>().enabled)
            {
                StartCoroutine(uiHandler.SwipHelp());
            }
            else
            {
                StartCoroutine(uiHandler.ClicHelp());
            }
        }
    }

    public void StartGame()
    {
        Debug.Log("StartGame");
        gameStarted = true;
        audioSource.clip = (firstNode.newMusic);
        audioSource.Play();
        uiHandler.narrativeCanvas.SetActive(true);
        sceneHandler.LoadScene(curentNode.sceneName);

        NextSentence();
    }

    public void NextSentence()
    {
        curentSentence++;

        //Passer à la phrase suivante si il y an a encore
        if (curentNode.dialogues[curentDialogue].Sentences.Length > curentSentence)
        {
            UpdateTextField();
        }

        //Passer au narrateur si il y an a encore
        else
        {
            curentSentence = 0;
            curentDialogue++;

            if (curentNode.dialogues.Length > curentDialogue)
            {
                UpdateTextField();
            }

            else
            {
                if (curentNode.nextNodes.Length == 1)
                {
                    NextNode(0);
                }
                else if (curentNode.kigurumiChoice)
                {
                    kigurumis.GetComponent<Canvas>().enabled = true;
                }
                else
                {
                    uiHandler.EnableOptions(curentNode.options[0], curentNode.options[1]);
                }
            }
        }
    }



    public void SelecteOption(int index)
    {
        uiHandler.DisableOptions();
        NextNode(index);
    }



    public void NextNode(int index)
    {
        curentSentence = 0;
        curentDialogue = 0;
        Debug.Log(curentNode.music);

        if (curentNode.nextNodes[index].newMusic != null)
        {
            audioSource.clip = curentNode.nextNodes[index].newMusic;
            audioSource.Play();
        }

        if (curentNode.nextNodes[index].newSceneName != "" && curentNode.nextNodes[index].newSceneName != curentNode.sceneName)
        {
            sceneHandler.LoadScene(curentNode.nextNodes[index].newSceneName);
        }
        animHandler = GameObject.FindWithTag("Anim Handler").GetComponent<Animator>();

        curentNode.SettingUp(curentNode.nextNodes[index]);
        if (animHandler != null)
        {
            animHandler.SetTrigger(index.ToString());
        }

        UpdateTextField();
    }



    public void EndHelp()
    {
        isHelping = false;
        timeSinceLastInput = 0;
    }



    void UpdateTextField()
    {
        if (curentSentence == 0)
        {
            if (curentNode.dialogues[curentDialogue].playAnim)
            {
                animHandler.SetTrigger("0");
            }

            uiHandler.UpdateTextField(curentNode.dialogues[curentDialogue].speaker,
                                    curentNode.dialogues[curentDialogue].Sentences[curentSentence],
                                    curentNode.dialogues[curentDialogue].waitingTime);
        }
        else
        {
            uiHandler.UpdateTextField(curentNode.dialogues[curentDialogue].speaker,
                                    curentNode.dialogues[curentDialogue].Sentences[curentSentence],
                                    0);
        }
    }
}

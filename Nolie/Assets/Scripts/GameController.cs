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

    [SerializeField] private GameObject kigurumis;


    [SerializeField] private NodeDatas firstNode;

    private Node curentNode = new Node();
    private NodeDatas nextNodeDatas;
    private int curentDialogue, curentSentence = -1;

    [SerializeField] private float timeBeforeHelp = 1f;
    private float timeSinceLastInput;
    private bool isHelping;


    void Start()
    {
        uiHandler = GetComponent<UiHandler>();
        sceneHandler = GetComponent<SceneHandler>();

        curentNode.SettingUp(firstNode);
        sceneHandler.LoadScene(curentNode.sceneName);
        NextSentence();
    }


    void Update()
    {
        timeSinceLastInput += Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
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
        if (curentNode.dialogues[curentDialogue].playAnim && curentSentence == 0)
        {            
            animHandler.SetTrigger("0");
        }
        uiHandler.UpdateTextField(curentNode.dialogues[curentDialogue].speaker,
                                curentNode.dialogues[curentDialogue].Sentences[curentSentence]);
    }
}

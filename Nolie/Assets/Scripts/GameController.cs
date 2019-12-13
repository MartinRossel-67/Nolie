using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private UiHandler uiHandler;
    [SerializeField] private GameObject kigurumis;


    [SerializeField] private NodeDatas firstNode;

    private Node curentNode = new Node();
    private NodeDatas nextNodeDatas;
    private int curentDialogue, curentSentence = -1;



    void Start()
    {
        uiHandler = GetComponent<UiHandler>();

        curentNode.SettingUp(firstNode);
        NextSentence();
    }


    void Update()
    {
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
            }
        }
    }



    public void NextNode(int index)
    {
        curentSentence = 0;
        curentDialogue = 0;

        curentNode.SettingUp(curentNode.nextNodes[index]);

        UpdateTextField();
    }


    void UpdateTextField()
    {
        uiHandler.UpdateTextField(curentNode.dialogues[curentDialogue].speaker,
                                curentNode.dialogues[curentDialogue].Sentences[curentSentence]);
    }
}

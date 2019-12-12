using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private UiHandler uiHandler;


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


    public void NextSentence()
    {
        curentSentence++;
        if (curentNode.dialogues[curentDialogue].Sentences.Length > curentSentence)
        {
            UpdateTextField();
        }
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
                curentSentence = 0;
                curentDialogue = 0;
                NextNode();

                UpdateTextField();
            }
        }
    }


    void NextNode()
    {
        curentNode.SettingUp(curentNode.nextNode);
    }


    public void NewNode(NodeDatas newNode)
    {
        curentSentence = 0;
        curentDialogue = 0;

        curentNode.SettingUp(newNode);

        UpdateTextField();
    }


    void UpdateTextField()
    {
        uiHandler.UpdateTextField(curentNode.dialogues[curentDialogue].speaker,
                                curentNode.dialogues[curentDialogue].Sentences[curentSentence]);
    }
}

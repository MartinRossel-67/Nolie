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
        curentSentence ++;
        if (curentNode.dialogues[curentDialogue].Sentences.Length > curentSentence)
        {
            uiHandler.UpdateTextField(firstNode.dialogues[curentDialogue].speaker,
                                      firstNode.dialogues[curentDialogue].Sentences[curentSentence]);
        }
        else
        {
            curentSentence = 0;
            curentDialogue ++;

            if (curentNode.dialogues.Length > curentDialogue)
            {
                uiHandler.UpdateTextField(firstNode.dialogues[curentDialogue].speaker,
                                          firstNode.dialogues[curentDialogue].Sentences[curentSentence]);
            }

            else
            {
                curentSentence = 0;
                curentDialogue = 0;
                NextNode();

                uiHandler.UpdateTextField(firstNode.dialogues[curentDialogue].speaker,
                                          firstNode.dialogues[curentDialogue].Sentences[curentSentence]);
            }
        }
    }


    void NextNode()
    {
        nextNodeDatas = curentNode.nextNode;
        curentNode.SettingUp(nextNodeDatas);
    }
}

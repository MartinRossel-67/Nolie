using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Dialogue[] dialogues;
    public bool kigurumiChoice;
    public NodeDatas[] nextNodes;

    public void SettingUp(NodeDatas nodeChoices)
    {
        dialogues = nodeChoices.dialogues;
        kigurumiChoice = nodeChoices.kigurumiChoice;
        nextNodes = nodeChoices.nextNodes;
    }
}

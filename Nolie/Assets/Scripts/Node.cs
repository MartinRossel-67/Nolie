using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Dialogue[] dialogues;
    public bool kigurumiChoice;
    public string[] options;
    public NodeDatas[] nextNodes;

    public void SettingUp(NodeDatas nodeChoices)
    {
        dialogues = nodeChoices.dialogues;
        kigurumiChoice = nodeChoices.kigurumiChoice;
        options = nodeChoices.options;
        nextNodes = nodeChoices.nextNodes;
    }
}

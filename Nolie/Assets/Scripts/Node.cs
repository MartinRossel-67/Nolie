using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Dialogue[] dialogues;
    public NodeDatas nextNode;

    public void SettingUp(NodeDatas node)
    {
        dialogues = node.dialogues;
        nextNode = node.nextNode;
    }
}

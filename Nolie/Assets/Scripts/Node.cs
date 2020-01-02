using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Node
{
    public string sceneName;

    public Dialogue[] dialogues;    
    public bool kigurumiChoice;
    public string[] options;
    public NodeDatas[] nextNodes;

    public void SettingUp(NodeDatas nodeChoices)
    {
        sceneName = nodeChoices.newSceneName;

        dialogues = nodeChoices.dialogues;
        kigurumiChoice = nodeChoices.kigurumiChoice;
        options = nodeChoices.options;
        nextNodes = nodeChoices.nextNodes;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Node", menuName = "Node")]
public class NodeDatas : ScriptableObject
{
    [SerializeField] public string newSceneName;

    [Header("Dialogues")]
    [Space(10)]
    [SerializeField] public Dialogue[] dialogues;

    [Header("Nodes")]
    [Space(10)]
    [SerializeField] public bool kigurumiChoice = false;
    [SerializeField] public string[] options;
    [SerializeField] public NodeDatas[] nextNodes;
}

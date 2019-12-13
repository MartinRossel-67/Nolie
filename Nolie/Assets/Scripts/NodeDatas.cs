using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Node", menuName = "Node")]
public class NodeDatas : ScriptableObject
{
    [Header("Dialogues")]
    [SerializeField] public Dialogue[] dialogues;

    [Header("Nodes")]
    [Space(10)]
    [SerializeField] public bool kigurumiChoice = false;
    [SerializeField] public NodeDatas[] nextNodes;
}

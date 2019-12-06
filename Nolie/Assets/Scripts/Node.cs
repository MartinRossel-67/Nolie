using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Node", menuName = "Node")]
public class Node : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] int index;

    [SerializeField][TextArea] string[] Texts;
}

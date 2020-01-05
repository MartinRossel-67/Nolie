using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Bindings;

[System.Serializable]
public struct Dialogue
{
    public string speaker;
    [TextArea] public string[] Sentences;
    public bool playAnim;
    public float waitingTime;
}
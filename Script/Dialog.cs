using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog 
{
    [TextArea(1,2)]
    public string[] sentence;
    public Sprite[] objects;
    public Sprite[] dialogWindows;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EndContent",menuName = "EndContent")]
public class EndContent : ScriptableObject
{
    public Sprite CG;
    
    public Dialog[] content;
    public Queue<Dialog> GetQueue()
    {
        Queue<Dialog> output=new();
        foreach (var item in content)
        {
            output.Enqueue(item);
        }
        return output;
    }
}
[Serializable]
public class Dialog
{
    public string _name;
    [TextArea(1, 3)]
    public string scentence;
}

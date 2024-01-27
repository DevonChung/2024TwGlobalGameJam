using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EndContent",menuName = "EndContent")]
public class EndContent : ScriptableObject
{
    public Sprite CG;
    [TextArea(1,5)]
    public string[] content;
    public Queue<string> GetQueue()
    {
        Queue<string> output=new();
        foreach (string item in content)
        {
            output.Enqueue(item);
        }
        return output;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UIReadyGo : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI ReadyGoText;
    
    UnityEvent OnAfterReadyGo;
    private void Awake()
    {
        
    }
    public void SetOnAfterReadyGo(UnityEvent ReadyGoEvent)
    {
        OnAfterReadyGo = ReadyGoEvent;
    }
    public void StartReadyGo(float readyTime, float goTime)
    {
        StartCoroutine(StartReadyGoCoroutine(readyTime, goTime));
    }
    IEnumerator StartReadyGoCoroutine(float readyTime,float goTime)
    {
        ReadyGoText.text = "Ready...";
        yield return new WaitForSecondsRealtime(readyTime);
        ReadyGoText.text = "GO!";
        yield return new WaitForSecondsRealtime(goTime);
        ReadyGoText.text = string.Empty;
        
        OnAfterReadyGo?.Invoke();
    }

    
}

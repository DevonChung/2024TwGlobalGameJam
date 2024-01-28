using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UIReadyGo : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI ReadyGoText;
    int lerpPointsNumber = 100;
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
        gameObject.SetActive(true);
        StartCoroutine(StartReadyGoCoroutine(readyTime, goTime));
    }
    IEnumerator StartReadyGoCoroutine(float readyTime,float goTime)
    {
        AudioManager.instance.PlayBgm();

        ReadyGoText.text = "Ready...";
        yield return new WaitForSecondsRealtime(readyTime);
        ReadyGoText.text = "GO!";
        float currentAlpha = ReadyGoText.alpha;
        for (int i = 0; i < lerpPointsNumber; i++)
        {
            ReadyGoText.alpha-= currentAlpha / lerpPointsNumber;
            yield return new WaitForSecondsRealtime(goTime/lerpPointsNumber);
        }
        
        ReadyGoText.text = string.Empty;
        
        OnAfterReadyGo?.Invoke();
        gameObject.SetActive(false);
    }

    
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIShower : MonoBehaviour
{
    [SerializeField]
    protected Image cg;
    [SerializeField]
    protected TextMeshProUGUI contentText,nameText;
    [SerializeField]
    protected float typeSpeed=10f;
    protected Queue<Dialog> plots;

    protected bool isShowingCG = false;
    protected bool isTyping = false;
    protected Dialog typingPlot;
    protected void Update()
    {
        if (!isShowingCG) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (!isTyping) ShowNextScentence();
            else ShowAllText();
        }
    }
    protected void ShowPlots(EndContent content)
    {
        plots = content.GetQueue();
        ShowNextScentence();
    }
    protected void ShowNextScentence()
    {
        
        if (plots.Count > 0)
        {
            typingPlot = plots.Dequeue();
            if(typingPlot != null)
            {
                cg.color = Color.white;
                cg.sprite = typingPlot.cg;
            }
            nameText.text = typingPlot._name;
            StartCoroutine(TypingWords(typingPlot.scentence));
        }
        else
        {
            isShowingCG=false;
            EndEnding();
        }
    }
    protected void ShowAllText()
    {
        StopAllCoroutines();
        contentText.text = typingPlot.scentence;
        isTyping=false;
    }
    protected IEnumerator TypingWords(string words)
    {
        isTyping = true;
        contentText.text = string.Empty;
        foreach(var word in words)
        {
            contentText.text += word;
            yield return new WaitForSecondsRealtime(1/typeSpeed);
        }
        isTyping = false;
    }
    protected virtual void EndEnding()
    {

        Debug.Log("Ending");
    }
}

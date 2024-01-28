using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Animator))]
public class SceneTransistor : MonoBehaviour
{
    public UnityEvent OnEndTransist;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void FadeOut()
    {
        animator.Play("FadeOut");
    }
    public void EndTransist()
    {
        OnEndTransist?.Invoke();
    }
}

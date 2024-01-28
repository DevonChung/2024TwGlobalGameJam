using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UITitle : MonoBehaviour
{
    SceneTransistor transistor;
    public void GameStart()
    {
        transistor = FindObjectOfType<SceneTransistor>();
        transistor.OnEndTransist.AddListener(delegate { SceneManager.LoadScene("StartingAnimation"); });
        transistor.FadeOut();
        //SceneManager.LoadScene("StartingAnimation");
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying= false;
#else
        Application.Quit();
#endif
    }
}

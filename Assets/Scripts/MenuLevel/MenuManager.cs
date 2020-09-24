using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject startButton, exitButton;

    void Start()
    {
        FadeOut();
    }

    
    void Update()
    {
        
    }

    void FadeOut()
    {
        startButton.GetComponent<CanvasGroup>().DOFade(1, 0.8f);
        exitButton.GetComponent<CanvasGroup>().DOFade(1, 0.8f).SetDelay(.5f); // SetDelay komutu gecikme yapmasını sağlar kod 0.5 saniye bekledikten sonra çalışır.

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGameLevel()
    {
        SceneManager.LoadScene("gameLevel");
    }
}

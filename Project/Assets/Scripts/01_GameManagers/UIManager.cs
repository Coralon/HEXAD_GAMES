using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Splash Scene UI")]
    [SerializeField] private GameObject menuBtn;

    [Header("Menu Scene UI")]
    [SerializeField] private GameObject playBtn;


    private void Start()
    {
        SetSplashScreenUI(true); // rework
        SetMenuScreenUI(false);
    }

    public void SetSplashScreenUI(bool value)
    {
        menuBtn.SetActive(value);
    }

    public void SetMenuScreenUI(bool value)
    {
        playBtn.SetActive(value);
    }

    public void GoToMenuScene()
    {
        GameManager.Instance.MenuScene();
        SetSplashScreenUI(false);
        SetMenuScreenUI(true);
    }

    public void GoToPlayScene()
    {
        GameManager.Instance.PlayScene();
        SetSplashScreenUI(false);
        SetMenuScreenUI(false);
    }
}

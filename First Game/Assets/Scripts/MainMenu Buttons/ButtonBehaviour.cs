using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] private ButtonAction action;
    [SerializeField] private Button button;

    private enum ButtonAction
    {
        Play,
        Settings,
        Quit
    }

    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }
    }

    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        switch (action)
        {
            case ButtonAction.Play:
                PlayScene();
                break;
            case ButtonAction.Settings:
                SettingsScene();
                break;
            case ButtonAction.Quit:
                QuitGame();
                break;
        }
    }
    
    private void PlayScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void SettingsScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    private void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

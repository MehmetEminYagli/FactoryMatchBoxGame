using UnityEngine;
using System;
using System.ComponentModel;
public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public UIMainMenu mainMenu;
    public UILevelEnd levelEnd;
    public UIOverlay overlay;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;

    }

   
    private void OnGameStateChanged()
    {
        switch (GameManager.Instance.GameState)
        {
            case GameState.Loading:
                break;
            case GameState.Ready:
                mainMenu.gameObject.SetActive(true);
                levelEnd.gameObject.SetActive(false);
                break;
            case GameState.Gameplay:
                mainMenu.gameObject.SetActive(false);
                levelEnd.gameObject.SetActive(false);
                break;
            case GameState.Complete:
                mainMenu.gameObject.SetActive(false);
                levelEnd.gameObject.SetActive(true);
                levelEnd.Show(true);
                break;
            case GameState.Fail:
                mainMenu.gameObject.SetActive(false);
                levelEnd.gameObject.SetActive(true);
                levelEnd.Show(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }



}

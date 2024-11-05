using UnityEngine;
using System;
using System.ComponentModel;
public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public UIMainMenu mainMenu;
    public UILevelEnd levelEnd;
    public UIOverlay overlay;



    [Category("Save")]
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs cleared successfully!");
    }


    [Category("Economy")]
    public void AddCurrency()
    {
        DataManager.Currency += 10;
    }

    public void IncreaseCurrency()
    {
        DataManager.Currency -= 10;
    }
    public int Currency
    {
        get => DataManager.Currency;
        set => DataManager.Currency = value;
    }


}

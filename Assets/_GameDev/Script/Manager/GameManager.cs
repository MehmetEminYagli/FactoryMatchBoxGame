using UnityEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
public class GameManager : MonoSingleton<GameManager>
{

    public AudioManager audioManager;
    public LevelManager levelManager;
    public UIManager uiManager;
    public NotificationManager notificationManager;
    public AdsManager adsManager;

    public Action OnGameStateChanged;
    private GameState _gameState;
    public GameState GameState
    {
        get => _gameState;
        private set
        {
            _gameState = value;
            OnGameStateChanged?.Invoke();
        }
    }


    public float spawnMachineFireRateMin;
    public float spawnMachineFireRateMax;
    public float fireTime;

    public List<MachineSpawnScript> spawnMachineList = new List<MachineSpawnScript>();
    public List<ItemController> itemControllerList = new List<ItemController>();
    protected override void Init()
    {
        base.Init();
        GameState = GameState.Loading;

        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //haptic buraya yazılacak

        audioManager = GetComponentInChildren<AudioManager>();
        levelManager = GetComponentInChildren<LevelManager>();
        uiManager = GetComponentInChildren<UIManager>();
        notificationManager = GetComponentInChildren<NotificationManager>();
        adsManager = GetComponentInChildren<AdsManager>();
    }

    private void Start()
    {
        GameState = GameState.Ready;
        LevelStart();
    }

    public void LevelStart()
    {
        if (GameState != GameState.Ready)
        {
            Debug.LogError("Game State is not Ready", this);
            return;
        }

        GameState = GameState.Gameplay;
        

    }


    public void LevelFinish(bool isSuccess)
    {

        GameState = isSuccess ? GameState.Complete : GameState.Fail;
       
        if (isSuccess)
        {
            DataManager.CurrentLevelIndex++;
        }
    }

    public void RegisterMachine(MachineSpawnScript machine)
    {
        if (!spawnMachineList.Contains(machine))
            spawnMachineList.Add(machine);
    }

    public void RegisterItemScoreController(ItemController itemScore)
    {
        if (!itemControllerList.Contains(itemScore))
            itemControllerList.Add(itemScore);
    }



   


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

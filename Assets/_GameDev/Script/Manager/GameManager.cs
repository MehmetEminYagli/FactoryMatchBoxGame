using UnityEngine;
using System;
public class GameManager : MonoSingleton<GameManager>
{

    public AudioManager audioManager;
    public LevelManager levelManager;
    public UIManager uiManager;
    public NotificationManager notificationManager;



    protected override void Init()
    {
        base.Init();


        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //haptic buraya yazılacak

        audioManager = GetComponentInChildren<AudioManager>();
        levelManager = GetComponentInChildren<LevelManager>();
        uiManager = GetComponentInChildren<UIManager>();
        notificationManager = GetComponentInChildren<NotificationManager>();
    }

    void Start()
    {


    }




    public void LevelFinish(bool isSuccess)
    {

        if (isSuccess)
        {
            DataManager.CurrentLevelIndex++;
        }
    }




}

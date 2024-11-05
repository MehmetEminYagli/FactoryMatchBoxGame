using UnityEngine;
using System;
public class GameManager : MonoSingleton<GameManager>
{

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private NotificationManager notificationManager;



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


    public void LevelStart()
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

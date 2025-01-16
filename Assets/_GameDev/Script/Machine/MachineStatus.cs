using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;
public class MachineStatus : MonoBehaviour
{
    private MachineSpawnScript machineSpawnScript;
    [SerializeField] private GameObject machineBrokeEffect;
    [SerializeField] private GameObject machineBurnEffect;
    [SerializeField] private GameObject fireman;

    public Action OnMachineStateChanged;
    private MachineState _machineState;
    private MachineState previousState;
    public MachineState MachineState
    {
        get => _machineState;
        private set
        {
            previousState = _machineState;
            _machineState = value;
            OnMachineStateChanged?.Invoke();
        }
    }


    void Start()
    {
        machineSpawnScript = GetComponent<MachineSpawnScript>();
        EffectStartState();
        OnMachineStateChanged += OnMachineStateChangedEvent;
        MachineState = MachineState.Good;
        StartCoroutine(RandomMachineStatusChange());
    }


    private void EffectStartState()
    {
        machineBrokeEffect.SetActive(false);
        machineBurnEffect.SetActive(false);
    }


    private void OnMachineStateChangedEvent()
    {
        switch (_machineState)
        {
            case MachineState.Good:
                machineSpawnScript.SetIsBroken(false);
                machineBurnEffect.SetActive(false);
                machineBrokeEffect.SetActive(false);
                fireman.GetComponentInChildren<FiremanController>().WaitAndMoveSafeArea();
                if (previousState == MachineState.Fire)
                {
                    StartCoroutine(RandomMachineStatusChange());
                }
                break;
            case MachineState.Fire:
                machineSpawnScript.SetIsBroken(true);
                machineBurnEffect.SetActive(true);
                machineBrokeEffect.SetActive(false);
                break;
            case MachineState.Broken:
                machineSpawnScript.SetIsBroken(true);
                machineBurnEffect.SetActive(false);
                machineBrokeEffect.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    IEnumerator RandomMachineStatusChange()
    {
        float randomWaitTime = UnityEngine.Random.Range(GameManager.Instance.spawnMachineFireRateMin, GameManager.Instance.spawnMachineFireRateMax);
        yield return new WaitForSeconds(randomWaitTime);
        MachineState = MachineState.Fire;

        //MachineState = (UnityEngine.Random.Range(0, 2) == 0) ? MachineState.Fire : MachineState.Broken;
    }

    public void MachineGoodBtn()
    {
        fireman.GetComponentInChildren<FiremanController>().BtnFireMan();
        StartCoroutine(MachineStatusGood());
    }

    IEnumerator MachineStatusGood()
    {
        float randomWaitTime = UnityEngine.Random.Range(3, 10);
        yield return new WaitForSeconds(GameManager.Instance.fireTime);
        foreach (Transform child in machineBurnEffect.transform)
        {
            child.DOScale(new Vector3(0, 0, 0), .4f).OnComplete(() =>
            {
                MachineState = MachineState.Good;
                child.transform.DOScale(Vector3.one, .1f);
            });
        }
    }

   
}

public enum MachineState
{
    Broken = 0,
    Fire = 1,
    Good = 2,
}

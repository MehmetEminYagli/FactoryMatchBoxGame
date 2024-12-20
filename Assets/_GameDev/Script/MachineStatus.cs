using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;
public class MachineStatus : MonoBehaviour
{
    private MachineSpawnScript machineSpawnScript;
    [SerializeField] private GameObject machineBrokeEffect;
    [SerializeField] private GameObject machineBurnEffect;


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
        float randomWaitTime = UnityEngine.Random.Range(30, 90);
        Debug.Log(gameObject.name + "şu kadar süre => " + randomWaitTime);
        yield return new WaitForSeconds(randomWaitTime);
        MachineState = MachineState.Fire;
        //bug var belirli bir yerden sonra sürekli çalışıyor
        //machine sönme olayı yanan makineye belirli bir süre basarsa o sönme geçer gibi birşey düşünüyorum

        //MachineState = (UnityEngine.Random.Range(0, 2) == 0) ? MachineState.Fire : MachineState.Broken;
    }

    public void MachineGoodBtn()
    {
        StartCoroutine(MachineStatusGood());
    }

    IEnumerator MachineStatusGood()
    {
        float randomWaitTime = UnityEngine.Random.Range(3, 10);
        yield return new WaitForSeconds(.5f);
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

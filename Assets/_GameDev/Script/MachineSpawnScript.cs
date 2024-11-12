using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MachineSpawnScript : MonoBehaviour
{
    private MachineController machineController;
    [SerializeField] private List<GameObject> spawnableObjects;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool isSpawn = true;
    [SerializeField] private bool isBroken = false;
    [SerializeField] private int spawnedCount;
    public bool GetisSpawn()
    {
        return isSpawn;
    }
    public bool GetisBroken()
    {
        return isBroken;
    }
    public void SetIsBroken(bool broke)
    {
        isBroken = broke;
    }
    void Start()
    {
        GameManager.Instance.RegisterMachine(this);
        ComponentStart();
        StartCoroutine(SpawnItemCoroutine());
        StartCoroutine(stop());
        isBroken = false;
    }


    private void ComponentStart()
    {
        machineController = GetComponent<MachineController>();
    }

    [SerializeField] private int spawnRate;
    IEnumerator SpawnItemCoroutine()
    {
        while (isSpawn)
        {
            float randomTime = Random.Range(spawnRate - 2, spawnRate + 5);

            yield return new WaitForSeconds(randomTime);

            if (isBroken == false)
            {
                SpawnFactoryItem();
                spawnedCount++;
                if (spawnedCount == GameManager.Instance.levelManager.controlFinishCount)
                {
                    Debug.Log("nesne spawn olmayı bırakıyor");
                    isSpawn = false;
                }
            }
            else
            {
                Debug.Log("makine bozuldu");
            }
        }
    }


    private void SpawnFactoryItem()
    {
        spawnableObjects = machineController.GetSpawnListScript().GetSpawnList();
        if (spawnableObjects.Count == 0) return;
        int randomIndex = Random.Range(0, spawnableObjects.Count);
        GameObject selectedItem = spawnableObjects[randomIndex];
        GameObject spawnedObject = Instantiate(selectedItem, spawnPoint.position, Quaternion.identity, spawnPoint);
        GameManager.Instance.levelManager.AddSpawnedObject(spawnedObject.gameObject);
        GameManager.Instance.levelManager.ControlSpawnedID(spawnedObject.gameObject);




    }

    public void StopMachinePalet()
    {
        machineController.GetConveyorScript().StopMoveObject();
    }
    public void StartMachinePalet()
    {
        machineController.GetConveyorScript().StartMoveObject();
    }

    IEnumerator stop()
    {
        while (true)
        {
            //yield return new WaitForSeconds(1f);
            //Debug.Log("bekliyor");
            //StopMachinePalet();
            yield return new WaitForSeconds(1f);
            StartMachinePalet();
        }
    }
}

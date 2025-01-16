using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MachineSpawnScript : MonoBehaviour
{
    public MachineController machineController;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool isSpawn = true;
    [SerializeField] private bool isBroken = false;
    [SerializeField] private int spawnedCount;
    [SerializeField] public GameObject spawnListScript;
    private List<GameObject> spawnList;

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
        spawnList = spawnListScript.GetComponent<SpawnListGameobject>().GetSpawnList();
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
                if (spawnList.Count == 0)
                {
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
        spawnList = spawnListScript.GetComponent<SpawnListGameobject>().GetSpawnList();
        if (spawnList.Count == 0) return;
        int randomIndex = Random.Range(0, spawnList.Count);
        GameObject selectedItem = spawnList[randomIndex];
        GameObject spawnedObject = Instantiate(selectedItem, spawnPoint.position, Quaternion.identity, spawnPoint);
        GameManager.Instance.levelManager.AddSpawnedObject(spawnedObject.gameObject);
        GameManager.Instance.levelManager.ControlSpawnedID(spawnedObject.gameObject);


        spawnListScript.GetComponent<SpawnListGameobject>().ControlItemSpawn(spawnedObject);
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

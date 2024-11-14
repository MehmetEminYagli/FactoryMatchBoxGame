using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MachineSpawnScript : MonoBehaviour
{
    public MachineController machineController;
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

    public void RemoveSpawnableItemByID(int itemID)
    {
        // Find the object with the matching item ID
        GameObject itemToRemove = spawnableObjects.Find(obj => obj.GetComponent<FactoryItem>().GetItemID() == itemID);
        // If the item is found, remove it
        if (itemToRemove != null)
        {
            spawnableObjects.Remove(itemToRemove);
        }
    }
    public List<GameObject> GetSpawnableObjects()
    {
        return spawnableObjects;
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
                if (spawnableObjects.Count ==0)
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

    public void SetIsSpawn(bool spawn)
    {
        isSpawn = spawn;
    }

    private void SpawnFactoryItem()
    {
        spawnableObjects = machineController.GetSpawnListScript().GetSpawnList();
        if (spawnableObjects.Count == 0) return;

        int randomIndex = Random.Range(0, spawnableObjects.Count);
        GameObject selectedItem = spawnableObjects[randomIndex];

        int itemID = selectedItem.GetComponent<FactoryItem>().GetItemID();

        // Check if this item can still be spawned
        if (GameManager.Instance.levelManager.CanSpawnItem(itemID,this))
        {
            GameObject spawnedObject = Instantiate(selectedItem, spawnPoint.position, Quaternion.identity, spawnPoint);
            GameManager.Instance.levelManager.AddSpawnedObject(spawnedObject.gameObject);
            GameManager.Instance.levelManager.ControlSpawnedID(spawnedObject.gameObject);

           
        }
        else
        {
            Debug.Log($"Cannot spawn item with ID {itemID} - spawn limit reached.");
        }


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

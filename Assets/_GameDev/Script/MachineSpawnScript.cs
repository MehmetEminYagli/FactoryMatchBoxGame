using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MachineSpawnScript : MonoBehaviour
{
    private MachineController machineController;
    [SerializeField] private List<GameObject> spawnableObjects;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool isSpawn = true;
 

    void Start()
    {
        ComponentStart();
        StartCoroutine(SpawnItemCoroutine());
        StartCoroutine(stop());
    }
    private void ComponentStart()
    {
        machineController = GetComponent<MachineController>();
    }

    [SerializeField] private int SpawnRate;
    IEnumerator SpawnItemCoroutine()
    {
        while (isSpawn)
        {
            float randomTime = Random.Range(SpawnRate-2, SpawnRate+5);

            yield return new WaitForSeconds(randomTime);
            SpawnFactoryItem();
        }
    }


    private void SpawnFactoryItem()
    {
        spawnableObjects = machineController.GetSpawnListScript().GetSpawnList();
        if (spawnableObjects.Count == 0) return;

        int randomIndex = Random.Range(0, spawnableObjects.Count);
        GameObject selectedItem = spawnableObjects[randomIndex];

        Instantiate(selectedItem, spawnPoint.position, Quaternion.identity,transform.parent);
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





    // Update is called once per frame
    void Update()
    {

    }
}

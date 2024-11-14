using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class LevelManager : MonoBehaviour
{
    public bool iscontrol = false;

    public List<MachineSpawnScript> spawnMachineList;
    private List<ItemController> itemControllers;
    [SerializeField] private List<GameObject> spawnedObjects;

    public int minRequiredCount;
    public int MaxRequiredCount;


    void Start()
    {
        spawnMachineList = GameManager.Instance.spawnMachineList;
        itemControllers = GameManager.Instance.itemControllerList;
        spawnedObjects = new List<GameObject>();
        RandomRequiredItemCountGenerator();
    }

    public void RemoveSpawnedObject(GameObject destroyedObject)
    {
        spawnedObjects.Remove(destroyedObject);
    }
    public void AddSpawnedObject(GameObject spawnedObject)
    {
        if (!spawnedObjects.Contains(spawnedObject))
        {
            spawnedObjects.Add(spawnedObject);
        }
    }
    public int spawnObjectID_1;
    public int spawnObjectID_2;
    public int spawnObjectID_3;
    public void ControlSpawnedID(GameObject newObject)
    {
        // Check the new object's ID and increment the corresponding counter
        int itemID = newObject.GetComponent<FactoryItem>().GetItemID();

        switch (itemID)
        {
            case 1:
                spawnObjectID_1++;
                break;
            case 2:
                spawnObjectID_2++;
                break;
            case 3:
                spawnObjectID_3++;
                break;
        }

    }


    //bunu düzelticem
    [SerializeField] private List<int> machineIDList;
    [SerializeField] private List<int> requiredCountList;

    public bool CanSpawnItem(int itemID, MachineSpawnScript machine)
    {
        // Assuming requiredCountList indices correspond to item IDs (1, 2, 3)
        switch (itemID)
        {
            case 1:
                if (spawnObjectID_1 >= requiredCountList[1])
                {
                    machine.GetSpawnableObjects().RemoveAt(requiredCountList[1]);
                    return false;
                }
                break;
            case 2:
                if (spawnObjectID_2 >= requiredCountList[0])
                {
                    machine.GetSpawnableObjects().RemoveAt(requiredCountList[0]);
                    return false;
                }
                break;
            case 3:
                if (spawnObjectID_3 >= requiredCountList[2])
                {
                    machine.GetSpawnableObjects().RemoveAt(requiredCountList[2]);
                    return false;
                }
                break;
        }
        return true;
    }

    private void Update()
    {
        ControlWinOrFail();
    }
    public void ControlWinOrFail()
    {
        if (spawnMachineList.All(machine => machine.GetisSpawn() == false))
        {
            if (spawnedObjects.Count == 0)
            {
                iscontrol = true;
                if (iscontrol)
                {
                    bool allCorrect = itemControllers.All(controller => controller.GetItemScoreController().ControlWinOrFail());
                    //listdeki tüm sayıları leveli geçmek için gerekli olan sayı ile karşılaştırıyor biri eşit değilse false değer döndürüyor hepsi eşit ise true değer döndüyor
                    //oyuncu win mi fail mi kontrol et
                    if (allCorrect)
                    {
                        GameManager.Instance.LevelFinish(true);
                        Debug.Log("oyunu kazanın");
                    }
                    else
                    {
                        GameManager.Instance.LevelFinish(false);
                        Debug.Log("kaybettin loser");
                    }
                }
            }
            else {/*Debug.Log("tüm spawn olan nesneler daha destory olmadı");*/}
        }
        else {/*Debug.Log("tüm makineler daha spawn etmeyi bırakmadı");*/}
    }

    public void OnExtinguishFireButtonPressed()
    {
        foreach (MachineSpawnScript machine in GameManager.Instance.spawnMachineList)
        {
            MachineStatus machineStatus = machine.GetComponent<MachineStatus>();
            if (machineStatus != null && machine.GetisBroken() == true)
            {
                Debug.Log($"{gameObject.name} fire extinguished.");
                machineStatus.MachineGoodBtn(); // Extinguish fire if machine is in Fire state
            }
            else
            {
                Debug.Log("makine yanmıyor");
            }
        }
    }

    public List<int> generatedCounts = new List<int>();
    public void RandomRequiredItemCountGenerator()
    {
        foreach (ItemController itemScore in GameManager.Instance.itemControllerList)
        {
            int requiredCount = itemScore.GetItemScoreController().GenerateRandomRequiredCount();


            generatedCounts.Add(requiredCount);
            itemScore.GetItemScoreController().SetRequiredItemCount(requiredCount);
            requiredCountList.Add(itemScore.GetItemScoreController().GetRequiredItemCount());
            machineIDList.Add(itemScore.GetMachineController().GetMachineID());


        }
    }



    public static void ReloadScene()
    {
        DOTween.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

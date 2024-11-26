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
    public List<int> itemRequiredCount;
    public List<int> machineIDList;



    public void AddMachineID(int machineID)
    {
        if (!machineIDList.Contains(machineID))
        {
            machineIDList.Add(machineID);
        }
    }
    public void AddRequiredCount(int requiredCount)
    {
        itemRequiredCount.Add(requiredCount);
    }
    void Start()
    {
        spawnMachineList = GameManager.Instance.spawnMachineList;
        itemControllers = GameManager.Instance.itemControllerList;
        spawnedObjects = new List<GameObject>();
        PlusItemSpawn();

        if (spawnedObjects.Count == 0)
        {
            InvokeRepeating(nameof(ControlWinOrFail), 5f, 5f);

        }
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

    public int spawnObjectID_0;
    public int spawnObjectID_1;
    public int spawnObjectID_2;
    public void ControlSpawnedID(GameObject newObject)
    {
        // Check the new object's ID and increment the corresponding counter
        int itemID = newObject.GetComponent<FactoryItem>().GetItemID();

        switch (itemID)
        {
            case 1:
                spawnObjectID_0++;
                break;
            case 2:
                spawnObjectID_1++;
                break;
            case 3:
                spawnObjectID_2++;
                break;
        }
    }

    public void PlusItemSpawn()
    {
        int randomCount0 = Random.Range(1, 5);
        int randomCount1 = Random.Range(1, 5);
        int randomCount2 = Random.Range(1, 5);
        GameManager.Instance.levelManager.spawnObjectID_0 -= randomCount0;
        GameManager.Instance.levelManager.spawnObjectID_1 -= randomCount1;
        GameManager.Instance.levelManager.spawnObjectID_2 -= randomCount2;
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
                        GameManager.Instance.adsManager.adsInterstitial.ShowInterstitialAd();
                        Debug.Log("oyunu kazanın");
                    }
                    else
                    {
                        GameManager.Instance.LevelFinish(false);
                        GameManager.Instance.adsManager.adsInterstitial.ShowInterstitialAd();
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

    public static void ReloadScene()
    {
        DOTween.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}

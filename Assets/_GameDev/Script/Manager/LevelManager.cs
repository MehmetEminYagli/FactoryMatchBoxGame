using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class LevelManager : MonoBehaviour
{
    [Header("kontrol")]
    public int controlFinishCount; //burası şu kadar nesne spawn olduktan sonra spawn olmayı durdur yapıcaz ve kontrol kısmına geçicek
    /*[SerializeField] private int RequiredTrueItemCount;*/ // leveli geçmek için gerekli olan doğru item sayısına bakıcağız

    public bool iscontrol = false;

    private List<MachineSpawnScript> spawnMachineList;
    private List<ItemScoreController> itemScoreControllers;
    [SerializeField] private List<GameObject> spawnedObjects;


    void Start()
    {
        spawnMachineList = GameManager.Instance.spawnMachineList;
        itemScoreControllers = GameManager.Instance.itemScoreControllerList;
        spawnedObjects = new List<GameObject>();
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
                    bool allCorrect = itemScoreControllers.All(controller => controller.ControlWinOrFail());
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
            else
            {
                //Debug.Log("tüm spawn olan nesneler daha destory olmadı");
            }

        }
        else
        {
            //Debug.Log("tüm makineler daha spawn etmeyi bırakmadı");
        }
    }

    public void OnExtinguishFireButtonPressed()
    {
        foreach (var machine in GameManager.Instance.spawnMachineList)
        {
            MachineStatus machineStatus = machine.GetComponent<MachineStatus>();
            if (machineStatus != null && machine.GetisBroken() == true)
            {
                Debug.Log($"{gameObject.name} fire extinguished.");
                machineStatus.MachineGoodBtn(); // Extinguish fire if machine is in Fire state
            }
        }
    }


    public static void ReloadScene()
    {
        DOTween.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

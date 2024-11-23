using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnListGameobject : MonoBehaviour
{

    [SerializeField] private List<GameObject> spawnList;
    private LevelManager levelManager;
    void Start()
    {
        ControlItemID();
        levelManager = GameManager.Instance.levelManager;
    }


    public List<GameObject> GetSpawnList()
    {
        return spawnList;
    }


    public void ControlItemID()
    {
        foreach (GameObject obj in spawnList)
        {
            if (!obj.GetComponent<FactoryItem>())
            {
                Debug.LogError("spawn listte factory item scriptti bulunamadı");
            }
        }

    }

    public void ControlItemSpawn(GameObject spawnobject)
    {
        int itemid = spawnobject.GetComponent<FactoryItem>().GetItemID();

        // Makine için gerekli olan nesne sayısını ve mevcut üretilen nesne sayısını bir sözlükte tutuyoruz.
        Dictionary<int, int> spawnObjectCounts = new Dictionary<int, int>
        {
            { 1, levelManager.spawnObjectID_0 },
            { 2, levelManager.spawnObjectID_1 },
            { 3, levelManager.spawnObjectID_2 }
        };

        // Gerekli sayıları da sözlükte tutuyoruz.
        Dictionary<int, int> requiredCounts = new Dictionary<int, int>
        {
            { 1, levelManager.itemRequiredCount[0] },
            { 2, levelManager.itemRequiredCount[1] },
            { 3, levelManager.itemRequiredCount[2] }
        };

        // Eğer itemid sözlükte geçerli ise işlemi gerçekleştiriyoruz.
        if (spawnObjectCounts.ContainsKey(itemid) && requiredCounts.ContainsKey(itemid))
        {
            if (spawnObjectCounts[itemid] >= requiredCounts[itemid])
            {
                // Güvenli bir şekilde itemleri kaldırıyoruz.
                for (int i = spawnList.Count - 1; i >= 0; i--)
                {
                    if (spawnList[i].GetComponent<FactoryItem>().GetItemID() == itemid)
                    {
                        spawnList.RemoveAt(i);
                    }
                }
                Debug.Log($"ID'si {itemid} olan itemler kaldırıldı.");
            }
        }
        else
        {
            Debug.LogError($"Geçersiz item ID'si: {itemid}");
        }

    }



}

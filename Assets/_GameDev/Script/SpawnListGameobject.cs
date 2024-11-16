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
    //1,3,2
    public void ControlItemSpawn(GameObject spawnobject)
    {
        int itemid = spawnobject.GetComponent<FactoryItem>().GetItemID();

        if (itemid == 1)
        {
            if (levelManager.spawnObjectID_0 >= levelManager.itemRequiredCount[0])
            {
                // Use a for loop to safely remove elements
                for (int i = spawnList.Count - 1; i >= 0; i--)
                {
                    if (spawnList[i].GetComponent<FactoryItem>().GetItemID() == itemid)
                    {
                        spawnList.RemoveAt(i);
                    }
                    else
                    {
                        Debug.Log("birinci id'li item bitti");
                    }
                }
            }

        }
        else if (itemid == 2)
        {
            if (levelManager.spawnObjectID_1 >= levelManager.itemRequiredCount[2])
            {
                // Use a for loop to safely remove elements
                for (int i = spawnList.Count - 1; i >= 0; i--)
                {
                    if (spawnList[i].GetComponent<FactoryItem>().GetItemID() == itemid)
                    {
                        spawnList.RemoveAt(i);
                    }
                    else
                    {
                        Debug.Log("ikinci id'li item bitti");
                    }
                }
            }

        }
        else if (itemid == 3)
        {
            if (levelManager.spawnObjectID_2 >= levelManager.itemRequiredCount[1])
            {
                // Use a for loop to safely remove elements
                for (int i = spawnList.Count - 1; i >= 0; i--)
                {
                    if (spawnList[i].GetComponent<FactoryItem>().GetItemID() == itemid)
                    {
                        spawnList.RemoveAt(i);
                    }
                    else
                    {
                        Debug.Log("üçümcü id'li item bitti");
                    }
                }
            }
        }


    }



}

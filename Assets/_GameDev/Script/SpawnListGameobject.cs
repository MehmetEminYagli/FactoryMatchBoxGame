using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnListGameobject : MonoBehaviour
{

    [SerializeField] private List<GameObject> spawnList;
    void Start()
    {
        ControlItemID();
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
   
}

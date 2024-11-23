using UnityEngine;

public class FactoryItem : MonoBehaviour
{
    [SerializeField] private int itemID;
    [SerializeField] private string itemName;


    public int GetItemID()
    {
        return itemID;
    }


    public string GetItemName()
    {
        return itemName;
    }
}

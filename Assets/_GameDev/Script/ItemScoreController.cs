using UnityEngine;

public class ItemScoreController : MonoBehaviour
{
    [SerializeField] private ItemController itemController;
    [SerializeField] public int trueItemCount;



    void Start()
    {
        itemController = GetComponent<ItemController>();
        GameManager.Instance.RegisterItemScoreController(this);
    }

    public int getTrueItemCount()
    {
        return trueItemCount;
    }






    void Update()
    {
        
    }
}

using UnityEngine;
using TMPro;
using DG.Tweening;
public class ItemScoreController : MonoBehaviour
{
    [SerializeField] private ItemController itemController;
    public int trueItemCount;
    private int requiredItemCount;
    [SerializeField] private TextMeshProUGUI textControl;

    void Start()
    {
        itemController = GetComponent<ItemController>();
        GameManager.Instance.RegisterItemScoreController(this);
        GenerateRandomRequiredCount();
    }


    public void SetRequiredItemCount(int count)
    {
        requiredItemCount = count;
        Debug.Log("sayılar aynı yeni sayı üretildi. Yeni sayi => " + requiredItemCount);
    }

    public int GenerateRandomRequiredCount()
    {
        return requiredItemCount = Random.Range(GameManager.Instance.levelManager.minRequiredCount,GameManager.Instance.levelManager.MaxRequiredCount);
    }
    public int getTrueItemCount()
    {
        return trueItemCount;
    }
    public void TrueText()
    {
        trueItemCount++;
     

        textControl.color = Color.green;
        textControl.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), .25f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            textControl.transform.DOScale(Vector3.one, .25f).OnComplete(() =>
            {
                TextColorController();
            });
        });
    }
    public void FalseText()
    {
        trueItemCount--;

        textControl.color = Color.red;
        textControl.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), .3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            textControl.transform.DOScale(Vector3.one, .3f).SetEase(Ease.OutBack).OnComplete(()=> 
            {
                TextColorController();
            });
        });
    }

    private void TextColorController()
    {
        if (trueItemCount > requiredItemCount)
        {
            textControl.color = Color.green;
        }
        else if (trueItemCount >= 0)
        {
            textControl.color = Color.white;
        }
        else if (trueItemCount < 0)
        {
            textControl.color = Color.red;
        }
    }
    public bool ControlWinOrFail()
    {
        Debug.Log(this.name + " gerekli olan true item sayisi => " + requiredItemCount);
        if (trueItemCount >= requiredItemCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Update()
    {
        textControl.text = trueItemCount.ToString() + " / " + requiredItemCount.ToString();
    }
}

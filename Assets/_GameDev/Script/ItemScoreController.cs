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
        requiredItemCount = Random.Range(1, 5);
        Debug.Log(this.name + " " + requiredItemCount);
    }

    public int getTrueItemCount()
    {
        return trueItemCount;
    }
    public void TrueText()
    {
        trueItemCount++;
        if (trueItemCount > 0)
        {
            textControl.color = Color.green;
        }
        else if (trueItemCount == 0)
        {
            textControl.color = Color.white;
        }

        textControl.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), .25f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            textControl.transform.DOScale(Vector3.one, .25f);
        });
    }
    public void FalseText()
    {
        trueItemCount--;
        if (trueItemCount < 0)
        {
            textControl.color = Color.red;
        }
        else if (trueItemCount == 0)
        {
            textControl.color = Color.white;
        }
        textControl.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), .3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            textControl.transform.DOScale(Vector3.one, .3f).SetEase(Ease.OutBack);
        });
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

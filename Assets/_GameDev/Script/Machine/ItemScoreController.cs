using UnityEngine;
using TMPro;
using DG.Tweening;
public class ItemScoreController : MonoBehaviour
{
    public int trueItemCount;
    private int requiredItemCount;
    [SerializeField] private TextMeshProUGUI textControl;

    void Start()
    {
        GenerateRandomRequiredCount();
        GameManager.Instance.levelManager.AddRequiredCount(getRequiredItemCount());
    }

    public int GenerateRandomRequiredCount()
    {
        return requiredItemCount = Random.Range(GameManager.Instance.levelManager.minRequiredCount, GameManager.Instance.levelManager.MaxRequiredCount);
    }
    public int getTrueItemCount()
    {
        return trueItemCount;
    }
    public int getRequiredItemCount()
    {
        return requiredItemCount;
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
            textControl.transform.DOScale(Vector3.one, .3f).SetEase(Ease.OutBack).OnComplete(() =>
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

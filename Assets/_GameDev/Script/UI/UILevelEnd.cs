using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UILevelEnd : MonoBehaviour
{
    [Header("Success Panel")]
    [SerializeField]
    private GameObject successPanel;

    [SerializeField] private List<Sprite> successIcons;
    [SerializeField] private Image successIconImage;
    [SerializeField] private GameObject successShine;
    private bool isSuccessIconChosen = false;
    [Header("Fail Panel")]
    [SerializeField]
    private GameObject failPanel;

    [SerializeField] private List<Sprite> failIcons;
    [SerializeField] private Image failIconImage;
    [SerializeField] private GameObject failShine;
    private bool isFailIconChosen = false;

    public void Show(bool isSuccess)
    {
        if (isSuccess)
        {
            ShowSuccessPanel();
        }
        else
        {
            ShowFailPanel();
        }
    }

    private void ShowSuccessPanel()
    {
        if (!isSuccessIconChosen)
        {
            RandomTrueIcon();
            isSuccessIconChosen = true;
        }

        successIconImage.transform.localScale = Vector3.zero;
        successIconImage.transform.DOScale(1, .2f)
            .SetDelay(.25f)
            .SetEase(Ease.OutBack);

        successShine.transform.localScale = Vector3.zero;
        successShine.transform.DOScale(1, .5f)
            .SetDelay(.25f);

        successPanel.SetActive(true);
    }

  

    private void ShowFailPanel()
    {
        if (!isFailIconChosen)
        {
            RandomFalseIcon();
            isFailIconChosen = true;
        }

        failIconImage.transform.localScale = Vector3.zero;
        failIconImage.transform.DOScale(1, .2f)
            .SetDelay(.25f)
            .SetEase(Ease.OutBack);

        failShine.transform.localScale = Vector3.zero;
        failShine.transform.DOScale(1, .5f)
            .SetDelay(.25f);


        failPanel.SetActive(true);
    }

    private void RandomTrueIcon()
    {
        int randomSprite = Random.Range(0, successIcons.Count);
        Sprite SelectedIcon = successIcons[randomSprite];
        successIconImage.sprite = SelectedIcon;
    }
    private void RandomFalseIcon()
    {
        int randomSprite = Random.Range(0, failIcons.Count);
        Sprite SelectedIcon = failIcons[randomSprite];
        failIconImage.sprite = SelectedIcon;
    }


    public void OnNextLevelButtonClick()
    {
        LevelManager.ReloadScene();
    }

    public void OnRestartLevelButtonClick()
    {
        LevelManager.ReloadScene();
    }

}

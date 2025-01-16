using UnityEngine;
using TMPro;
public class UIOverlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    //[SerializeField] private GameObject noInternetPopUp;


    private void OnEnable()
    {
        DataManager.OnCurrencyUpdated += UpdateCurrencyText;
    }

    private void Start()
    {
        UpdateCurrencyText(DataManager.Currency);
        InvokeRepeating(nameof(CheckForInternetConnection), 1f, 3f);
    }

    private void CheckForInternetConnection()
    {
        var isReachable = Application.internetReachability != NetworkReachability.NotReachable;
        //noInternetPopUp.SetActive(!isReachable);
    }

    private void UpdateCurrencyText(int value)
    {
        currencyText.text = value.ToString();
    }

    private void OnDisable()
    {
        DataManager.OnCurrencyUpdated -= UpdateCurrencyText;
    }


}






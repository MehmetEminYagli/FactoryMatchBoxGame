using UnityEngine;
using DG.Tweening;

public class ItemController : MonoBehaviour
{


    [SerializeField] private MachineController machine;
    [SerializeField] private ItemScoreController itemScore;
    [SerializeField] private GameObject controlMachineObject;
    [SerializeField] private GameObject trueMachineEffect;
    [SerializeField] private GameObject falseMachineEffect;
    [SerializeField] private float trueDestoryDelayTime;

    private void Awake()
    {
        GameManager.Instance.RegisterItemScoreController(this);
        machine = GetComponentInParent<MachineController>();
        itemScore = GetComponent<ItemScoreController>();
        SetTrueMachineEffect(false);
        SetFalseMachineEffect(false);
    }

    private void Start()
    {
        //GameManager.Instance.levelManager.AddMachineIDandRequiredCount(machine.GetMachineID(), itemScore.getRequiredItemCount());
    }
    public ItemScoreController GetItemScoreController()
    {
        return itemScore;
    }

    public MachineController GetMachineController()
    {
        return machine;
    }
    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FactoryItem>(out FactoryItem factoryItem))
        {
            Vector3 initialScale = factoryItem.transform.localScale;
            SetTrueMachineEffect(false);
            SetFalseMachineEffect(false);

            if (factoryItem.GetItemID() == machine.GetMachineID())
            {
                GameManager.Instance.AddCurrency();
                SetTrueMachineEffect(true);
                itemScore.TrueText();
                DestroyFactoryItem(factoryItem, (trueDestoryDelayTime));
            }
            else
            {
                GameManager.Instance.IncreaseCurrency();
                SetFalseMachineEffect(true);
                itemScore.FalseText();
                DestroyFactoryItem(factoryItem, (.5f));

                //haptic added
                HapticManager.GenerateHaptic(PresetType.Warning);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<FactoryItem>(out FactoryItem factoryItem))
        {
            SetTrueMachineEffect(false);
            SetFalseMachineEffect(false);
        }
    }

    private void DestroyFactoryItem(FactoryItem itemscript, float delayTime)
    {
        itemscript.transform.DOScale(Vector3.zero, .5f).SetEase(Ease.InOutQuad).SetDelay(delayTime).OnComplete(() =>
        {
            GameManager.Instance.levelManager.RemoveSpawnedObject(itemscript.gameObject);
            Destroy(itemscript.gameObject);
            SetTrueMachineEffect(false);
            SetFalseMachineEffect(false);
        });
    }
    private void SetTrueMachineEffect(bool isActive)
    {
        trueMachineEffect.SetActive(isActive);
    }
    private void SetFalseMachineEffect(bool isActive)
    {
        falseMachineEffect.SetActive(isActive);
    }



}

//eski sistemdede nesne yukarı kalkıyor sallanıyor sonra yoluna devam ediyordu. bunları kaldırıp bunu yerine makine ekledim makine efekt çıkarsın istiyorum. #region //private Vector3 initialScale; //private Rigidbody itemRb; //private void Start() //{ // machine = GetComponentInParent<MachineController>(); //} //private void OnTriggerEnter(Collider other) //{ // if (other.TryGetComponent<FactoryItem>(out FactoryItem item)) // { // initialScale = item.transform.localScale; // itemRb = item.GetComponent<Rigidbody>(); // if (item.GetItemID() == machine.GetMachineID()) // { // Debug.Log("item id ile machine id'i doğru +1 puan ver"); // if (itemRb != null) // { // itemRb.useGravity = false; // } // item.transform.DOMoveY(transform.position.y + .2f, .3f).SetEase(Ease.OutBack); // item.transform.DOScale(initialScale * 1.3f, .3f).SetEase(Ease.OutBack) // .OnComplete(() => // { // if (itemRb != null) // { // itemRb.useGravity = true; // } // item.transform.DOScale(initialScale, .2f).SetEase(Ease.InOutQuad).OnComplete(() => // { // item.transform.DOScale(Vector3.zero, .5f).SetEase(Ease.InOutQuad).SetDelay(2f).OnComplete(() => // { // Destroy(item); // }); // }); // }); // } // //item id ile machine id ayni degil wronggg destroy ettt kız salla bisi yap iste // else // { // Debug.Log("WRONGGGGGGGG"); // if (itemRb != null) // { // itemRb.useGravity = false; // } // item.transform.DOMoveY(transform.position.y + .2f, .3f).SetEase(Ease.OutBack); // item.transform.DOScale(initialScale * 1.3f, .3f).SetEase(Ease.OutBack) // .OnComplete(() => // { // item.transform.DOShakePosition(0.3f, new Vector3(0.2f, 0, 0)).SetLoops(3, LoopType.Yoyo); // item.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).SetDelay(0.5f) // .OnComplete(() => // { // Destroy(item); // }); // }); // } // } //} #endregion }
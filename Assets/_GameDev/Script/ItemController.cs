using UnityEngine;
using DG.Tweening;

public class ItemController : MonoBehaviour
{
    [SerializeField] private MachineController machine;

    private Vector3 initialScale;
    private Rigidbody itemRb;

    private void Start()
    {

        machine = GetComponentInParent<MachineController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FactoryItem>(out FactoryItem item))
        {
            initialScale = item.transform.localScale;
            itemRb = item.GetComponent<Rigidbody>();

            if (item.GetItemID() == machine.GetMachineID())
            {
                Debug.Log("item id ile machine id'i doğru +1 puan ver");
                if (itemRb != null)
                {
                    itemRb.useGravity = false;
                }

                item.transform.DOMoveY(transform.position.y + .2f, .3f).SetEase(Ease.OutBack);
                item.transform.DOScale(initialScale * 1.3f, .3f).SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    if (itemRb != null)
                    {
                        itemRb.useGravity = true;
                    }
                    item.transform.DOScale(initialScale, .2f).SetEase(Ease.InOutQuad).OnComplete(() =>
                    {
                        item.transform.DOScale(Vector3.zero, .5f).SetEase(Ease.InOutQuad).SetDelay(2f).OnComplete(() =>
                        {
                            Destroy(item);
                        });
                    });
                });
            }
            //item id ile machine id ayni degil wronggg destroy ettt kız salla bisi yap iste
            else
            {
                Debug.Log("WRONGGGGGGGG");
                if (itemRb != null)
                {
                    itemRb.useGravity = false;
                }

                item.transform.DOMoveY(transform.position.y + .2f, .3f).SetEase(Ease.OutBack);
                item.transform.DOScale(initialScale * 1.3f, .3f).SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        item.transform.DOShakePosition(0.3f, new Vector3(0.2f, 0, 0)).SetLoops(3, LoopType.Yoyo);
                        item.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).SetDelay(0.5f)
                        .OnComplete(() =>
                        {
                            Destroy(item);
                        });
                    });
            }
        }
    }

}

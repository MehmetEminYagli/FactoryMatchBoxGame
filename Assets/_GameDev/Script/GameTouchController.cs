using UnityEngine;
using DG.Tweening;
public class GameTouchController : MonoBehaviour
{
    [SerializeField] private GameObject selectedObject;

    void Update()
    {
        TouchControl();
    }

    private void TouchControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                Debug.Log(clickedObject.name);

                if (clickedObject.GetComponent<FactoryItem>() != null && selectedObject == null)
                {
                    selectedObject = clickedObject;
                    selectedObject.transform.DOMove(new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y + 1, selectedObject.transform.position.z), .5f);
                    Rigidbody rb = selectedObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.useGravity = false;
                    }
                }
                else if (clickedObject.GetComponent<ConveyorScriptV2>() != null && selectedObject != null)
                {
                    Vector3 targetPosition = new Vector3(hit.point.x, hit.point.y, selectedObject.transform.position.z);

                    selectedObject.transform.DOMove(targetPosition, .6f).SetEase(Ease.InOutQuad);
                    
                    Rigidbody rb = selectedObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.useGravity = true;
                    }
                    selectedObject = null;
                }
            }
        }
    }
}

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
        bool isTouched = false;
        Vector3 touchPosition = Vector3.zero;


        if (Input.GetMouseButtonDown(0))
        {
            isTouched = true;
            touchPosition = Input.mousePosition;
        }

        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            isTouched = true;
            touchPosition = Input.GetTouch(0).position;
        }

        if (isTouched)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
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
                    Vector3 targetPosition = new Vector3(hit.point.x, selectedObject.transform.position.y, selectedObject.transform.position.z);

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

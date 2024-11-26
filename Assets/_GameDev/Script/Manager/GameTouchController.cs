using UnityEngine;
using DG.Tweening;

public class GameTouchController : MonoBehaviour
{
    [SerializeField] private GameObject selectedObject;
    private bool isDragging = false;
    private float touchAreaRadius = 1f;  // Dokunma alanı yarıçapı
    private Vector3 targetPosition;
    private float smoothTime = 0.1f; // Sürükleme işlemi için smooth geçiş süresi
    private Vector3 velocity = Vector3.zero; // SmoothDamp için hız vektörü
    private float initialXPosition; // Başlangıç X pozisyonunu saklamak için

    void Update()
    {
        TouchControl();
        SmoothMoveObject();
    }

    private void TouchControl()
    {
        Vector3 touchPosition = Vector3.zero;

        if (Input.GetMouseButton(0))
        {
            touchPosition = Input.mousePosition;
        }
        else if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
        }

        if (isDragging && (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            ReleaseObject();
        }

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray ray = Camera.main ? Camera.main.ScreenPointToRay(touchPosition) : default;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (clickedObject.GetComponent<FactoryItem>() != null)
                {
                    if (selectedObject != null && selectedObject != clickedObject)
                    {
                        ReleaseObject();
                    }
                    selectedObject = clickedObject;
                    StartDragging();
                }
            }
            else
            {
                // Eğer raycast ile tıklanmazsa, OverlapSphere ile tıklama alanını genişlet
                Collider[] hitColliders = Physics.OverlapSphere(Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, Camera.main.nearClipPlane)), touchAreaRadius);

                foreach (var collider in hitColliders)
                {
                    GameObject clickedObject = collider.gameObject;

                    if (clickedObject.GetComponent<FactoryItem>() != null)
                    {
                        if (selectedObject != null && selectedObject != clickedObject)
                        {
                            ReleaseObject();
                        }
                        selectedObject = clickedObject;
                        StartDragging();
                        break;
                    }
                }
            }
        }

        if (isDragging && selectedObject != null)
        {
            UpdateTargetPosition(touchPosition);
        }
    }

    private void StartDragging()
    {
        isDragging = true;
        Rigidbody rb = selectedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }
        selectedObject.transform.DOMove(new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y + 1, selectedObject.transform.position.z), .1f);
        targetPosition = selectedObject.transform.position;

        // Nesnenin başlangıç X pozisyonunu sakla
        initialXPosition = selectedObject.transform.position.x;
    }

    private void ReleaseObject()
    {
        isDragging = false;

        if (selectedObject != null)
        {
            Rigidbody rb = selectedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;
            }
            selectedObject = null;
        }
    }

    private void UpdateTargetPosition(Vector3 touchPosition)
    {
        Ray ray = Camera.main ? Camera.main.ScreenPointToRay(touchPosition) : default;
        Plane plane = new Plane(Vector3.up, selectedObject.transform.position);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);

            float newYPosition = Mathf.Clamp(targetPoint.y, 0, 3f);

            float newZPosition = Mathf.Clamp(targetPoint.z, selectedObject.transform.position.z - .2f, selectedObject.transform.position.z + .2f);
            targetPosition = new Vector3(targetPoint.x, newYPosition, newZPosition);
        }
    }

    private void SmoothMoveObject()
    {
        if (selectedObject != null)
        {
            selectedObject.transform.position = Vector3.SmoothDamp(selectedObject.transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
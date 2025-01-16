using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorScriptV2 : MonoBehaviour
{
    [SerializeField] private float speed =2.5f;
    [SerializeField] private float conveyorSpeed = 0.85f;
    private float currentSpeed, currentConveyorSpeed;
    [SerializeField] private List<GameObject> onBelt;
    [SerializeField] private Vector3 initialDirection;
    private Material paletMaterial;
    void Start()
    {
        GetPaletMaterial();
        currentSpeed = speed;
        currentConveyorSpeed = conveyorSpeed;
        ObjectDirection();
    }
    public List<GameObject> GetOnBelt()
    {
        return onBelt;
    }

    public Material GetPaletMaterial()
    {
        return paletMaterial = GetComponent<MeshRenderer>().material;
    }

    public Vector3 GetInitialDirection()
    {
        return initialDirection;
    }

    private void Update()
    {
        Vector2 currentOffset = paletMaterial.mainTextureOffset;
        currentOffset.y += conveyorSpeed * Time.deltaTime;
        paletMaterial.mainTextureOffset = currentOffset;
    }

    private void ObjectDirection()
    {
        Quaternion rotation = transform.rotation;
        initialDirection = rotation * Vector3.back;
    }

    void FixedUpdate()
    {
        onBelt.RemoveAll(item => item == null);
        foreach (GameObject obj in onBelt)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 targetPosition = rb.position + GetInitialDirection() * speed * Time.fixedDeltaTime;
                rb.MovePosition(targetPosition);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }


    public void StartMoveObject()
    {
        speed = currentSpeed;
        conveyorSpeed = currentConveyorSpeed;
    }

    public void StopMoveObject()
    {
        speed = 0f;
        conveyorSpeed = 0f;
    }

}

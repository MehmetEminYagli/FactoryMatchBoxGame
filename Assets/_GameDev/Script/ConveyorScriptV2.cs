using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorScriptV2 : MonoBehaviour
{
    [SerializeField] private float speed, conveyorSpeed, currentSpeed;
    [SerializeField] private List<GameObject> onBelt;
    [SerializeField] private Vector3 initialDirection;
    [SerializeField] private MachineController machine;

    void Start()
    {
        machine = GetComponentInParent<MachineController>();
        currentSpeed = speed;
        ObjectDirection();
    }

    private void ObjectDirection()
    {
        Quaternion rotation = transform.rotation;
        initialDirection = rotation * Vector3.back;
    }

    public Vector3 GetInitialDirection()
    {
        return initialDirection;
    }

    void FixedUpdate()
    {
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
    }

    public void StopMoveObject()
    {
        speed = 0f;
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ConveyorScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private MachineController machineController;

    Rigidbody rb;
    [SerializeField] private Vector3 initialDirection;
    private void Awake()
    {
        machineController = GetComponentInParent<MachineController>();
    }

    void Start()
    {
        currentSpeed = speed;
        rb = GetComponent<Rigidbody>();
        ObjectDirection();
    }

    private void ObjectDirection()
    {
        Quaternion rotation = transform.rotation;
        initialDirection = rotation * Vector3.forward;
    }

    public Vector3 GetInitialDirection()
    {
        return initialDirection;
    }

    private void FixedUpdate()
    {
        Vector3 pos = rb.position;
        rb.position += GetInitialDirection() * speed * Time.fixedDeltaTime;
        rb.MovePosition(pos);
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

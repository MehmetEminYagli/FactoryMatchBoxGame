using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorScriptV2 : MonoBehaviour
{
    [SerializeField]
    private float speed, conveyorSpeed;
    [SerializeField] private List<GameObject> onBelt;
    [SerializeField] private Vector3 initialDirection;

    void Start()
    {
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

    void FixedUpdate()
    {
        // OnBelt listesinde bulunan her nesne için konumunu kaydırarak hareket ettir
        foreach (GameObject obj in onBelt)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 newPosition = rb.position + GetInitialDirection() * -speed * Time.fixedDeltaTime;
                rb.MovePosition(newPosition);
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
}

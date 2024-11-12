using UnityEngine;

public class FireMachinePoint : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;


    private void Start()
    {
        if(pointA ==null || pointB == null)
        {
            Debug.Log("point noktaları atanmadı");
        }
    }


    public Transform GetPointA()
    {
        return pointA;
    }
    public Transform GetPointB()
    {
        return pointB;
    }
}

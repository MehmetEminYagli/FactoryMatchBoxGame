using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FiremanController : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform lookAtPointA;
    public Transform lookAtPointB;
    public Animator animator;
    public float waitTime = 1f;

    [SerializeField] private NavMeshAgent navMeshAgent;
    private bool isMovingToB = false;




    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void BtnFireMan()
    {
        MoveToFireMachine();
        Debug.Log("Moving to extinguish fire");
    }

    void Update()
    {
        if (navMeshAgent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("IsWalking", true);
            Vector3 directionToTarget = navMeshAgent.destination - transform.position;
            Quaternion rotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        if (!isMovingToB && Vector3.Distance(transform.position, pointA.position) < 0.5f)
        {
            StartCoroutine(WaitAndReturnToSafeArea());
            LookAtPoint(lookAtPointA);
        }
        else if (isMovingToB && Vector3.Distance(transform.position, pointB.position) < 0.5f)
        {
            StartCoroutine(WaitAtSafeArea());
            LookAtPoint(lookAtPointB);
        }
    }

    void LookAtPoint(Transform lookAtPoint)
    {
        if (lookAtPoint != null)
        {
            navMeshAgent.isStopped = true;
            Vector3 directionToLook = lookAtPoint.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(directionToLook);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);
        }
    }

    void MoveToFireMachine()
    {
        Debug.Log("Moving towards fire machine");
        navMeshAgent.SetDestination(pointA.position);
        animator.SetBool("IsWalking", true);
        navMeshAgent.isStopped = false;
    }

    void MoveToSafeArea()
    {
        navMeshAgent.SetDestination(pointB.position);
        animator.SetBool("IsWalking", true);
        navMeshAgent.isStopped = false;
        isMovingToB = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FireMachinePoint>(out FireMachinePoint firePoint))
        {
            animator.SetBool("IsWalking", false);
            StartCoroutine(WaitAndReturnToSafeArea());
        }
    }

    IEnumerator WaitAndReturnToSafeArea()
    {
        yield return new WaitForSeconds(waitTime);
        MoveToSafeArea();
    }

    IEnumerator WaitAtSafeArea()
    {
        yield return new WaitForSeconds(waitTime);
        isMovingToB = false; //bu kontrolü yazmayı unuttugumu fark etmem bir günüme sebep oldu ve cozumu 2dk da buldum bazen ara vermek gerekiyormus.
    }
}



using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class FiremanController : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform lookAtPointA;
    [SerializeField] private Transform lookAtPointB;
    [SerializeField] private Animator animator;


    private NavMeshAgent navMeshAgent;
    private bool isMovingToB = false;

    //efects
    [SerializeField] private GameObject FireExtinguisherEffects;


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
            //StartCoroutine(WaitAndReturnToSafeArea());
            LookAtPoint(lookAtPointA);
            SetActiveFireExtinguisher(true);

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
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 6f);

        }
    }
    private void SetActiveFireExtinguisher(bool setactive)
    {
        if (setactive)
        {
            FireExtinguisherEffects.SetActive(true); // setactive true olduğunda hemen aktif hale getiriyoruz
            FireExtinguisherEffects.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
            {
                FireExtinguisherEffects.SetActive(setactive);
            });
        }
        else
        {
            FireExtinguisherEffects.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                FireExtinguisherEffects.SetActive(false);
            });
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
        SetActiveFireExtinguisher(false);
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
            //StartCoroutine(WaitAndReturnToSafeArea());
        }
    }

    public void WaitAndMoveSafeArea()
    {
        StartCoroutine(WaitAndReturnToSafeArea());
    }

    IEnumerator WaitAndReturnToSafeArea()
    {
        yield return new WaitForSeconds(.1f);
        MoveToSafeArea();
    }

    IEnumerator WaitAtSafeArea()
    {
        yield return new WaitForSeconds(.6f);
        isMovingToB = false; //bu kontrolü yazmayı unuttugumu fark etmem bir günüme sebep oldu ve cozumu 2dk da buldum bazen ara vermek gerekiyormus.
    }
}



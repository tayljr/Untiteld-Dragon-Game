using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AIController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private NavMeshAgent agent;
    [SerializeField] private GameObject target;
    [SerializeField]
    private Transform position;

    public Transform[] patrolPoints;
    public int currentPoint;
    public bool patroling;
    public float fovdelay;
    public float agro;
    [SerializeField,Range(0f,10f)]
    private float agromax;

    public float minDistance;

    [Range(40f, 120f)]
    public float FOV;
    public LayerMask RaycastMask;

    void Start()
    {
        //grabs that boi at the start
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FOVCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

        //patrol bool
        if (patroling)
            Patrol();
        if (agro > 0)
        {
            agent.SetDestination(target.transform.position);
            agro -= Time.deltaTime;
        }
        else
        {
            agent.ResetPath();
        }
    }

    private void Patrol()
    {
        //if agent has no path then set destination
        if (agent.remainingDistance <= minDistance)
        {
            //checks if its the last

            currentPoint++;
            if (currentPoint == patrolPoints.Length)
            {
                currentPoint = 0;
            }
            agent.SetDestination(patrolPoints[currentPoint].position);
        }

    }

    public IEnumerator FOVCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fovdelay);
            //check if player is still in view
            FovCheck();
        }

    }

    private void FovCheck()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        RaycastHit hit;
        Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity, RaycastMask);
        if( hit.collider != null )
        {
            Debug.DrawRay(transform.position, hit.point);
        }
        if (Mathf.Abs(Vector3.Angle(transform.forward, dir)) < FOV && hit.transform.gameObject == target)
        {
           agro = agromax;
        }
    }

    public void gotopos()
    {
        agent.SetDestination(target.transform.position);
    }
    private void OnDrawGizmos()
    {
        //null check for patrol points so no 100000 errors
        if (patrolPoints != null)
        {
            foreach (var point in patrolPoints)
            {
                //null check for poins for also no 10000 errors
                if (point != null)
                {
                    
                    Gizmos.DrawIcon(point.position, "point",true);
                }
            }
        }
    }
}


using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


public enum AIType
{
    Flying,
    Grounded
}
[RequireComponent(typeof(NavMeshAgent))]
public class AIControllerEnemy : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private NavMeshAgent agent;
    private CharacterController characterController;
    private Rigidbody rb;
    private EnemyController enemyController;
    public EnemyCombatBase combat;
    [SerializeField] private GameObject target;

    [SerializeField] private Transform foveye;

    public AIType Type;
    public Transform[] patrolPoints;
    public int currentPoint;
    public bool isAttacking = false;
    public bool patroling;
    public bool roaming;
    [SerializeField] private Vector3 roamPosition;
    public bool lineOfSight = false;
    public float fovdelay;

    public bool notGrounded;
    private float verticalvel;
    public float agro;
    [SerializeField,Range(0f,10f)]
    private float agromax = 4f;
    public float Gravity;
    public float minDistance;

    [Range(40f, 120f)]
    public float FOV = 65f;
    [Range (30f, 1000f)]
    public float FOVRange = 10.0f;
    public LayerMask RaycastMask;

    [SerializeField] private Vector3 agentVelocity;
    [SerializeField] private Vector3 CharControlVelocity;

    void Awake()
    {
        //grabs that boi at the start
        agent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        enemyController = GetComponent<EnemyController>();
        rb = GetComponentInChildren<Rigidbody>();
        combat = GetComponent<EnemyCombatBase>();
        agent.updatePosition = false;
        agent.updateRotation = false;

        agent.autoTraverseOffMeshLink = true;
        StartCoroutine(FOVCoroutine());
        StartCoroutine(AttackCoroutine());
    }

    // Update is called once per frame
    private void Update()
    {
        agent.speed = enemyController.speed;
        
    }
    void FixedUpdate()
    {
        agentVelocity = agent.velocity;
        CharControlVelocity = characterController.velocity;

        notGrounded = !characterController.isGrounded;
        Vector3 desVelocity = agent.desiredVelocity;

        Vector3 Lookpos = agent.steeringTarget - transform.position;
        Lookpos.y = 0;
        if (Lookpos!= Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(Lookpos);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * agent.angularSpeed);

        }
        if (notGrounded)
        {
            verticalvel += Gravity * Time.deltaTime;
        }
        else
        {
            verticalvel = -1f;
        }
        //move for charcontroller according to NavMeshAgent

        Vector3 move = (desVelocity.magnitude * transform.forward)  * agent.speed;
        move.y = verticalvel;
        
        

        characterController.Move(move * Time.deltaTime);
        agent.nextPosition = transform.position;
        

        
  

        
        //gravity for the CharController!


        //patrol bool
        if (patroling)
            Patrol();
        if (roaming)
            Roam();
        if (agro > 0)
        {
            agent.SetDestination(target.transform.position);
            agro -= Time.deltaTime;
        }
        else if (!patroling && !roaming)
        {
            agent.ResetPath();
            agro = 0;
            lineOfSight = false;
            roaming = true;
        }
    }
    private void Roam()
    {
       if (roamPosition != Vector3.zero)
       {
           if (agent.remainingDistance <= minDistance)
            {
                
                if (RandomPointOnNavMesh(agent.transform.position, FOVRange, out roamPosition))
                {
                    agent.SetDestination(roamPosition);
                }
            }
       }
       else
       {
           RandomPointOnNavMesh(agent.transform.position, FOVRange, out roamPosition);
           agent.SetDestination(roamPosition);
       }
 

    
    }
    private void Patrol()
    {
        //check if patrol has points
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            //if the agent is close to the marker
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



    }
    public bool RandomPointOnNavMesh(Vector3 center,float range,out Vector3 position)
    {
        for (int i = 0; i < 30; i++)
        { 
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint,out hit, range, NavMesh.AllAreas))
            {
                position = hit.position;
                return true;
            }

        }
        position = Vector3.zero;
        return false;
    }
    public IEnumerator FOVCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fovdelay);
            //check if player is still in view
           LineOfSight();
        }

    }
    public IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(combat.attackDelay);
            //check if the enemy can attack the player
            if (AttackDistanceCheck())
            {
                isAttacking = true;
                enemyController.OnAttackEvent(combat.TypeOfAttack);
            }
            
        }
    }
    public void LineOfSight()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;

        float angleToTarget = Vector3.Angle(transform.forward, dir);
        if (angleToTarget > FOV / 2)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, FOVRange, RaycastMask))
            {
                Debug.DrawRay(transform.position, dir * hit.distance, Color.red);
                Debug.Log($"Did Hit {hit.collider.name}");
                if (hit.transform.gameObject == target)
                {
                    agro = agromax;
                    patroling = false;
                    roaming = false;
                    lineOfSight = true;
                }
                else lineOfSight = false;
            }
        else
        {
                lineOfSight = false;
                return; //outside of view, lets get out of this function!
        }

            
        
        }
    }
    public bool AttackDistanceCheck()
    {
        if (Vector3.Distance(gameObject.transform.position,target.transform.position) <= combat.attackRange && lineOfSight)
        {
            return true;
        }
        else 
            return false;
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
        //does not work in build
        //Handles.DrawWireArc(foveye.transform.position, Vector3.up,Vector3.forward,360,FOVRange);
    }
    private void OnDisable()
    {
        agent.enabled = false;
        rb.isKinematic = false;
        rb.linearVelocity = characterController.velocity;
    }
    private void OnEnable()
    {
        agent.enabled = true;
    }
}

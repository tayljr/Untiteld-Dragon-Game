using System;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    Melee,
    Charge,
    Flying_Melee,
    Ranged,
    Flying_Ranged
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] private AIControllerEnemy AI;
    [SerializeField] private HealthBase HealthBase;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    public EnemyType enemyType;
    
   
    public float speed = 1f;
    public float damage = 2f;

    public bool isDead = false;



    public string enemyName;
    public string description;
    
    void Awake()
    {
        AI = GetComponent<AIControllerEnemy>();
        agent = GetComponent<NavMeshAgent>();
        HealthBase = GetComponent<HealthBase>();
        animator = GetComponentInChildren<Animator>();

    }
    private void Start()
    {
        agent.speed *= speed;

    }

    // Update is called once per frame
    void Update()
    {
        isDead = HealthBase.isDead;

        if (isDead)
        {
            AI.enabled = false;
        }

    }
    private void OnEnable() => AIControllerEnemy.OnAttackEvent += AIControllerEnemy_OnAttackEvent;
    private void OnDisable() => AIControllerEnemy.OnAttackEvent -= AIControllerEnemy_OnAttackEvent;

    private void AIControllerEnemy_OnAttackEvent(ListOfAttacks attack)
    {
        animator.SetTrigger(attack.ToString());
    }
}

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

public class EnemyController : MonoBehaviour, IHit
{
    public AIControllerEnemy AI;
    public HealthBase HealthBase;
    public NavMeshAgent agent;
    public Animator animator;
    public AudioSource source;
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
        source = GetComponent<AudioSource>();

    }
    private void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        isDead = HealthBase.isDead;
        agent.speed = speed;


        if (isDead)
        {
            AI.enabled = false;
        }

    }
    public void Hit(object args)
    {
        source.Play();
    }
}

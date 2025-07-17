using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private AIControllerEnemy AI;
    [SerializeField] private HealthBase HealthBase;
    [SerializeField] private NavMeshAgent agent;
    public float speed = 1f;
    public float damage = 2f;

    public bool isDead = false;
    public bool isAttacking = false;



    public string enemyName;
    public string description;
    
    void Awake()
    {
        AI = GetComponent<AIControllerEnemy>();
        agent = GetComponent<NavMeshAgent>();
        HealthBase = GetComponent<HealthBase>();
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
}

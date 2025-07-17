using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private AIControllerEnemy AI;
    public float health = 10f;
    public float speed = 1f;
    public float damage = 2f;

    public bool isDead = false;
    public bool isAttacking = false;



    public string enemyName;
    public string description;
    
    void Awake()
    {
        AI = GetComponent<AIControllerEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

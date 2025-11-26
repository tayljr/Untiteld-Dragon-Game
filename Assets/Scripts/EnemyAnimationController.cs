using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
   
{
    private Animator animator;
    private AIControllerEnemy controllerEnemy;
    private AudioSource source;
    private Vector3 enemyVel;
    private Vector3 enemyLookPos;

     

    bool isAttacking;
    bool isDead;
    bool idle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        controllerEnemy = GetComponentInParent<AIControllerEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimatorValues();
    }
    public void PlaySound()
    {
        source.Play();
    }
    void UpdateAnimatorValues()
    {
        enemyVel = controllerEnemy.CharControlVelocity.normalized;
        enemyLookPos = controllerEnemy.PlayerTarget.transform.position;
        isAttacking = controllerEnemy.lineOfSight;

        if (enemyVel == Vector3.zero)
        {
            idle = true;
        }
        else
        {
            idle = false;
        }

        animator.SetBool("IsIdle", idle);
        animator.SetBool("IsAttacking", isAttacking);
        animator.SetFloat("Velocity.x", enemyVel.x);
        animator.SetFloat("Velocity.y", enemyVel.z);







    }
    private void OnAnimatorIK(int layerIndex)
    {
        float lookatweight = isAttacking ? 1f : 0f;
        animator.SetLookAtWeight(lookatweight, lookatweight);
        Vector3 headTransform = animator.GetBoneTransform(HumanBodyBones.Head).position;
        animator.SetLookAtPosition(enemyLookPos);
    }
}

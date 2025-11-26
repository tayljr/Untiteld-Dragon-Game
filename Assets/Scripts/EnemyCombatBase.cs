using UnityEngine;
using System.Collections;
public enum ListOfAttacks
{
    None,
    Punch,
    Charge,
    Shoot_Projectile,
    Shoot_Lazer

}
public enum ListOfEnemies 
{ 
    NPC,
    Knight,
    Golem,
    Skeleton,
    Necromancer
}

public class EnemyCombatBase : MonoBehaviour
{
    public ListOfEnemies IAmA;

    public ListOfAttacks TypeOfAttack;
    public BoxCollider hurtBox;
    public float attackRange;
    public float attackDelay;
    private DamageBase damageBase;
    private AttackBase attackBase;
    private Animator animator;


    public bool isAttacking;
    public bool isCharging;
    public bool roaming;


    private AIControllerEnemy AIControllerEnemy;
    

    private void Start()
    {
        AIControllerEnemy = GetComponent<AIControllerEnemy>();
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(AttackCoroutine());

    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (transform.forward * attackRange) + transform.position );

    }
    public bool AttackDistanceCheck(bool hasLineOfSigh)
    {
        if (TypeOfAttack == ListOfAttacks.None) { return false; }
        if (TypeOfAttack == ListOfAttacks.Punch)
        {
            //your punch/sword 
            if (Vector3.Distance(gameObject.transform.position, AIControllerEnemy.PlayerTarget.transform.position) <= attackRange)
            {
                return true;
            }
            else
                return false;
        }
        if (TypeOfAttack == ListOfAttacks.Charge)
        {
            //this would be a long distance melee attack where they charge at the player like a cavilary or somethin
            return false;
        }
        if (TypeOfAttack == ListOfAttacks.Shoot_Projectile)
        {
            //I used to be a adventure like you...until I took a arrow to the knee
            return (hasLineOfSigh ? true : false);

        }
        if (TypeOfAttack == ListOfAttacks.Shoot_Lazer)
        {
            //A lazer BWAAAAAAA
            if (hasLineOfSigh)
            {
                return true;
            }
            else if (hasLineOfSigh && isCharging == true)
            {
                return false;
            }

        }
        Debug.LogWarning("How did i get here?!?!?!?");
        return false;

    }

    public IEnumerator AttackCoroutine()
    {
        while (true)
        {
            LineOfSight();
            yield return new WaitForSeconds(attackDelay);
            //check if the enemy can attack the player
            if (AttackDistanceCheck(AIControllerEnemy.lineOfSight))
            {
                OnAttackEvent(TypeOfAttack);
            }

        }
    }
    public void OnAttackEvent(ListOfAttacks attack)
    {
        animator.SetTrigger(attack.ToString());
    }

    public void LineOfSight()
    {
        Vector3 dir = (AIControllerEnemy.PlayerTarget.transform.position - transform.position).normalized;

        float angleToTarget = Vector3.Angle(transform.forward, dir);
        if (angleToTarget > AIControllerEnemy.FOV / 2)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, AIControllerEnemy.FOVRange, AIControllerEnemy.RaycastMask))
            {
                Debug.DrawRay(transform.position, dir * hit.distance, Color.red);
                Debug.LogWarning($"Did Hit {hit.collider.name}");
                if (hit.transform.gameObject == AIControllerEnemy.PlayerTarget)
                {
                    AIControllerEnemy.agro = AIControllerEnemy.agromax;
                    AIControllerEnemy.lineOfSight = true;
                }
                else AIControllerEnemy.lineOfSight = false;
            }
            else
            {
                AIControllerEnemy.lineOfSight = false;
                return; //outside of view, lets get out of this function!
            }



        }
    }
}

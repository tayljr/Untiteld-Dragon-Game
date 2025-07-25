using UnityEngine;

public enum ListOfAttacks
{
    None,
    Punch,
    Charge,
    Shoot_Projectile,
    Shoot_Lazer

}


public class EnemyCombatBase : MonoBehaviour
{
    public ListOfAttacks TypeOfAttack;
    public BoxCollider hurtBox;
    public float attackRange;
    public float attackDelay;
    private DamageBase damageBase;
    private AttackBase attackBase;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (transform.forward * attackRange) + transform.position );

    }
}

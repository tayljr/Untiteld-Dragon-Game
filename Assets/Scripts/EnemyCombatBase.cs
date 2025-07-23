using UnityEngine;

public enum ListOfAttacks
{
    None,
    Windup_Punch,
    Punch,
    Windup_Charge,
    Charge,
    Windup_Projectile,
    Shoot_Projectile,
    Windup_Lazer,
    Shoot_Lazer

}


public class EnemyCombatBase : MonoBehaviour
{
    public EnemyType type;
    private DamageBase damageBase;
    private AttackBase attackBase;

    public float attackrange;
    public float attackdelay;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (type == EnemyType.Melee)
        {

        }
        if (type == EnemyType.Charge)
        {

        }
        if (type == EnemyType.Flying_Melee)
        {

        }
        if (type == EnemyType.Ranged)
        {

        }
        if (type == EnemyType.Flying_Ranged)
        {

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (transform.forward - transform.position) * attackrange);

    }
}

using System;
using UnityEngine;
using System.Collections;

public enum AttackType
{
    Duration,
    Continuous,
}

public class AttackBase : MonoBehaviour, IPauseable
{
    public Collider hurtBox;
    public AttackType attackType = AttackType.Duration;

    public float attackDelay = 0.67f;
    public float attackDuration = 0.25f;

    private bool isAttacking = false;
    
    public void StartAttack()
    {
        //hurtBox.enabled = true;
        //print("ATTAAAAACK!!");
        
        isAttacking = true;
        
        StartCoroutine(BeginAttack());
    }

    IEnumerator BeginAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        if(isAttacking)
        {
            hurtBox.enabled = true;
        }
        if (attackType == AttackType.Duration)
        {
            yield return new WaitForSeconds(attackDuration);
            isAttacking = false;
            hurtBox.enabled = false;
        }
    }

    public void StopAttack()
    {
        if (attackType == AttackType.Continuous)
        {
            isAttacking = false;
            hurtBox.enabled = false;
        }
        print("Done");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hurtBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPause()
    {
        if (attackType == AttackType.Continuous)
        {
            hurtBox.enabled = false;
        }
    }

    public void OnResume()
    {
        if (attackType == AttackType.Continuous)
        {
            hurtBox.enabled = false;
        }
    }
}

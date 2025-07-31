using System;
using UnityEngine;
using System.Collections;

public enum AttackType
{
    Duration,
    Continuous,
}

public class AttackBase : MonoBehaviour
{
    public Collider hurtBox;
    public AttackType attackType = AttackType.Duration;

    public float attackDuration = 0.25f;

    public void StartAttack()
    {
        hurtBox.enabled = true;
        //print("ATTAAAAACK!!");

        if (attackType == AttackType.Duration)
        {
            StartCoroutine(BeginAttack());
        }
    }

    IEnumerator BeginAttack()
    {
        yield return new WaitForSeconds(attackDuration);
        hurtBox.enabled = false;
    }

    public void StopAttack()
    {
        if (attackType == AttackType.Continuous)
        {
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
}

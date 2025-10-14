using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class HealthBase : MonoBehaviour
{
    public float maxHealth = 10f;
    public float health = 10f;
    public float healthPercent = 100f;

    public float iFrameTime = 0.25f;

    public bool invincible = false;

    public bool isDead = false;

    //LootTable
    [Header("Loot")]
    public List<LootItem> LootTable = new List<LootItem>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth * (healthPercent / 100);
        invincible = false;
        HealthCheck();
    }

    public void Damage(float amount)
    {
        if (!invincible)
        {
            health -= amount;
            StartCoroutine(IFrames());
            HealthCheck();
        }
    }

    public void DamagePercent(float percentage)
    {
        if (!invincible)
        {
            health -= maxHealth * (percentage / 100);
            StartCoroutine(IFrames());
            HealthCheck();
        }
    }

    IEnumerator IFrames()
    {
        invincible = true;
        yield return new WaitForSeconds(iFrameTime);
        invincible = false;
    }

    public void Heal(float amount)
    {
        health += amount;

        HealthCheck();
    }

    public void HealPercent(float percentage)
    {
        health += maxHealth * (percentage / 100);

        HealthCheck();
    }

    private void HealthCheck()
    {
        if (health <= 0)
        {
            health = 0;
            isDead = true;
            Debug.Log("Dead");
            Register();
            Destroy(gameObject);


            //Spawn Item
            foreach (LootItem lootItem in LootTable)
            {
                if (Random.Range(0f, 100f) <= lootItem.dropChance)
                {
                    InstantiateLoot(lootItem.itemPrefab);
                }
            }

        }
        else if (health > maxHealth)
        {
            health = maxHealth;
            isDead = false;
        }
        else
        {
            isDead = false;
        }
        healthPercent = (health / maxHealth) * 100;
    }


    // Update is called once per frame
    void Update()
    {

    }
    void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            GameObject droppedLoot = Instantiate(loot, transform.position, Quaternion.identity);

        }
    }
    public void Register()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f); // Small sphere to detect triggers at enemy's position
        foreach (Collider collider in colliders)
        {
            CombatZone tracker = collider.GetComponent<CombatZone>();
            if (tracker != null)
            {
                tracker.RegisterKill();
                break;
            }
        }
    }
}

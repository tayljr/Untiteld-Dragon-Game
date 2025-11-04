using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class HealthBase : MonoBehaviour
{
    public delegate void DeathEvent(string tag);
    public static event DeathEvent OnDeath;

    public delegate void DamageEvent(float damage, string tag);
    public static event DamageEvent OnDamage;

    public delegate void HealEvent(float heal, string tag);
    public static event HealEvent OnHeal;

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
            OnDamage?.Invoke(amount, gameObject.tag);
            object[] args = new object[2];
            args[0] = amount;
            args[1] = gameObject.tag;

            gameObject.SendMessage("Hit", args, SendMessageOptions.DontRequireReceiver);
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
        OnHeal?.Invoke(amount, gameObject.tag);
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
            OnDeath?.Invoke(gameObject.tag);
            Debug.Log("Dead");
            if(gameObject.tag != "Player")
            {
                Destroy(gameObject);
            }

            
            //Spawn Item
            foreach(LootItem lootItem in LootTable)
            {
                if(Random.Range(0f, 100f) <= lootItem.dropChance)
                {
                    InstantiateLoot(lootItem.itemPrefab);
                }
            }
            
        }
        else if(health > maxHealth)
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
}

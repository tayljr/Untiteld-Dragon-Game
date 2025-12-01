using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerNecro : MonoBehaviour
{

    public SpawnEnemy EnemyInstance;
    public GameObject Target;
    public bool Spawning;
    private List<GameObject> aliveEnemies = new List<GameObject>();

    //todo this is just temp for the demo

    private void Start()
    {
        Target = GameObject.Find("Player");
    }
    public void SpawnSkeleton()
    {
        GameObject enemy = Instantiate(EnemyInstance.enemy, gameObject.transform.position, Quaternion.identity);
        aliveEnemies.Add(enemy);
        enemy.GetComponent<AIControllerEnemy>().PlayerTarget = Target;
    }
    // Update is called once per frame
    private void OnEnable()
    {
        HealthBase.OnDeath += HealthBase_OnDeath;
    }
    public void StartAttack()
    {
        Spawning = true;
        SpawnSkeleton();
    }
    public void StopAttack()
    {
        Spawning = false;
    }
    private void OnDisable()
    {
        HealthBase.OnDeath -= HealthBase_OnDeath;
    }

    private void HealthBase_OnDeath(string tag, GameObject obj)
    {
        if (aliveEnemies.Contains(obj))
        {
            // waves[0].enemiesLeft--;
            aliveEnemies.Remove(obj);
        }
    }
}


[System.Serializable]
public class SpawnEnemy
{
    public GameObject enemy;

}

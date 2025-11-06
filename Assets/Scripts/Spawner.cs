using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float countdown;

    [SerializeField] private GameObject SpawnPoint;

    public Wave[] waves;

    public int currentWaveIndex = 0;
    private int currentWave = 0;

    private bool readyToCountDown;
    private bool playerNear = false;
    
    private List<GameObject> aliveEnemies = new List<GameObject>();
    
    //todo this is just temp for the demo
    public GameObject wall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void Start()
    {
        playerNear = false;
    }

    // Update is called once per frame
    void Update()
    {
        //todo fix current wave index after level restart
        if (currentWaveIndex != currentWave)
        {
            currentWave = currentWaveIndex;
        }
        if (playerNear)
        {
            if (currentWaveIndex >= waves.Length)
            {
                //Debug.Log("waves finished");
                if (wall != null)
                {
                    wall.SetActive(false);
                }

                return;
            }

            if (readyToCountDown == true)
            {
                countdown -= Time.deltaTime;

            }

            if (countdown <= 0)
            {
                readyToCountDown = false;
                //countdown = waves[0].timeToNextWave;
                countdown = waves[currentWaveIndex].timeToNextWave;

                StartCoroutine(Spawnwave());

            }

            // if (waves[0].enemiesLeft == 0)
            if (waves[currentWaveIndex].enemiesLeft == 0)
            {
                readyToCountDown = true;
                currentWaveIndex++;
                // currentWaveIndex = 1;
            }
        }
    }
    private void OnEnable()
    {
        currentWaveIndex = 0;
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length;
        }
        
        HealthBase.OnDeath += HealthBase_OnDeath;
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
            waves[currentWaveIndex].enemiesLeft--;
        }
    }

    private IEnumerator Spawnwave()
    {
        if (currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[0].enemies.Length; i++)
            {
                GameObject enemy = Instantiate(waves[0].enemies[i], SpawnPoint.transform.position, Quaternion.identity);
                aliveEnemies.Add(enemy);
                //enemy.transform.position = SpawnPoint.transform.position;
                //enemy.transform.SetParent(SpawnPoint.transform);
                yield return new WaitForSeconds(waves[0].timeToNextEnemy);
            }
        }
       
    }

    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemies;
        public float timeToNextWave;
        public float timeToNextEnemy;

        public int enemiesLeft;

    }

}

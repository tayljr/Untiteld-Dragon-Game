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
        //todo fix current
        if (currentWaveIndex != currentWave)
        {
            currentWave = currentWaveIndex;
        }
        if (playerNear)
        {
            if (currentWaveIndex >= waves.Length)
            {
                Debug.Log("waves finished");
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
                countdown = waves[currentWaveIndex].timeToNextWave;
                StartCoroutine(Spawnwave());

            }

            if (waves[currentWaveIndex].enemiesLeft == 0)
            {
                readyToCountDown = true;
                currentWaveIndex++;
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

    private void HealthBase_OnDeath(string tag)
    {
        if (tag == "Enemy")
        {
            waves[currentWaveIndex].enemiesLeft--;
        }
    }

    private IEnumerator Spawnwave()
    {
        if (currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
            {
                GameObject enemy = Instantiate(waves[currentWaveIndex].enemies[i], SpawnPoint.transform.position, Quaternion.identity);
                //enemy.transform.position = SpawnPoint.transform.position;
                //enemy.transform.SetParent(SpawnPoint.transform);
                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
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

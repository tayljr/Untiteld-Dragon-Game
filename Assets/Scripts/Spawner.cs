using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float countdown;

    [SerializeField] private GameObject SpawnPoint;

    public Wave[] waves;

    public int currentWaveIndex = 0;

    private bool readyToCountDown;

    private void Start()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("waves finished");
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

        if (waves[currentWaveIndex].enemiesLeft ==0)
        {
            readyToCountDown = true;
            currentWaveIndex++;
        }
    }
    private void OnEnable()
    {
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
                GameObject enemy = Instantiate(waves[currentWaveIndex].enemies[i], SpawnPoint.transform);

                enemy.transform.SetParent(SpawnPoint.transform);

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

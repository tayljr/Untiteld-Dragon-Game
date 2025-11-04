using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Enemy : MonoBehaviour
{
    private Spawner spawner;
    private HealthBase healthBase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawner = GetComponentInParent<Spawner>();

        healthBase = GetComponent<HealthBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBase.health <=0)
        {
            //spawner.waves[spawner.currentWaveIndex].enemiesLeft--;
        }
    }
}

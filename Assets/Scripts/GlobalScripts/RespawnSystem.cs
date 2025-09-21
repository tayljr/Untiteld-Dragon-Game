using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RespawnPoint 
{
    public Transform transform;
    public BoxCollider boxTrigger;
    public Color gizmoColor = Color.green;
    public string spawnName;
}

public class RespawnSystem : MonoBehaviour
{

    public List<RespawnPoint> respawnPoints = new List<RespawnPoint>();
    public GameObject playerRef;

    public bool SpawnAtStart = true;

    [SerializeField] private int currentRespawnIndex = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FindPlayer());
    }
    public IEnumerator FindPlayer()
    {
        while (playerRef == null)
        {
            playerRef = GameObject.FindGameObjectWithTag("Player");
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void FixedUpdate()
    {
        StartCoroutine(TrySpawnPlayer());

    }
    public IEnumerator TrySpawnPlayer()
    {
        while (SpawnAtStart == true)
        {
            if (playerRef != null && respawnPoints.Count > 0)
            {
                RespawnPlayer();
                SpawnAtStart = false;
            }
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    // Update is called once per frame
    public void RespawnPlayer()
    {
        if (playerRef != null && SpawnAtStart && respawnPoints.Count > 0)
        {
            //playerRef.Respawn function here = respawnPoints[0].transform.position;
            SpawnAtStart = false;
        }

        if (playerRef != null && respawnPoints.Count > 0)
        {
            //playerRef.Respawn function here  = respawnPoints[currentRespawnIndex].transform.position;
        }

    }
    private void OnDrawGizmos()
    {
        foreach (RespawnPoint rp in respawnPoints)
        {
            if (rp.transform != null)
            {


                Gizmos.color = rp.gizmoColor;
                Gizmos.matrix = rp.transform.localToWorldMatrix;
                Gizmos.DrawWireCube(Vector3.zero, rp.boxTrigger.size);
            }
        }
    }
}

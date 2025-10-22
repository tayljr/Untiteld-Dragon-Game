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
    public static RespawnSystem instance;
    public List<RespawnPoint> respawnPoints = new List<RespawnPoint>();
    public GameObject playerRef;
    [SerializeField]
    //private GameObject PlayerOBJInstance;
    public bool PlayerIsAlive = true;
    public int currentRespawnIndex = 0;

    private void Awake()
    {
        if (this != instance && instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FindPlayer());
        // RespawnPlayer(0);
        // StartCoroutine(TrySpawnPlayer());
    }
    public void OnEnable()
    {
        HealthBase.OnDeath += HealthBase_OnDeath;
        foreach (RespawnPoint respawnPoint in respawnPoints)
        {
            respawnPoint.transform.gameObject.GetComponent<ColliderEvents>().OnTriggerEnterEvent += OnRespawnTriggerEvent;
        }
    }

    private void OnRespawnTriggerEvent(GameObject self, Collider other)
    {
        if (other.gameObject == playerRef)
        {
            RespawnPoint newRespawnPoint = respawnPoints.Find(RespawnPoint => RespawnPoint.transform == self.transform);
            currentRespawnIndex = respawnPoints.IndexOf(newRespawnPoint);
        }
    }

    public void OnDisable() => HealthBase.OnDeath -= HealthBase_OnDeath;

    private void HealthBase_OnDeath(string tag)
    {
        //Debug.LogWarning("RespawnSystem detected death of: " + tag);
        //playerRef = Instantiate(PlayerOBJInstance);
        if(tag == "Player")
        {
            PlayerIsAlive = false;
            StartCoroutine(TrySpawnPlayer());
        }
    }

    public IEnumerator FindPlayer()
    {
        while (playerRef == null)
        {
            playerRef = GameObject.FindGameObjectWithTag("Player");
            yield return new WaitForSeconds(0.5f);
           
        }
        StopCoroutine(FindPlayer());
    }
    public IEnumerator TrySpawnPlayer()
    {
        while (PlayerIsAlive == false)
        {
            if (playerRef != null && respawnPoints.Count > 0)
            {
                RespawnPlayer(currentRespawnIndex);
            }
            yield return new WaitForSeconds(0.5f);
        }
        //while (PlayerIsAlive == true)
        //{
        //    if (playerRef != null && respawnPoints.Count > 0)
        //    {
        //        RespawnPlayer(currentRespawnIndex);
        //    }
        //    yield return new WaitForSeconds(0.5f);
        //}

    }
    // Update is called once per frame
    public void RespawnPlayer(int spawnIndex)
    {
        playerRef.GetComponent<CharacterMovement>().Teleport(respawnPoints[spawnIndex].transform.position);
        PlayerIsAlive = true;
        playerRef.GetComponent<HealthBase>().HealPercent(100f);
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

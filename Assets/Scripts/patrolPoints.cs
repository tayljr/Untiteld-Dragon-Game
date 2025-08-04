using UnityEditor;
using UnityEngine;

public class patrolPoints : MonoBehaviour
{
    public Color sectorColor = Color.white;
    public Transform[] patrolTransfroms;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log($"I Have Changed Patrol to {gameObject.name}");
           var enemy = other.GetComponent<AIControllerEnemy>().patrolPoints = patrolTransfroms;
        }

    }
    private void OnDrawGizmos()
    {
        //null check for patrol points so no 100000 errors
        if (patrolTransfroms != null)
        {
            foreach (var point in patrolTransfroms)
            {
                //null check for poins for also no 10000 errors
                if (point != null)
                {

                    Gizmos.DrawIcon(point.position, "point",true,sectorColor);
                }
            }
        }

    }
}

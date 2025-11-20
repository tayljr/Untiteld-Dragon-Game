using System;
using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 2.0f;
    public float resetDelay = 5.0f;
    
    private Vector3 resetPosition;
    private Quaternion resetRotation;

    private void OnEnable()
    {
        resetPosition = transform.position;
        resetRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            StartCoroutine(FallAfterDelay());
        }
    }

    IEnumerator FallAfterDelay()
    {
        yield return new WaitForSeconds(fallDelay);
        GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(resetDelay);
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = resetPosition;
        transform.rotation = resetRotation;
    }
}
using System.Collections;
using UnityEngine;

public class ItemFloat : MonoBehaviour
{
    private Vector3 floatPos;
    private float time;
    public AnimationCurve curve;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       floatPos = transform.position;
    }

    void FixedUpdate()
    {
        if (time <= 1)
        {
            time += Time.deltaTime;
            transform.position = new Vector3(floatPos.x, floatPos.y + (1f * curve.Evaluate(time)), floatPos.z);
        }
        else
            time = 0f;

    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}

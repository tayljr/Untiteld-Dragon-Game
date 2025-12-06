using System.Collections;
using UnityEngine;

public class DestoryTimer : MonoBehaviour
{
    public float Time;
    private void Start()
    {
        StartCoroutine(KYS());
    }
    public IEnumerator KYS()
    {
        yield return new WaitForSeconds(Time);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}

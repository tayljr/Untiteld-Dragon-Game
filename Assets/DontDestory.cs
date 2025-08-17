using UnityEngine;

public class DontDestory : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}

using Unity.Cinemachine;
using UnityEngine;

public class SetCameraToPlayer : MonoBehaviour
{

    private CinemachineCamera virtualCamera;
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player GameObject not found in the scene.");
            return;
        }
        virtualCamera = GetComponent<CinemachineCamera>();
        if (virtualCamera == null)
        {
            Debug.LogError("CinemachineCamera component not found on this GameObject.");
            return;
        }

        virtualCamera.Follow = player.transform;
    }

}

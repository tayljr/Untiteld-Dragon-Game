using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ResawpnFromStart()
    {
        RespawnSystem.instance.RespawnPlayer(0);
    }
    public void RespawnFromLastPoint()
    {
        RespawnSystem.instance.RespawnPlayer(RespawnSystem.instance.currentRespawnIndex);
    }
}

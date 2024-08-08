using UnityEngine;

public class DeathZone : DangerZone
{
    protected override void OnPlayerEnter(GameObject player)
    {
        EntityHealth health = player.GetComponent<EntityHealth>();
        
        health.InstantKill();
    }

    protected override void OnPlayerExit(GameObject player)
    {

    }
}
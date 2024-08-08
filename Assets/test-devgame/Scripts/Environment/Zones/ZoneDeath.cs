using UnityEngine;

public class DeathZone : DangerZone
{
    protected override void OnPlayerEnter(GameObject character)
    {
        EntityHealth health = character.GetComponent<EntityHealth>();

        health.InstantKill();
    }
}
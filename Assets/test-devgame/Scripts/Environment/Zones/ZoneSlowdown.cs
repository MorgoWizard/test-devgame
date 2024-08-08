using UnityEngine;

public class ZoneSlowdown : DangerZone
{
    [SerializeField] private float slowdownFactor = 0.6f;

    private PlayerController _playerController;

    protected override void OnPlayerEnter(GameObject player)
    {
        if (!_playerController)
        {
            _playerController = player.GetComponent<PlayerController>();
        }

        _playerController.ModifySpeed(slowdownFactor);
    }

    protected override void OnPlayerExit(GameObject player)
    {
        if (!_playerController)
        {
            _playerController = player.GetComponent<PlayerController>();
        }

        _playerController.ResetSpeed();
    }
}

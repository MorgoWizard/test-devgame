using UnityEngine;

public class ZoneSlowdown : DangerZone
{
    [SerializeField] private float slowdownFactor = 0.6f;

    private CharacterMovement _characterMovement;
    
    protected override void OnPlayerExit(GameObject character)
    {
        if (!_characterMovement)
        {
            _characterMovement = character.GetComponent<CharacterMovement>();
        }

        _characterMovement.ResetSpeed();
    }

    protected override void OnPlayerStay(GameObject character)
    {
        if (!_characterMovement)
        {
            _characterMovement = character.GetComponent<CharacterMovement>();
        }

        _characterMovement.ModifySpeed(slowdownFactor);
    }
}

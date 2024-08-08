using System.Collections;
using UnityEngine;

public class BonusSpeedup : BonusTemporary
{
    [SerializeField] private float speedModifier = 1.5f;
    protected override void ApplyEffect(GameObject character)
    {
        StartCoroutine(ApplyTemporaryBonus(character));
    }

    protected override IEnumerator ApplyTemporaryBonus(GameObject character)
    {
        CharacterMovement characterMovement = character.GetComponent<CharacterMovement>();
        characterMovement.ModifySpeed(speedModifier);
        yield return new WaitForSeconds(duration);
        characterMovement.ResetSpeed();
    }
}

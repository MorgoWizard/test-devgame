using System.Collections;
using UnityEngine;

public class BonusInvulnerable : BonusTemporary
{
    protected override void ApplyEffect(GameObject character)
    {
        StartCoroutine(ApplyTemporaryBonus(character));
    }

    protected override IEnumerator ApplyTemporaryBonus(GameObject character)
    {
        EntityHealth entityHealth = character.GetComponent<EntityHealth>();
        entityHealth.SetInvulnerable(true);
        yield return new WaitForSeconds(duration);
        entityHealth.SetInvulnerable(false);
    }
}

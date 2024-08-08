using System.Collections;
using UnityEngine;

public abstract class BonusTemporary : Bonus
{
    [SerializeField] protected float duration = 10f;

    protected abstract IEnumerator ApplyTemporaryBonus(GameObject character);
}

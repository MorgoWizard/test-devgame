using UnityEngine;

public class BonusWeapon : Bonus
{
    [field: SerializeField] public Weapon Weapon { get; private set; }
    protected override void ApplyEffect(GameObject character)
    {
        character.GetComponent<CharacterAttack>().EquipWeapon(Weapon);
    }
}

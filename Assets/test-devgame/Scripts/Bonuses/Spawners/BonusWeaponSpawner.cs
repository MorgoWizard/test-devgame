using System.Collections.Generic;
using UnityEngine;

public class BonusWeaponSpawner : BonusSpawner
{
    [SerializeField] private CharacterAttack characterAttack;

    protected override Bonus GetRandomBonus()
    {
        // Получаем текущее оружие игрока
        Weapon currentWeapon = characterAttack.CurrentWeapon;
        List<Bonus> validBonuses = new List<Bonus>();

        // Исключаем бонусы, связанные с текущим оружием
        foreach (var bonus in bonusPool)
        {
            if (bonus is BonusWeapon weaponBonus)
            {
                if (weaponBonus.Weapon.ID == currentWeapon.ID)
                {
                    continue; // Пропускаем бонус, связанный с текущим оружием
                }
            }
            validBonuses.Add(bonus);
        }

        int randomIndex = Random.Range(0, validBonuses.Count);
        return validBonuses[randomIndex];
    }
}

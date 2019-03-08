using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace Battle
{
    public class Fight
    {
        private static Dictionary<Weapon, int> _weaponDamage = new Dictionary<Weapon, int>() { { Weapon.BareFist, 1 }, { Weapon.Axe, 3 }, { Weapon.Spear, 2 }, { Weapon.Sword, 2 } };

        private readonly Soldier _attacker;
        private readonly Soldier _defender;

        public Fight(Soldier attacker, Soldier defender)
        {
            _attacker = attacker;
            _defender = defender;
        }


        public Soldier GetWinner()
        {
            if (BothSoldiersHaveMagicPotions())
            {
                return _attacker;
            }

            var attackerDamage = GetDamage(_attacker.Weapon, _defender.Weapon) +
                                 GetAttackerBonus(_attacker.Weapon, _defender.Weapon);
            var defenderDamage = GetDamage(_defender.Weapon, _attacker.Weapon);

            return attackerDamage >= defenderDamage ? _attacker : _defender;
        }

        private bool BothSoldiersHaveMagicPotions()
        {
            return _attacker.Weapon == Weapon.MagicPotion && _defender.Weapon == Weapon.MagicPotion;
        }

        private static int GetDamage(Weapon weapon, Weapon oponentsWeapon)
        {
            if (weapon == Weapon.MagicPotion)
                return (_weaponDamage[oponentsWeapon] % 2 == 0) ? 10 : 0;

            return _weaponDamage[weapon];
        }

        private static int GetAttackerBonus(Weapon weapon, Weapon oponentsWeapon)
        {
            if ((weapon == Weapon.Axe && oponentsWeapon == Weapon.Spear)
                || (weapon == Weapon.Spear && oponentsWeapon == Weapon.Sword)
                || (weapon == Weapon.Sword && oponentsWeapon == Weapon.Axe))
                return 3;
            return 0;
        }
    }
}
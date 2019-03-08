using System.Collections.Generic;

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
            return GetDamage(_attacker.Weapon) >= GetDamage(_defender.Weapon) ? _attacker : _defender;
        }

        private static int GetDamage(Weapon weapon)
        {
            return _weaponDamage[weapon];
        }
    }
}
using System;

namespace Battle
{
    public class Soldier
    {
        public string Name { get; }

        public Weapon Weapon { get; set; }

        public Soldier(string name, Weapon weapon = Weapon.BareFist)
        {
            Weapon = weapon;
            ValidateNameisNotBlank(name);
            Name = name;
        }

        private static void ValidateNameisNotBlank(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name can not be blank");
            }
        }
    }
}
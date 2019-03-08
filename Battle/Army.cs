using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle
{
    public class Army
    {
        public IList<Soldier> Soldiers { get; } = new List<Soldier>();

        public Soldier FrontMan => HasSoldiers ? Soldiers[0] : throw new EmptyArmyException();

        public bool HasSoldiers => Soldiers.Any();

        public void Enroll(Soldier soldier)
        {
            if (soldier == null)
                throw new ArgumentNullException();

            Soldiers.Add(soldier);
        }

        public void FrontManDies()
        {
            if (!HasSoldiers)
                throw new EmptyArmyException();
            Soldiers.RemoveAt(0);
        }
    }
}
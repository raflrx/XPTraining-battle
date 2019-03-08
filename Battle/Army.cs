using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Tests;

namespace Battle
{
    public class Army
    {
        private readonly IHeadQuarters _headQuarters;
        public IList<Soldier> Soldiers { get; } = new List<Soldier>();

        public Soldier FrontMan => HasSoldiers ? Soldiers[0] : throw new EmptyArmyException();

        public bool HasSoldiers => Soldiers.Any();

        public Army(IHeadQuarters headQuarters)
        {
            _headQuarters = headQuarters;
        }

        public void Enroll(Soldier soldier)
        {
            if (soldier == null)
                throw new ArgumentNullException();

            soldier.Id = _headQuarters.ReportEnlistment(soldier.Name);
            Soldiers.Add(soldier);
        }

        public void FrontManDies()
        {
            if (!HasSoldiers)
                throw new EmptyArmyException();

            _headQuarters.ReportCasualty(Soldiers[0].Id);

            Soldiers.RemoveAt(0);
        }
    }
}
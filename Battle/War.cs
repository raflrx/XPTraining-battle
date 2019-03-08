using System;
using Battle.Tests;

namespace Battle
{
    public class War
    {
        private readonly Army _attacker;
        private readonly Army _defender;
        private readonly IHeadQuarters _headQuarters;

        public War(Army attacker, Army defender, IHeadQuarters headQuarters)
        {
            _attacker = attacker;
            _defender = defender;
            _headQuarters = headQuarters;
        }


        public Army GetWinner()
        {
            while (_attacker.HasSoldiers && _defender.HasSoldiers)
            {
                var fight = new Fight(_attacker.FrontMan, _defender.FrontMan);
                var winner = fight.GetWinner();
                if (winner == _attacker.FrontMan)
                    _defender.FrontManDies();
                else
                    _attacker.FrontManDies();
            }

            var winningArmy = _attacker.HasSoldiers ? _attacker : _defender;

            _headQuarters.ReportVictory(winningArmy.Soldiers.Count);

            return winningArmy;
        }
    }
}
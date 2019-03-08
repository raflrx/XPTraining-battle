using System;

namespace Battle
{
    public class War
    {
        private readonly Army _attacker;
        private readonly Army _defender;

        public War(Army attacker, Army defender)
        {
            _attacker = attacker;
            _defender = defender;
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

            return _attacker.HasSoldiers ? _attacker : _defender;
        }
    }
}
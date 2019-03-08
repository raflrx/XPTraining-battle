using System;

namespace Battle.Tests
{
    public class HeadQuarters : IHeadQuarters
    {
        private int id = 0;

        public int ReportEnlistment(string soldierName)
        {
            if (string.IsNullOrEmpty(soldierName))
            throw new ArgumentException();

            id++;
            return id;
        }

        public void ReportCasualty(int soldierId)
        {
            throw new NotImplementedException();
        }

        public void ReportVictory(int remainingNumberOfSoldiers)
        {
            throw new NotImplementedException();
        }
    }
}
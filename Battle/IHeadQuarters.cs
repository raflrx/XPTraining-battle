namespace Battle.Tests
{
    public interface IHeadQuarters
    {
        int ReportEnlistment(string soldierName);
        void ReportCasualty(int soldierId);
        void ReportVictory(int remainingNumberOfSoldiers);
    }
}
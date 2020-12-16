namespace TestingCombinatoriale
{
    public class JacocoLine
    {
        public int CoveredBranches;
        public int CoveredInstructions;
        public bool Hit;
        public int Id;
        public int LineNumber;
        public int MissedBranches;
        public int MissedInstructions;

        public JacocoLine(int id, int lineNumber, int missedInstructions, int coveredInstructions, int missedBranches,
            int coveredBranches)
        {
            Id = id;
            LineNumber = lineNumber;
            MissedInstructions = missedInstructions;
            CoveredInstructions = coveredInstructions;
            MissedBranches = missedBranches;
            CoveredBranches = coveredBranches;
            VerifyHit();
        }

        private void VerifyHit()
        {
            if (MissedBranches > 0 || CoveredBranches > 0) // la linea è un branch
            {
                if (CoveredBranches > 0) Hit = true;
            }
            else // la linea è uno statement
            {
                if (CoveredInstructions > 0) Hit = true;
            }
        }
    }
}
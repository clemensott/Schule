namespace LabyrinthSim
{
    class BestCaseInterpreter : IRelationInterpreter
    {
        public bool IsOpen(int relationValue)
        {
            return relationValue != 1;
        }
    }
}

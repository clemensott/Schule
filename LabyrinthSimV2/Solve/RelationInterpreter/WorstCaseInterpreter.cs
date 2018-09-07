namespace LabyrinthSim
{
    class WorstCaseInterpreter : IRelationInterpreter
    {
        public bool IsOpen(int relationValue)
        {
            return relationValue == 0;
        }
    }
}

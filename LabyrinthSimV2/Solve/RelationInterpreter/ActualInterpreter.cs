namespace LabyrinthSim
{
    class ActualInterpreter : IRelationInterpreter
    {
        public bool IsOpen(int relationValue)
        {
            return relationValue == 0 || relationValue == 2;
        }
    }
}

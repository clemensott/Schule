using System.Threading.Tasks;

namespace LabyrinthSim
{
    interface ISearcher
    {
        bool IsSearching { get; }

        Task BeginSearch();

        void CancelSearch();
    }
}

using DreamHome_Mobile_SQLite.Models;


namespace DreamHome_Mobile_SQLite.Contexts
{
    /// <summary>
    /// App-wide context class to store current selected branch
    /// </summary>


    public interface IBranchContext
    {
        Branch? Current { get; set; }
        bool HasBranch => Current != null;
    }


    public class BranchContext : IBranchContext
    {
        public Branch? Current { get; set; }
    }

}

using DreamHome_Mobile_SQLite.Models;

namespace DreamHome_Mobile_SQLite.Services
{
    /// <summary>
    /// DreamHome Service interface class
    /// </summary>
    public interface IDreamHomeService
    {

        /// <summary>
        /// Get all branches
        /// </summary>
        /// <returns>List Branch objects</returns>
        Task<List<Branch>> GetBranches();

        /// <summary>
        /// Get properties for given branch
        /// </summary>
        /// <returns>List Property objects</returns>
        Task<List<PropertyForRent>> GetPropertiesForBranch(string branchNo);


        /// <summary>
        /// Get staff for given branch
        /// </summary>
        /// <returns>List Staff objects</returns>
        Task<List<Staff>> GetStaffForBranch(string branchNo);


        /// <summary>
        /// Get staff with a salary above a given threshold
        /// </summary>
        /// <param name="salary">slary threshold</param>
        /// <returns>List Staff objects</returns>
        Task<List<Staff>> GetHighEarners(decimal salary);


        /// <summary>
        /// Get a count of properties viewed between the given start and end dates
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>integer count</returns>
        Task<int> GetPropertyViewingsCount(DateTime startDate, DateTime endDate);


        /// <summary>
        /// Get a list of properties viewed between the given start and end dates
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>integer count</returns>
        Task<List<Viewing>> GetPropertyViewings(DateTime startDate, DateTime endDate);


        /// <summary>
        /// Get all client viewings
        /// </summary>
        /// <returns>List ClientViewing objects</returns>
        Task<List<ClientViewing>> GetClientViewings();


        /// <summary>
        /// Get count of staff for each branch
        /// </summary>
        /// <returns>List BranchStaffCount objects</returns>
        Task<List<BranchStaffCount>> GetBranchStaffCount();


        /// <summary>
        /// Get list of staff in branches in a given city
        /// </summary>
        /// <param name="city">city to be filtered</param>
        /// <returns>List CityStaff objects</returns>
        Task<List<CityStaff>> GetCityStaff(string city);


        /// <summary>
        /// Add/Update a staff record
        /// </summary>
        /// <param name="staff">Staff record to be added/updated</param>
        /// <returns>Updated staff object</returns>
        Task<Staff> AddOrUpdateStaffAsync(Staff staff);

    }
}
